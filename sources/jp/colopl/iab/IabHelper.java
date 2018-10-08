package jp.colopl.iab;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentSender.SendIntentException;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.os.RemoteException;
import android.util.Log;
import com.android.vending.billing.IInAppBillingService;
import com.android.vending.billing.IInAppBillingService.Stub;
import com.appsflyer.share.Constants;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import org.json.JSONException;
import org.onepf.oms.appstore.GooglePlay;

public class IabHelper {
    public static final int ARBITARY_REQUEST_CODE = 10001;
    public static final int BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE = 3;
    public static final int BILLING_RESPONSE_RESULT_DEVELOPER_ERROR = 5;
    public static final int BILLING_RESPONSE_RESULT_ERROR = 6;
    public static final int BILLING_RESPONSE_RESULT_ITEM_ALREADY_OWNED = 7;
    public static final int BILLING_RESPONSE_RESULT_ITEM_NOT_OWNED = 8;
    public static final int BILLING_RESPONSE_RESULT_ITEM_UNAVAILABLE = 4;
    public static final int BILLING_RESPONSE_RESULT_OK = 0;
    public static final int BILLING_RESPONSE_RESULT_USER_CANCELED = 1;
    public static final String GET_SKU_DETAILS_ITEM_LIST = "ITEM_ID_LIST";
    public static final String GET_SKU_DETAILS_ITEM_TYPE_LIST = "ITEM_TYPE_LIST";
    public static final int IABHELPER_BAD_RESPONSE = -1002;
    public static final int IABHELPER_ERROR_BASE = -1000;
    public static final int IABHELPER_INVALID_CONSUMPTION = -1010;
    public static final int IABHELPER_MISSING_TOKEN = -1007;
    public static final int IABHELPER_REMOTE_EXCEPTION = -1001;
    public static final int IABHELPER_SEND_INTENT_FAILED = -1004;
    public static final int IABHELPER_SUBSCRIPTIONS_NOT_AVAILABLE = -1009;
    public static final int IABHELPER_UNKNOWN_ERROR = -1008;
    public static final int IABHELPER_UNKNOWN_PURCHASE_RESPONSE = -1006;
    public static final int IABHELPER_USER_CANCELLED = -1005;
    public static final int IABHELPER_VERIFICATION_FAILED = -1003;
    public static final String INAPP_CONTINUATION_TOKEN = "INAPP_CONTINUATION_TOKEN";
    public static final String ITEM_TYPE_INAPP = "inapp";
    public static final String ITEM_TYPE_SUBS = "subs";
    public static final String RESPONSE_BUY_INTENT = "BUY_INTENT";
    public static final String RESPONSE_CODE = "RESPONSE_CODE";
    public static final String RESPONSE_GET_SKU_DETAILS_LIST = "DETAILS_LIST";
    public static final String RESPONSE_INAPP_ITEM_LIST = "INAPP_PURCHASE_ITEM_LIST";
    public static final String RESPONSE_INAPP_PURCHASE_DATA = "INAPP_PURCHASE_DATA";
    public static final String RESPONSE_INAPP_PURCHASE_DATA_LIST = "INAPP_PURCHASE_DATA_LIST";
    public static final String RESPONSE_INAPP_SIGNATURE = "INAPP_DATA_SIGNATURE";
    public static final String RESPONSE_INAPP_SIGNATURE_LIST = "INAPP_DATA_SIGNATURE_LIST";
    boolean mAsyncInProgress = false;
    String mAsyncOperation = "";
    Context mContext;
    boolean mDebugLog = false;
    String mDebugTag = "IabHelper";
    OnIabPurchaseFinishedListener mPurchaseListener;
    String mPurchasingItemType;
    int mRequestCode;
    IInAppBillingService mService;
    ServiceConnection mServiceConn;
    boolean mSetupDone = false;
    String mSignatureBase64 = null;
    boolean mSubscriptionsSupported = false;
    boolean mV6IAPSupported = false;

