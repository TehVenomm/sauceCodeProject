package org.onepf.oms.appstore.googleUtils;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.RemoteException;
import com.android.vending.billing.IInAppBillingService;
import com.android.vending.billing.IInAppBillingService.Stub;
import com.appsflyer.share.Constants;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.onepf.oms.Appstore;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.GooglePlay;
import org.onepf.oms.util.Logger;

public class IabHelper implements AppstoreInAppBillingService {
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
    public static final int QUERY_SKU_DETAILS_BATCH_SIZE = 20;
    public static final String RESPONSE_BUY_INTENT = "BUY_INTENT";
    public static final String RESPONSE_CODE = "RESPONSE_CODE";
    public static final String RESPONSE_GET_SKU_DETAILS_LIST = "DETAILS_LIST";
    public static final String RESPONSE_INAPP_ITEM_LIST = "INAPP_PURCHASE_ITEM_LIST";
    public static final String RESPONSE_INAPP_PURCHASE_DATA = "INAPP_PURCHASE_DATA";
    public static final String RESPONSE_INAPP_PURCHASE_DATA_LIST = "INAPP_PURCHASE_DATA_LIST";
    public static final String RESPONSE_INAPP_SIGNATURE = "INAPP_DATA_SIGNATURE";
    public static final String RESPONSE_INAPP_SIGNATURE_LIST = "INAPP_DATA_SIGNATURE_LIST";
    private Appstore appstore;
    ComponentName componentName;
    boolean mAsyncInProgress = false;
    String mAsyncOperation = "";
    Context mContext;
    @Nullable
    OnIabPurchaseFinishedListener mPurchaseListener;
    String mPurchasingItemType;
    int mRequestCode;
    @Nullable
    IInAppBillingService mService;
    @Nullable
    ServiceConnection mServiceConn;
    boolean mSetupDone = false;
    @Nullable
    String mSignatureBase64 = null;
    boolean mSubscriptionsSupported = false;

    public interface OnIabSetupFinishedListener {
        void onIabSetupFinished(IabResult iabResult);
    }

    public interface OnConsumeFinishedListener {
        void onConsumeFinished(Purchase purchase, IabResult iabResult);
    }