    public interface QueryInventoryFinishedListener {
        void onQueryInventoryFinished(IabResult iabResult, Inventory inventory);
    }

    public interface OnConsumeMultiFinishedListener {
        void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2);
    }

    public interface OnIabSetupFinishedListener {
        void onIabSetupFinished(IabResult iabResult);
    }

    public interface OnConsumeFinishedListener {
        void onConsumeFinished(Purchase purchase, IabResult iabResult);
    }

    public interface OnIabPurchaseFinishedListener {
        void onIabPurchaseFinished(IabResult iabResult, Purchase purchase);
    }

    public IabHelper(Context context, String str) {
        this.mContext = context.getApplicationContext();
        this.mSignatureBase64 = str;
        logDebug("IAB helper created.");
    }

    public static String getResponseDesc(int i) {
        String[] split = "0:OK/1:User Canceled/2:Unknown/3:Billing Unavailable/4:Item unavailable/5:Developer Error/6:Error/7:Item Already Owned/8:Item not owned".split(Constants.URL_PATH_DELIMITER);
        String[] split2 = "0:OK/-1001:Remote exception during initialization/-1002:Bad response received/-1003:Purchase signature verification failed/-1004:Send intent failed/-1005:User cancelled/-1006:Unknown purchase response/-1007:Missing token/-1008:Unknown error/-1009:Subscriptions not available/-1010:Invalid consumption attempt".split(Constants.URL_PATH_DELIMITER);
        if (i > -1000) {
            return (i < 0 || i >= split.length) ? String.valueOf(i) + ":Unknown" : split[i];
        } else {
            int i2 = -1000 - i;
            return (i2 < 0 || i2 >= split2.length) ? String.valueOf(i) + ":Unknown IAB Helper Error" : split2[i2];
        }
    }

    public boolean IsSetupDone() {
        return this.mSetupDone;
    }

    void checkSetupDone(String str) {
        if (!this.mSetupDone) {
            logError("Illegal state for operation (" + str + "): IAB helper is not set up.");
            throw new IllegalStateException("IAB helper is not set up. Can't perform operation: " + str);
        }
    }

    void consume(Purchase purchase) throws IabException {
        checkSetupDone("consume");
        if (purchase.mItemType.equals("inapp")) {
            try {
                String token = purchase.getToken();
                String sku = purchase.getSku();
                if (token == null || token.equals("")) {
                    logError("Can't consume " + sku + ". No token.");
                    throw new IabException(-1007, "PurchaseInfo is missing token for sku: " + sku + " " + purchase);
                }
                logDebug("Consuming sku: " + sku + ", token: " + token);
                int consumePurchase = this.mService.consumePurchase(3, this.mContext.getPackageName(), token);
                if (consumePurchase == 0) {
                    logDebug("Successfully consumed sku: " + sku);
                    return;
                } else {
                    logDebug("Error consuming consuming sku " + sku + ". " + getResponseDesc(consumePurchase));
                    throw new IabException(consumePurchase, "Error consuming sku " + sku);
                }
            } catch (Exception e) {
                throw new IabException(-1001, "Remote exception while consuming. PurchaseInfo: " + purchase, e);
            }
        }
        throw new IabException(-1010, "Items of type '" + purchase.mItemType + "' can't be consumed.");
    }

    public void consumeAsync(List<Purchase> list, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        consumeAsyncInternal(list, null, onConsumeMultiFinishedListener);
    }

    public void consumeAsync(Purchase purchase, OnConsumeFinishedListener onConsumeFinishedListener) {
        checkSetupDone("consume");
        List arrayList = new ArrayList();
        arrayList.add(purchase);
        consumeAsyncInternal(arrayList, onConsumeFinishedListener, null);
    }

    void consumeAsyncInternal(List<Purchase> list, OnConsumeFinishedListener onConsumeFinishedListener, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        final Handler handler = new Handler(Looper.getMainLooper());
        flagStartAsync("consume");
        final List<Purchase> list2 = list;
        final OnConsumeFinishedListener onConsumeFinishedListener2 = onConsumeFinishedListener;
        final OnConsumeMultiFinishedListener onConsumeMultiFinishedListener2 = onConsumeMultiFinishedListener;
        new Thread(new Runnable() {
            public void run() {
                final List arrayList = new ArrayList();
                for (Purchase purchase : list2) {
                    try {
                        IabHelper.this.consume(purchase);
                        arrayList.add(new IabResult(0, "Successful consume of sku " + purchase.getSku()));
                    } catch (IabException e) {
                        arrayList.add(e.getResult());
                    }
                }
                IabHelper.this.flagEndAsync();
                if (onConsumeFinishedListener2 != null) {
                    handler.post(new Runnable() {
                        public void run() {
                            onConsumeFinishedListener2.onConsumeFinished((Purchase) list2.get(0), (IabResult) arrayList.get(0));
                        }
                    });
                }
                if (onConsumeMultiFinishedListener2 != null) {
                    handler.post(new Runnable() {
                        public void run() {
                            onConsumeMultiFinishedListener2.onConsumeMultiFinished(list2, arrayList);
                        }
                    });
                }
            }
        }).start();
    }

    public void dispose() {
        logDebug("Disposing.");
        this.mSetupDone = false;
        if (this.mServiceConn != null) {
            logDebug("Unbinding from service.");
            if (this.mContext != null) {
                this.mContext.unbindService(this.mServiceConn);
            }
            this.mServiceConn = null;
            this.mService = null;
            this.mPurchaseListener = null;
        }
    }

    public void enableDebugLogging(boolean z) {
        this.mDebugLog = z;
    }

    public void enableDebugLogging(boolean z, String str) {
        this.mDebugLog = z;
        this.mDebugTag = str;
    }

    void flagEndAsync() {
        logDebug("Ending async operation: " + this.mAsyncOperation);
        this.mAsyncOperation = "";
        this.mAsyncInProgress = false;
    }

    void flagStartAsync(String str) {
        if (this.mAsyncInProgress) {
            throw new IllegalStateException("Can't start async operation (" + str + ") because another async operation(" + this.mAsyncOperation + ") is in progress.");
        }
        this.mAsyncOperation = str;
        this.mAsyncInProgress = true;
        logDebug("Starting async operation: " + str);
    }

    int getResponseCodeFromBundle(Bundle bundle) {
        Object obj = bundle.get("RESPONSE_CODE");
        if (obj == null) {
            logDebug("Bundle with null response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            logError("Unexpected type for bundle response code.");
            logError(obj.getClass().getName());
            throw new RuntimeException("Unexpected type for bundle response code: " + obj.getClass().getName());
        }
    }

    int getResponseCodeFromIntent(Intent intent) {
        Object obj = intent.getExtras().get("RESPONSE_CODE");
        if (obj == null) {
            logError("Intent with no response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            logError("Unexpected type for intent response code.");
            logError(obj.getClass().getName());
            throw new RuntimeException("Unexpected type for intent response code: " + obj.getClass().getName());
        }
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        IabResult iabResult;
        JSONException e;
        if (i != this.mRequestCode) {
            return false;
        }
        checkSetupDone("handleActivityResult");
        flagEndAsync();
        if (intent == null) {
            logError("Null data in IAB activity result.");
            iabResult = new IabResult(-1002, "Null data in IAB result");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            }
            return true;
        }
        int responseCodeFromIntent = getResponseCodeFromIntent(intent);
        String stringExtra = intent.getStringExtra("INAPP_PURCHASE_DATA");
        String stringExtra2 = intent.getStringExtra("INAPP_DATA_SIGNATURE");
        if (i2 == -1 && responseCodeFromIntent == 0) {
            logDebug("Successful resultcode from purchase activity.");
            logDebug("Purchase data: " + stringExtra);
            logDebug("Data signature: " + stringExtra2);
            logDebug("Extras: " + intent.getExtras());
            logDebug("Expected item type: " + this.mPurchasingItemType);
            if (stringExtra == null || stringExtra2 == null) {
                logError("BUG: either purchaseData or dataSignature is null.");
                logDebug("Extras: " + intent.getExtras().toString());
                iabResult = new IabResult(-1008, "IAB returned null purchaseData or dataSignature");
                if (this.mPurchaseListener != null) {
                    this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
                }
                return true;
            }
            try {
                Purchase purchase = new Purchase(this.mPurchasingItemType, stringExtra, stringExtra2);
                try {
                    String sku = purchase.getSku();
                    if (Security.verifyPurchase(this.mSignatureBase64, stringExtra, stringExtra2)) {
                        logDebug("Purchase signature successfully verified.");
                        if (this.mPurchaseListener != null) {
                            this.mPurchaseListener.onIabPurchaseFinished(new IabResult(0, "Success"), purchase);
                        }
                    } else {
                        logError("Purchase signature verification FAILED for sku " + sku);
                        iabResult = new IabResult(-1003, "Signature verification failed for sku " + sku);
                        if (this.mPurchaseListener != null) {
                            this.mPurchaseListener.onIabPurchaseFinished(iabResult, purchase);
                        }
                        return true;
                    }
                } catch (JSONException e2) {
                    e = e2;
                    logError("Failed to parse purchase data.");
                    e.printStackTrace();
                    iabResult = new IabResult(-1002, "Failed to parse purchase data.");
                    if (this.mPurchaseListener != null) {
                        this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
                    }
                    return true;
                }
            } catch (JSONException e3) {
                e = e3;
                logError("Failed to parse purchase data.");
                e.printStackTrace();
                iabResult = new IabResult(-1002, "Failed to parse purchase data.");
                if (this.mPurchaseListener != null) {
                    this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
                }
                return true;
            }
        } else if (i2 == -1) {
            logDebug("Result code was OK but in-app billing response was not OK: " + getResponseDesc(responseCodeFromIntent));
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(new IabResult(responseCodeFromIntent, "Problem purchashing item."), null);
            }
        } else if (i2 == 0) {
            logDebug("Purchase canceled - Response: " + getResponseDesc(responseCodeFromIntent));
            iabResult = new IabResult(-1005, "User canceled.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            }
        } else {
            logError("Purchase failed. Result code: " + Integer.toString(i2) + ". Response: " + getResponseDesc(responseCodeFromIntent));
            iabResult = new IabResult(-1006, "Unknown purchase response.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            }
        }
        return true;
    }

    public void launchPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "", str2);
    }

    public void launchPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2, String str3) {
        launchPurchaseFlow(activity, str, "inapp", i, onIabPurchaseFinishedListener, str2, str3);
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3, String str4) {
        checkSetupDone("launchPurchaseFlow");
        flagStartAsync("launchPurchaseFlow");
        IabResult iabResult;
        if (!str2.equals("subs") || this.mSubscriptionsSupported) {
            try {
                Bundle buyIntentExtraParams;
                logDebug("Constructing buy intent for " + str + ", item type: " + str2);
                Bundle bundle = new Bundle();
                if (!(str4 == null || str4.length() == 0)) {
                    bundle.putString("accountId", str4);
                }
                if (this.mV6IAPSupported) {
                    logDebug("Call mService.getBuyIntentExtraParams, hash=" + str4);
                    buyIntentExtraParams = this.mService.getBuyIntentExtraParams(6, this.mContext.getPackageName(), str, str2, str3, bundle);
                } else {
                    logDebug("Call mService.getBuyIntent");
                    buyIntentExtraParams = this.mService.getBuyIntent(3, this.mContext.getPackageName(), str, str2, str3);
                }
                int responseCodeFromBundle = getResponseCodeFromBundle(buyIntentExtraParams);
                if (responseCodeFromBundle != 0) {
                    logError("Unable to buy item, Error response: " + getResponseDesc(responseCodeFromBundle));
                    iabResult = new IabResult(responseCodeFromBundle, "Unable to buy item");
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                        return;
                    }
                    return;
                }
                PendingIntent pendingIntent = (PendingIntent) buyIntentExtraParams.getParcelable("BUY_INTENT");
                logDebug("Launching buy intent for " + str + ". Request code: " + i);
                this.mRequestCode = i;
                this.mPurchaseListener = onIabPurchaseFinishedListener;
                this.mPurchasingItemType = str2;
                activity.startIntentSenderForResult(pendingIntent.getIntentSender(), i, new Intent(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue());
                return;
            } catch (SendIntentException e) {
                logError("SendIntentException while launching purchase flow for sku " + str);
                e.printStackTrace();
                iabResult = new IabResult(-1004, "Failed to send intent.");
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                    return;
                }
                return;
            } catch (RemoteException e2) {
                logError("RemoteException while launching purchase flow for sku " + str);
                e2.printStackTrace();
                iabResult = new IabResult(-1001, "Remote exception while starting purchase flow");
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                    return;
                }
                return;
            }
        }
        iabResult = new IabResult(-1009, "Subscriptions are not available.");
        if (onIabPurchaseFinishedListener != null) {
            onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
        }
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchSubscriptionPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "", str2);
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2, String str3) {
        launchPurchaseFlow(activity, str, "subs", i, onIabPurchaseFinishedListener, str2, str3);
    }

    void logDebug(String str) {
        if (this.mDebugLog) {
            Log.d(this.mDebugTag, str);
        }
    }

    void logError(String str) {
        Log.e(this.mDebugTag, "In-app billing error: " + str);
    }

    void logWarn(String str) {
        Log.w(this.mDebugTag, "In-app billing warning: " + str);
    }

    public Inventory queryInventory(boolean z, List<String> list) throws IabException {
        return queryInventory(z, list, null);
    }

    public Inventory queryInventory(boolean z, List<String> list, List<String> list2) throws IabException {
        checkSetupDone("queryInventory");
        try {
            Inventory inventory = new Inventory();
            int queryPurchases = queryPurchases(inventory, "inapp");
            if (queryPurchases != 0) {
                throw new IabException(queryPurchases, "Error refreshing inventory (querying owned items).");
            }
            if (z) {
                queryPurchases = querySkuDetails("inapp", inventory, list);
                if (queryPurchases != 0) {
                    throw new IabException(queryPurchases, "Error refreshing inventory (querying prices of items).");
                }
            }
            if (this.mSubscriptionsSupported) {
                queryPurchases = queryPurchases(inventory, "subs");
                if (queryPurchases != 0) {
                    throw new IabException(queryPurchases, "Error refreshing inventory (querying owned subscriptions).");
                } else if (z) {
                    queryPurchases = querySkuDetails("subs", inventory, list);
                    if (queryPurchases != 0) {
                        throw new IabException(queryPurchases, "Error refreshing inventory (querying prices of subscriptions).");
                    }
                }
            }
            return inventory;
        } catch (Exception e) {
            throw new IabException(-1001, "Remote exception while refreshing inventory.", e);
        } catch (Exception e2) {
            throw new IabException(-1002, "Error parsing JSON response while refreshing inventory.", e2);
        }
    }

    public void queryInventoryAsync(QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(true, null, queryInventoryFinishedListener);
    }

    public void queryInventoryAsync(boolean z, List<String> list, QueryInventoryFinishedListener queryInventoryFinishedListener) {
        final Handler handler = new Handler(Looper.getMainLooper());
        checkSetupDone("queryInventory");
        flagStartAsync("refresh inventory");
        final boolean z2 = z;
        final List<String> list2 = list;
        final QueryInventoryFinishedListener queryInventoryFinishedListener2 = queryInventoryFinishedListener;
        new Thread(new Runnable() {
            public void run() {
                IabResult iabResult = new IabResult(0, "Inventory refresh successful.");
                Inventory inventory = null;
                try {
                    inventory = IabHelper.this.queryInventory(z2, list2);
                } catch (IabException e) {
                    iabResult = e.getResult();
                }
                IabHelper.this.flagEndAsync();
                handler.post(new Runnable() {
                    public void run() {
                        queryInventoryFinishedListener2.onQueryInventoryFinished(iabResult, inventory);
                    }
                });
            }
        }).start();
    }

    public void queryInventoryAsync(boolean z, QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(z, null, queryInventoryFinishedListener);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    int queryPurchases(jp.colopl.iab.Inventory r13, java.lang.String r14) throws org.json.JSONException, android.os.RemoteException {
        /*
        r12 = this;
        r5 = 0;
        r0 = new java.lang.StringBuilder;
        r0.<init>();
        r1 = "Querying owned items, item type: ";
        r0 = r0.append(r1);
        r0 = r0.append(r14);
        r0 = r0.toString();
        r12.logDebug(r0);
        r0 = new java.lang.StringBuilder;
        r0.<init>();
        r1 = "Package name: ";
        r0 = r0.append(r1);
        r1 = r12.mContext;
        r1 = r1.getPackageName();
        r0 = r0.append(r1);
        r0 = r0.toString();
        r12.logDebug(r0);
        r0 = 0;
        r1 = r12.mService;
        if (r1 != 0) goto L_0x0199;
    L_0x0038:
        r0 = -1008; // 0xfffffffffffffc10 float:NaN double:NaN;
    L_0x003a:
        return r0;
    L_0x003b:
        r1 = r0;
        r0 = r3;
    L_0x003d:
        r2 = new java.lang.StringBuilder;
        r2.<init>();
        r3 = "Calling getPurchases with continuation token: ";
        r2 = r2.append(r3);
        r2 = r2.append(r1);
        r2 = r2.toString();
        r12.logDebug(r2);
        r2 = r12.mService;
        r3 = 3;
        r4 = r12.mContext;
        r4 = r4.getPackageName();
        r6 = r2.getPurchases(r3, r4, r14, r1);
        r1 = r12.getResponseCodeFromBundle(r6);
        r2 = new java.lang.StringBuilder;
        r2.<init>();
        r3 = "Owned items response: ";
        r2 = r2.append(r3);
        r3 = java.lang.String.valueOf(r1);
        r2 = r2.append(r3);
        r2 = r2.toString();
        r12.logDebug(r2);
        if (r1 == 0) goto L_0x009c;
    L_0x0080:
        r0 = new java.lang.StringBuilder;
        r0.<init>();
        r2 = "getPurchases() failed: ";
        r0 = r0.append(r2);
        r2 = getResponseDesc(r1);
        r0 = r0.append(r2);
        r0 = r0.toString();
        r12.logDebug(r0);
        r0 = r1;
        goto L_0x003a;
    L_0x009c:
        r1 = "INAPP_PURCHASE_ITEM_LIST";
        r1 = r6.containsKey(r1);
        if (r1 == 0) goto L_0x00b4;
    L_0x00a4:
        r1 = "INAPP_PURCHASE_DATA_LIST";
        r1 = r6.containsKey(r1);
        if (r1 == 0) goto L_0x00b4;
    L_0x00ac:
        r1 = "INAPP_DATA_SIGNATURE_LIST";
        r1 = r6.containsKey(r1);
        if (r1 != 0) goto L_0x00bd;
    L_0x00b4:
        r0 = "Bundle returned from getPurchases() doesn't contain required fields.";
        r12.logError(r0);
        r0 = -1002; // 0xfffffffffffffc16 float:NaN double:NaN;
        goto L_0x003a;
    L_0x00bd:
        r1 = "INAPP_PURCHASE_ITEM_LIST";
        r7 = r6.getStringArrayList(r1);
        r1 = "INAPP_PURCHASE_DATA_LIST";
        r8 = r6.getStringArrayList(r1);
        r1 = "INAPP_DATA_SIGNATURE_LIST";
        r9 = r6.getStringArrayList(r1);
        r3 = r0;
        r4 = r5;
    L_0x00d1:
        r0 = r8.size();
        if (r4 >= r0) goto L_0x016e;
    L_0x00d7:
        r0 = r8.get(r4);
        r2 = r0;
        r2 = (java.lang.String) r2;
        r0 = r9.get(r4);
        r0 = (java.lang.String) r0;
        r1 = r7.get(r4);
        r1 = (java.lang.String) r1;
        r10 = r12.mSignatureBase64;
        r10 = jp.colopl.iab.Security.verifyPurchase(r10, r2, r0);
        if (r10 == 0) goto L_0x013b;
    L_0x00f2:
        r10 = new java.lang.StringBuilder;
        r10.<init>();
        r11 = "Sku is owned: ";
        r10 = r10.append(r11);
        r1 = r10.append(r1);
        r1 = r1.toString();
        r12.logDebug(r1);
        r1 = new jp.colopl.iab.Purchase;
        r1.<init>(r14, r2, r0);
        r0 = r1.getToken();
        r0 = android.text.TextUtils.isEmpty(r0);
        if (r0 == 0) goto L_0x0132;
    L_0x0117:
        r0 = "BUG: empty/null token!";
        r12.logWarn(r0);
        r0 = new java.lang.StringBuilder;
        r0.<init>();
        r10 = "Purchase data: ";
        r0 = r0.append(r10);
        r0 = r0.append(r2);
        r0 = r0.toString();
        r12.logDebug(r0);
    L_0x0132:
        r13.addPurchase(r1);
        r0 = r3;
    L_0x0136:
        r1 = r4 + 1;
        r3 = r0;
        r4 = r1;
        goto L_0x00d1;
    L_0x013b:
        r1 = "Purchase signature verification **FAILED**. Not adding item.";
        r12.logWarn(r1);
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r3 = "   Purchase data: ";
        r1 = r1.append(r3);
        r1 = r1.append(r2);
        r1 = r1.toString();
        r12.logDebug(r1);
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = "   Signature: ";
        r1 = r1.append(r2);
        r0 = r1.append(r0);
        r0 = r0.toString();
        r12.logDebug(r0);
        r0 = 1;
        goto L_0x0136;
    L_0x016e:
        r0 = "INAPP_CONTINUATION_TOKEN";
        r0 = r6.getString(r0);
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = "Continuation token: ";
        r1 = r1.append(r2);
        r1 = r1.append(r0);
        r1 = r1.toString();
        r12.logDebug(r1);
        r1 = android.text.TextUtils.isEmpty(r0);
        if (r1 == 0) goto L_0x003b;
    L_0x0190:
        if (r3 == 0) goto L_0x0196;
    L_0x0192:
        r0 = -1003; // 0xfffffffffffffc15 float:NaN double:NaN;
        goto L_0x003a;
    L_0x0196:
        r0 = r5;
        goto L_0x003a;
    L_0x0199:
        r1 = r0;
        r0 = r5;
        goto L_0x003d;
        */
        throw new UnsupportedOperationException("Method not decompiled: jp.colopl.iab.IabHelper.queryPurchases(jp.colopl.iab.Inventory, java.lang.String):int");
    }

    int querySkuDetails(String str, Inventory inventory, List<String> list) throws RemoteException, JSONException {
        logDebug("Querying SKU details.");
        ArrayList arrayList = new ArrayList();
        arrayList.addAll(inventory.getAllOwnedSkus(str));
        if (list != null) {
            arrayList.addAll(list);
        }
        if (arrayList.size() == 0) {
            logDebug("queryPrices: nothing to do because there are no SKUs.");
            return 0;
        }
        Bundle bundle = new Bundle();
        ArrayList arrayList2 = new ArrayList();
        int i = 0;
        while (i < arrayList.size()) {
            ArrayList arrayList3 = new ArrayList();
            arrayList3.addAll(arrayList.subList(i, i + 20 > arrayList.size() ? arrayList.size() : i + 20));
            bundle.putStringArrayList("ITEM_ID_LIST", arrayList3);
            Bundle skuDetails = this.mService.getSkuDetails(3, this.mContext.getPackageName(), str, bundle);
            if (skuDetails.containsKey("DETAILS_LIST")) {
                arrayList2.addAll(skuDetails.getStringArrayList("DETAILS_LIST"));
                i += 20;
            } else {
                int responseCodeFromBundle = getResponseCodeFromBundle(skuDetails);
                if (responseCodeFromBundle != 0) {
                    logDebug("getSkuDetails() failed: " + getResponseDesc(responseCodeFromBundle));
                    return responseCodeFromBundle;
                }
                logError("getSkuDetails() returned a bundle with neither an error nor a detail list.");
                return -1002;
            }
        }
        Iterator it = arrayList2.iterator();
        while (it.hasNext()) {
            SkuDetails skuDetails2 = new SkuDetails(str, (String) it.next());
            logDebug("Got sku details: " + skuDetails2);
            inventory.addSkuDetails(skuDetails2);
        }
        return 0;
    }

    public void startSetup(final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        if (this.mSetupDone) {
            throw new IllegalStateException("IAB helper is already set up.");
        }
        logDebug("Starting in-app billing setup.");
        this.mServiceConn = new ServiceConnection() {
            public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                int i = 3;
                IabHelper.this.logDebug("Billing service connected.");
                IabHelper.this.mService = Stub.asInterface(iBinder);
                String packageName = IabHelper.this.mContext.getPackageName();
                try {
                    IabHelper.this.logDebug("Checking for in-app billing 3 support.");
                    if (IabHelper.this.mService.isBillingSupported(6, packageName, "inapp") != 0) {
                        int isBillingSupported = IabHelper.this.mService.isBillingSupported(3, packageName, "inapp");
                        if (isBillingSupported != 0) {
                            if (onIabSetupFinishedListener != null) {
                                onIabSetupFinishedListener.onIabSetupFinished(new IabResult(isBillingSupported, "Error checking for billing v3 support."));
                            }
                            IabHelper.this.mSubscriptionsSupported = false;
                            return;
                        }
                    }
                    IabHelper.this.mV6IAPSupported = true;
                    IabHelper iabHelper = IabHelper.this;
                    StringBuilder append = new StringBuilder().append("In-app billing version ");
                    if (IabHelper.this.mV6IAPSupported) {
                        i = 6;
                    }
                    iabHelper.logDebug(append.append(i).append(" supported for ").append(packageName).toString());
                    i = IabHelper.this.mService.isBillingSupported(3, packageName, "subs");
                    if (i == 0) {
                        IabHelper.this.logDebug("Subscriptions AVAILABLE.");
                        IabHelper.this.mSubscriptionsSupported = true;
                    } else {
                        IabHelper.this.logDebug("Subscriptions NOT AVAILABLE. Response: " + i);
                    }
                    IabHelper.this.mSetupDone = true;
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(0, "Setup successful."));
                    }
                } catch (RemoteException e) {
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(-1001, "RemoteException while setting up in-app billing."));
                    }
                    e.printStackTrace();
                }
            }

            public void onServiceDisconnected(ComponentName componentName) {
                IabHelper.this.logDebug("Billing service disconnected.");
                IabHelper.this.mService = null;
            }
        };
        Intent intent = new Intent(GooglePlay.VENDING_ACTION);
        intent.setPackage("com.android.vending");
        List queryIntentServices = this.mContext.getPackageManager().queryIntentServices(intent, 0);
        if (queryIntentServices != null && !queryIntentServices.isEmpty()) {
            this.mContext.bindService(intent, this.mServiceConn, 1);
        } else if (onIabSetupFinishedListener != null) {
            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(3, "Billing service unavailable on device."));
        }
    }

    public boolean subscriptionsSupported() {
        return this.mSubscriptionsSupported;
    }
}