    public interface OnConsumeMultiFinishedListener {
        void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2);
    }

    public interface OnIabPurchaseFinishedListener {
        void onIabPurchaseFinished(IabResult iabResult, Purchase purchase);
    }

    public interface QueryInventoryFinishedListener {
        void onQueryInventoryFinished(IabResult iabResult, Inventory inventory);
    }

    public IabHelper(@NotNull Context context, String str, Appstore appstore) {
        this.mContext = context.getApplicationContext();
        this.mSignatureBase64 = str;
        this.appstore = appstore;
        Logger.m1000d("IAB helper created.");
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

    void checkSetupDone(String str) {
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        checkSetupDone("consume");
        if (purchase.mItemType.equals("inapp")) {
            try {
                String token = purchase.getToken();
                String sku = purchase.getSku();
                if (token == null || token.equals("")) {
                    Logger.m1005e("In-app billing error: Can't consume ", sku, ". No token.");
                    throw new IabException(-1007, "PurchaseInfo is missing token for sku: " + sku + " " + purchase);
                }
                Logger.m1001d("Consuming sku: ", sku, ", token: ", token);
                if (this.mService == null) {
                    Logger.m1001d("Error consuming consuming sku ", sku, ". Service is not connected.");
                    throw new IabException(6, "Error consuming sku " + sku);
                }
                int consumePurchase = this.mService.consumePurchase(3, getPackageName(), token);
                if (consumePurchase == 0) {
                    Logger.m1001d("Successfully consumed sku: ", sku);
                    return;
                }
                Logger.m1001d("Error consuming consuming sku ", sku, ". ", getResponseDesc(consumePurchase));
                throw new IabException(consumePurchase, "Error consuming sku " + sku);
            } catch (Exception e) {
                throw new IabException(-1001, "Remote exception while consuming. PurchaseInfo: " + purchase, e);
            }
        }
        throw new IabException(-1010, "Items of type '" + purchase.mItemType + "' can't be consumed.");
    }

    public void consumeAsync(@NotNull List<Purchase> list, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        consumeAsyncInternal(list, null, onConsumeMultiFinishedListener);
    }

    public void consumeAsync(Purchase purchase, OnConsumeFinishedListener onConsumeFinishedListener) {
        checkSetupDone("consume");
        List arrayList = new ArrayList();
        arrayList.add(purchase);
        consumeAsyncInternal(arrayList, onConsumeFinishedListener, null);
    }

    void consumeAsyncInternal(@NotNull List<Purchase> list, @Nullable OnConsumeFinishedListener onConsumeFinishedListener, @Nullable OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        final Handler handler = new Handler();
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
                    } catch (Throwable e) {
                        Logger.m1003e("consume(Purchase) failed.", e);
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
        Logger.m1000d("Disposing.");
        this.mSetupDone = false;
        if (this.mServiceConn != null) {
            Logger.m1000d("Unbinding from service.");
            if (this.mContext != null) {
                this.mContext.unbindService(this.mServiceConn);
            }
            this.mServiceConn = null;
            this.mService = null;
            this.mPurchaseListener = null;
        }
    }

    void flagEndAsync() {
        Logger.m1001d("Ending async operation: ", this.mAsyncOperation);
        this.mAsyncOperation = "";
        this.mAsyncInProgress = false;
    }

    void flagStartAsync(String str) {
        if (this.mAsyncInProgress) {
            throw new IllegalStateException("Can't start async operation (" + str + ") because another async operation(" + this.mAsyncOperation + ") is in progress.");
        }
        this.mAsyncOperation = str;
        this.mAsyncInProgress = true;
        Logger.m1001d("Starting async operation: ", str);
    }

    public String getPackageName() {
        return this.mContext.getPackageName();
    }

    int getResponseCodeFromBundle(@NotNull Bundle bundle) {
        Object obj = bundle.get("RESPONSE_CODE");
        if (obj == null) {
            Logger.m1000d("Bundle with null response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            Logger.m1005e("In-app billing error: ", "Unexpected type for bundle response code.");
            Logger.m1005e("In-app billing error: ", obj.getClass().getName());
            throw new RuntimeException("Unexpected type for bundle response code: " + obj.getClass().getName());
        }
    }

    int getResponseCodeFromIntent(@NotNull Intent intent) {
        Object obj = intent.getExtras().get("RESPONSE_CODE");
        if (obj == null) {
            Logger.m1002e("In-app billing error: Intent with no response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            Logger.m1002e("In-app billing error: Unexpected type for intent response code.");
            Logger.m1005e("In-app billing error: ", obj.getClass().getName());
            throw new RuntimeException("Unexpected type for intent response code: " + obj.getClass().getName());
        }
    }

    @Nullable
    protected IInAppBillingService getServiceFromBinder(IBinder iBinder) {
        return Stub.asInterface(iBinder);
    }

    protected Intent getServiceIntent() {
        Intent intent = new Intent(GooglePlay.VENDING_ACTION);
        intent.setPackage("com.android.vending");
        return intent;
    }

    public boolean handleActivityResult(int i, int i2, @Nullable Intent intent) {
        if (i != this.mRequestCode) {
            return false;
        }
        checkSetupDone("handleActivityResult");
        flagEndAsync();
        IabResult iabResult;
        if (intent == null) {
            Logger.m1002e("In-app billing error: Null data in IAB activity result.");
            iabResult = new IabResult(-1002, "Null data in IAB result");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            return true;
        }
        int responseCodeFromIntent = getResponseCodeFromIntent(intent);
        String stringExtra = intent.getStringExtra("INAPP_PURCHASE_DATA");
        String stringExtra2 = intent.getStringExtra("INAPP_DATA_SIGNATURE");
        if (i2 == -1 && responseCodeFromIntent == 0) {
            Logger.m1000d("Purchase successful.");
            processPurchaseSuccess(intent, stringExtra, stringExtra2);
            return true;
        } else if (i2 == -1) {
            Logger.m1001d("Purchase canceled - Response: ", getResponseDesc(responseCodeFromIntent));
            processPurchaseFail(responseCodeFromIntent);
            return true;
        } else if (i2 == 0) {
            Logger.m1001d("Purchase canceled - Response: ", getResponseDesc(responseCodeFromIntent));
            iabResult = new IabResult(-1005, "User canceled.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            return true;
        } else {
            Logger.m1002e("In-app billing error: Purchase failed. Result code: " + Integer.toString(i2) + ". Response: " + getResponseDesc(responseCodeFromIntent));
            iabResult = new IabResult(-1006, "Unknown purchase response.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            return true;
        }
    }

    boolean isValidDataSignature(@Nullable String str, @NotNull String str2, @NotNull String str3) {
        if (str == null) {
            return true;
        }
        boolean verifyPurchase = Security.verifyPurchase(str, str2, str3);
        if (verifyPurchase) {
            return verifyPurchase;
        }
        Logger.m1009w("In-app billing warning: Purchase signature verification **FAILED**.");
        return verifyPurchase;
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "inapp", i, onIabPurchaseFinishedListener, str2);
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, @NotNull String str2, int i, @Nullable OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        IabResult iabResult;
        checkSetupDone("launchPurchaseFlow");
        flagStartAsync("launchPurchaseFlow");
        IabResult iabResult2;
        if (!str2.equals("subs") || this.mSubscriptionsSupported) {
            try {
                Logger.m1001d("Constructing buy intent for ", str, ", item type: ", str2);
                if (this.mService == null) {
                    iabResult2 = new IabResult(6, "Unable to buy item");
                    Logger.m1002e("In-app billing error: Unable to buy item, Error response: service is not connected.");
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult2, null);
                    }
                    flagEndAsync();
                    return;
                }
                Bundle buyIntent = this.mService.getBuyIntent(3, getPackageName(), str, str2, str3);
                int responseCodeFromBundle = getResponseCodeFromBundle(buyIntent);
                if (responseCodeFromBundle != 0) {
                    iabResult2 = new IabResult(responseCodeFromBundle, "Unable to buy item");
                    Logger.m1002e("In-app billing error: Unable to buy item, Error response: " + getResponseDesc(responseCodeFromBundle));
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult2, null);
                    }
                    flagEndAsync();
                    return;
                }
                PendingIntent pendingIntent = (PendingIntent) buyIntent.getParcelable("BUY_INTENT");
                Logger.m1001d("Launching buy intent for ", str, ". Request code: ", Integer.valueOf(i));
                this.mRequestCode = i;
                this.mPurchaseListener = onIabPurchaseFinishedListener;
                this.mPurchasingItemType = str2;
                activity.startIntentSenderForResult(pendingIntent.getIntentSender(), i, new Intent(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue());
                flagEndAsync();
            } catch (Throwable e) {
                iabResult = new IabResult(-1004, "Failed to send intent.");
                Logger.m1003e("In-app billing error: SendIntentException while launching purchase flow for sku " + str, e);
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                }
            } catch (Throwable e2) {
                iabResult = new IabResult(-1001, "Remote exception while starting purchase flow");
                Logger.m1003e("In-app billing error: RemoteException while launching purchase flow for sku " + str, e2);
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                }
            }
        } else {
            iabResult2 = new IabResult(-1009, "Subscriptions are not available.");
            Logger.m1000d("Subscriptions are not available.");
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult2, null);
            }
            flagEndAsync();
        }
    }

    public void launchSubscriptionPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchSubscriptionPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchSubscriptionPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "subs", i, onIabPurchaseFinishedListener, str2);
    }

    public void processPurchaseFail(int i) {
        Logger.m1001d("Result code was OK but in-app billing response was not OK: ", getResponseDesc(i));
        if (this.mPurchaseListener != null) {
            this.mPurchaseListener.onIabPurchaseFinished(new IabResult(i, "Problem purchashing item."), null);
        }
    }

    public void processPurchaseSuccess(@NotNull Intent intent, @Nullable String str, @Nullable String str2) {
        IabResult iabResult;
        Logger.m1000d("Successful resultcode from purchase activity.");
        Logger.m1001d("Purchase data: ", str);
        Logger.m1001d("Data signature: ", str2);
        Logger.m1001d("Extras: ", intent.getExtras());
        Logger.m1001d("Expected item type: ", this.mPurchasingItemType);
        if (str == null || str2 == null) {
            Logger.m1002e("In-app billing error: BUG: either purchaseData or dataSignature is null.");
            Logger.m1001d("Extras: ", intent.getExtras());
            iabResult = new IabResult(-1008, "IAB returned null purchaseData or dataSignature");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
                return;
            }
            return;
        }
        try {
            Purchase purchase = new Purchase(this.mPurchasingItemType, str, str2, this.appstore.getAppstoreName());
            String sku = purchase.getSku();
            purchase.setSku(SkuManager.getInstance().getSku(this.appstore.getAppstoreName(), sku));
            if (isValidDataSignature(this.mSignatureBase64, str, str2)) {
                Logger.m1000d("Purchase signature successfully verified.");
                if (this.mPurchaseListener != null) {
                    this.mPurchaseListener.onIabPurchaseFinished(new IabResult(0, "Success"), purchase);
                    return;
                }
                return;
            }
            Logger.m1002e("In-app billing error: Purchase signature verification FAILED for sku " + sku);
            IabResult iabResult2 = new IabResult(-1003, "Signature verification failed for sku " + sku);
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult2, purchase);
            }
        } catch (JSONException e) {
            Logger.m1002e("In-app billing error: Failed to parse purchase data.");
            e.printStackTrace();
            iabResult = new IabResult(-1002, "Failed to parse purchase data.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult, null);
            }
        }
    }

    @Nullable
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
                    queryPurchases = querySkuDetails("subs", inventory, list2);
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

    public void queryInventoryAsync(@NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(true, null, queryInventoryFinishedListener);
    }

    public void queryInventoryAsync(boolean z, List<String> list, @NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        final Handler handler = new Handler();
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
                } catch (Throwable e) {
                    Logger.m1003e("queryInventory() failed.", e);
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

    public void queryInventoryAsync(boolean z, @NotNull QueryInventoryFinishedListener queryInventoryFinishedListener) {
        queryInventoryAsync(z, null, queryInventoryFinishedListener);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    int queryPurchases(@org.jetbrains.annotations.NotNull org.onepf.oms.appstore.googleUtils.Inventory r13, java.lang.String r14) throws org.json.JSONException, android.os.RemoteException {
        /*
        r12 = this;
        r0 = 2;
        r0 = new java.lang.Object[r0];
        r1 = 0;
        r2 = "Querying owned items, item type: ";
        r0[r1] = r2;
        r1 = 1;
        r0[r1] = r14;
        org.onepf.oms.util.Logger.m1001d(r0);
        r0 = 2;
        r0 = new java.lang.Object[r0];
        r1 = 0;
        r2 = "Package name: ";
        r0[r1] = r2;
        r1 = 1;
        r2 = r12.getPackageName();
        r0[r1] = r2;
        org.onepf.oms.util.Logger.m1001d(r0);
        r3 = 0;
        r0 = 0;
        r1 = r0;
        r0 = r3;
    L_0x0024:
        r2 = 2;
        r2 = new java.lang.Object[r2];
        r3 = 0;
        r4 = "Calling getPurchases with continuation token: ";
        r2[r3] = r4;
        r3 = 1;
        r2[r3] = r1;
        org.onepf.oms.util.Logger.m1001d(r2);
        r2 = r12.mService;
        if (r2 != 0) goto L_0x003d;
    L_0x0036:
        r0 = "getPurchases() failed: service is not connected.";
        org.onepf.oms.util.Logger.m1000d(r0);
        r0 = 6;
    L_0x003c:
        return r0;
    L_0x003d:
        r2 = r12.mService;
        r3 = 3;
        r4 = r12.getPackageName();
        r5 = r2.getPurchases(r3, r4, r14, r1);
        r1 = r12.getResponseCodeFromBundle(r5);
        r2 = 2;
        r2 = new java.lang.Object[r2];
        r3 = 0;
        r4 = "Owned items response: ";
        r2[r3] = r4;
        r3 = 1;
        r4 = java.lang.Integer.valueOf(r1);
        r2[r3] = r4;
        org.onepf.oms.util.Logger.m1001d(r2);
        if (r1 == 0) goto L_0x0074;
    L_0x0060:
        r0 = 2;
        r0 = new java.lang.Object[r0];
        r2 = 0;
        r3 = "getPurchases() failed: ";
        r0[r2] = r3;
        r2 = 1;
        r3 = getResponseDesc(r1);
        r0[r2] = r3;
        org.onepf.oms.util.Logger.m1001d(r0);
        r0 = r1;
        goto L_0x003c;
    L_0x0074:
        r1 = "INAPP_PURCHASE_ITEM_LIST";
        r1 = r5.containsKey(r1);
        if (r1 == 0) goto L_0x008c;
    L_0x007c:
        r1 = "INAPP_PURCHASE_DATA_LIST";
        r1 = r5.containsKey(r1);
        if (r1 == 0) goto L_0x008c;
    L_0x0084:
        r1 = "INAPP_DATA_SIGNATURE_LIST";
        r1 = r5.containsKey(r1);
        if (r1 != 0) goto L_0x0094;
    L_0x008c:
        r0 = "In-app billing error: Bundle returned from getPurchases() doesn't contain required fields.";
        org.onepf.oms.util.Logger.m1002e(r0);
        r0 = -1002; // 0xfffffffffffffc16 float:NaN double:NaN;
        goto L_0x003c;
    L_0x0094:
        r1 = "INAPP_PURCHASE_ITEM_LIST";
        r6 = r5.getStringArrayList(r1);
        r1 = "INAPP_PURCHASE_DATA_LIST";
        r7 = r5.getStringArrayList(r1);
        r1 = "INAPP_DATA_SIGNATURE_LIST";
        r8 = r5.getStringArrayList(r1);
        r1 = 0;
        r3 = r0;
        r4 = r1;
    L_0x00a9:
        r0 = r7.size();
        if (r4 >= r0) goto L_0x0140;
    L_0x00af:
        r0 = r7.get(r4);
        r0 = (java.lang.String) r0;
        r1 = r8.get(r4);
        r1 = (java.lang.String) r1;
        r2 = r6.get(r4);
        r2 = (java.lang.String) r2;
        r9 = r12.mSignatureBase64;
        r9 = r12.isValidDataSignature(r9, r0, r1);
        if (r9 == 0) goto L_0x011d;
    L_0x00c9:
        r9 = 2;
        r9 = new java.lang.Object[r9];
        r10 = 0;
        r11 = "Sku is owned: ";
        r9[r10] = r11;
        r10 = 1;
        r9[r10] = r2;
        org.onepf.oms.util.Logger.m1001d(r9);
        r2 = new org.onepf.oms.appstore.googleUtils.Purchase;
        r9 = r12.appstore;
        r9 = r9.getAppstoreName();
        r2.<init>(r14, r0, r1, r9);
        r1 = r2.getSku();
        r9 = org.onepf.oms.SkuManager.getInstance();
        r10 = r12.appstore;
        r10 = r10.getAppstoreName();
        r1 = r9.getSku(r10, r1);
        r2.setSku(r1);
        r1 = r2.getToken();
        r1 = android.text.TextUtils.isEmpty(r1);
        if (r1 == 0) goto L_0x0114;
    L_0x0101:
        r1 = "In-app billing warning: BUG: empty/null token!";
        org.onepf.oms.util.Logger.m1009w(r1);
        r1 = 2;
        r1 = new java.lang.Object[r1];
        r9 = 0;
        r10 = "Purchase data: ";
        r1[r9] = r10;
        r9 = 1;
        r1[r9] = r0;
        org.onepf.oms.util.Logger.m1001d(r1);
    L_0x0114:
        r13.addPurchase(r2);
        r0 = r3;
    L_0x0118:
        r1 = r4 + 1;
        r3 = r0;
        r4 = r1;
        goto L_0x00a9;
    L_0x011d:
        r2 = "In-app billing warning: Purchase signature verification **FAILED**. Not adding item.";
        org.onepf.oms.util.Logger.m1009w(r2);
        r2 = 2;
        r2 = new java.lang.Object[r2];
        r3 = 0;
        r9 = "   Purchase data: ";
        r2[r3] = r9;
        r3 = 1;
        r2[r3] = r0;
        org.onepf.oms.util.Logger.m1001d(r2);
        r0 = 2;
        r0 = new java.lang.Object[r0];
        r2 = 0;
        r3 = "   Signature: ";
        r0[r2] = r3;
        r2 = 1;
        r0[r2] = r1;
        org.onepf.oms.util.Logger.m1001d(r0);
        r0 = 1;
        goto L_0x0118;
    L_0x0140:
        r0 = "INAPP_CONTINUATION_TOKEN";
        r0 = r5.getString(r0);
        r1 = 2;
        r1 = new java.lang.Object[r1];
        r2 = 0;
        r4 = "Continuation token: ";
        r1[r2] = r4;
        r2 = 1;
        r1[r2] = r0;
        org.onepf.oms.util.Logger.m1001d(r1);
        r1 = android.text.TextUtils.isEmpty(r0);
        if (r1 == 0) goto L_0x0163;
    L_0x015a:
        if (r3 == 0) goto L_0x0160;
    L_0x015c:
        r0 = -1003; // 0xfffffffffffffc15 float:NaN double:NaN;
        goto L_0x003c;
    L_0x0160:
        r0 = 0;
        goto L_0x003c;
    L_0x0163:
        r1 = r0;
        r0 = r3;
        goto L_0x0024;
        */
        throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.googleUtils.IabHelper.queryPurchases(org.onepf.oms.appstore.googleUtils.Inventory, java.lang.String):int");
    }

    int querySkuDetails(String str, @NotNull Inventory inventory, @Nullable List<String> list) throws RemoteException, JSONException {
        Iterator it;
        Logger.m1000d("querySkuDetails() Querying SKU details.");
        SkuManager instance = SkuManager.getInstance();
        String appstoreName = this.appstore.getAppstoreName();
        Set<String> treeSet = new TreeSet();
        for (String storeSku : inventory.getAllOwnedSkus(str)) {
            treeSet.add(instance.getStoreSku(appstoreName, storeSku));
        }
        if (list != null) {
            for (String storeSku2 : list) {
                treeSet.add(instance.getStoreSku(appstoreName, storeSku2));
            }
        }
        if (treeSet.isEmpty()) {
            Logger.m1000d("querySkuDetails(): nothing to do because there are no SKUs.");
            return 0;
        }
        ArrayList arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList(20);
        int i = 0;
        ArrayList arrayList3 = arrayList2;
        for (String storeSku22 : treeSet) {
            arrayList3.add(storeSku22);
            int i2 = i + 1;
            if (arrayList3.size() == 20 || i2 == treeSet.size()) {
                arrayList.add(arrayList3);
                arrayList3 = new ArrayList(20);
                i = i2;
            } else {
                i = i2;
            }
        }
        Logger.m1001d("querySkuDetails() batches: ", Integer.valueOf(arrayList.size()), ", ", arrayList);
        Iterator it2 = arrayList.iterator();
        while (it2.hasNext()) {
            arrayList2 = (ArrayList) it2.next();
            Bundle bundle = new Bundle();
            bundle.putStringArrayList("ITEM_ID_LIST", arrayList2);
            if (this.mService == null) {
                Logger.m1002e("In-app billing error: unable to get sku details: service is not connected.");
                return -1002;
            }
            Bundle skuDetails = this.mService.getSkuDetails(3, this.mContext.getPackageName(), str, bundle);
            if (skuDetails.containsKey("DETAILS_LIST")) {
                it = skuDetails.getStringArrayList("DETAILS_LIST").iterator();
                while (it.hasNext()) {
                    SkuDetails skuDetails2 = new SkuDetails(str, (String) it.next());
                    skuDetails2.setSku(SkuManager.getInstance().getSku(appstoreName, skuDetails2.getSku()));
                    Logger.m1001d("querySkuDetails() Got sku details: ", skuDetails2);
                    inventory.addSkuDetails(skuDetails2);
                }
            } else {
                i2 = getResponseCodeFromBundle(skuDetails);
                if (i2 != 0) {
                    Logger.m1001d("getSkuDetails() failed: ", getResponseDesc(i2));
                    return i2;
                }
                Logger.m1002e("In-app billing error: getSkuDetails() returned a bundle with neither an error nor a detail list.");
                return -1002;
            }
        }
        return 0;
    }

    public void setSetupDone(boolean z) {
        this.mSetupDone = z;
    }

    public void setSubscriptionsSupported(boolean z) {
        this.mSubscriptionsSupported = z;
    }

    public void startSetup(@Nullable final OnIabSetupFinishedListener onIabSetupFinishedListener) {
        if (this.mSetupDone) {
            throw new IllegalStateException("IAB helper is already set up.");
        }
        Logger.m1000d("Starting in-app billing setup.");
        this.mServiceConn = new ServiceConnection() {
            public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                Logger.m1000d("Billing service connected.");
                IabHelper.this.mService = IabHelper.this.getServiceFromBinder(iBinder);
                IabHelper.this.componentName = componentName;
                String packageName = IabHelper.this.mContext.getPackageName();
                try {
                    Logger.m1000d("Checking for in-app billing 3 support.");
                    int isBillingSupported = IabHelper.this.mService.isBillingSupported(3, packageName, "inapp");
                    if (isBillingSupported != 0) {
                        if (onIabSetupFinishedListener != null) {
                            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(isBillingSupported, "Error checking for billing v3 support."));
                        }
                        IabHelper.this.mSubscriptionsSupported = false;
                        return;
                    }
                    Logger.m1001d("In-app billing version 3 supported for ", packageName);
                    if (IabHelper.this.mService.isBillingSupported(3, packageName, "subs") == 0) {
                        Logger.m1000d("Subscriptions AVAILABLE.");
                        IabHelper.this.mSubscriptionsSupported = true;
                    } else {
                        Logger.m1001d("Subscriptions NOT AVAILABLE. Response: ", Integer.valueOf(IabHelper.this.mService.isBillingSupported(3, packageName, "subs")));
                    }
                    IabHelper.this.mSetupDone = true;
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(0, "Setup successful."));
                        Logger.m1000d("Setup successful.");
                    }
                } catch (Throwable e) {
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(-1001, "RemoteException while setting up in-app billing."));
                    }
                    Logger.m1003e("RemoteException while setting up in-app billing", e);
                }
            }

            public void onServiceDisconnected(ComponentName componentName) {
                Logger.m1000d("Billing service disconnected.");
                IabHelper.this.mService = null;
            }
        };
        Intent serviceIntent = getServiceIntent();
        List queryIntentServices = this.mContext.getPackageManager().queryIntentServices(serviceIntent, 0);
        if (queryIntentServices != null && !queryIntentServices.isEmpty()) {
            this.mContext.bindService(serviceIntent, this.mServiceConn, 1);
        } else if (onIabSetupFinishedListener != null) {
            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(3, "Billing service unavailable on device."));
            Logger.m1000d("Billing service unavailable on device.");
        }
    }

    public boolean subscriptionsSupported() {
        return this.mSubscriptionsSupported;
    }
}
