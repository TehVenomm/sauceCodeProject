package org.onepf.oms.appstore.googleUtils;

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
import android.os.RemoteException;
import android.text.TextUtils;
import com.android.vending.billing.IInAppBillingService;
import com.android.vending.billing.IInAppBillingService.Stub;
import com.appsflyer.share.Constants;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
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

    public interface OnIabPurchaseFinishedListener {
        void onIabPurchaseFinished(IabResult iabResult, Purchase purchase);
    }

    public interface OnConsumeFinishedListener {
        void onConsumeFinished(Purchase purchase, IabResult iabResult);
    }

    public interface OnConsumeMultiFinishedListener {
        void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2);
    }

    public interface OnIabSetupFinishedListener {
        void onIabSetupFinished(IabResult iabResult);
    }

    public interface QueryInventoryFinishedListener {
        void onQueryInventoryFinished(IabResult iabResult, Inventory inventory);
    }

    public IabHelper(@NotNull Context context, String str, Appstore appstore2) {
        this.mContext = context.getApplicationContext();
        this.mSignatureBase64 = str;
        this.appstore = appstore2;
        Logger.m1025d("IAB helper created.");
    }

    public static String getResponseDesc(int i) {
        String[] split = "0:OK/1:User Canceled/2:Unknown/3:Billing Unavailable/4:Item unavailable/5:Developer Error/6:Error/7:Item Already Owned/8:Item not owned".split(Constants.URL_PATH_DELIMITER);
        String[] split2 = "0:OK/-1001:Remote exception during initialization/-1002:Bad response received/-1003:Purchase signature verification failed/-1004:Send intent failed/-1005:User cancelled/-1006:Unknown purchase response/-1007:Missing token/-1008:Unknown error/-1009:Subscriptions not available/-1010:Invalid consumption attempt".split(Constants.URL_PATH_DELIMITER);
        if (i > -1000) {
            return (i < 0 || i >= split.length) ? String.valueOf(i) + ":Unknown" : split[i];
        }
        int i2 = -1000 - i;
        return (i2 < 0 || i2 >= split2.length) ? String.valueOf(i) + ":Unknown IAB Helper Error" : split2[i2];
    }

    /* access modifiers changed from: 0000 */
    public void checkSetupDone(String str) {
    }

    public void consume(@NotNull Purchase purchase) throws IabException {
        checkSetupDone("consume");
        if (!purchase.mItemType.equals("inapp")) {
            throw new IabException(-1010, "Items of type '" + purchase.mItemType + "' can't be consumed.");
        }
        try {
            String token = purchase.getToken();
            String sku = purchase.getSku();
            if (token == null || token.equals("")) {
                Logger.m1030e("In-app billing error: Can't consume ", sku, ". No token.");
                throw new IabException(-1007, "PurchaseInfo is missing token for sku: " + sku + " " + purchase);
            }
            Logger.m1026d("Consuming sku: ", sku, ", token: ", token);
            if (this.mService == null) {
                Logger.m1026d("Error consuming consuming sku ", sku, ". Service is not connected.");
                throw new IabException(6, "Error consuming sku " + sku);
            }
            int consumePurchase = this.mService.consumePurchase(3, getPackageName(), token);
            if (consumePurchase == 0) {
                Logger.m1026d("Successfully consumed sku: ", sku);
                return;
            }
            Logger.m1026d("Error consuming consuming sku ", sku, ". ", getResponseDesc(consumePurchase));
            throw new IabException(consumePurchase, "Error consuming sku " + sku);
        } catch (RemoteException e) {
            throw new IabException(-1001, "Remote exception while consuming. PurchaseInfo: " + purchase, e);
        }
    }

    public void consumeAsync(@NotNull List<Purchase> list, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        checkSetupDone("consume");
        consumeAsyncInternal(list, null, onConsumeMultiFinishedListener);
    }

    public void consumeAsync(Purchase purchase, OnConsumeFinishedListener onConsumeFinishedListener) {
        checkSetupDone("consume");
        ArrayList arrayList = new ArrayList();
        arrayList.add(purchase);
        consumeAsyncInternal(arrayList, onConsumeFinishedListener, null);
    }

    /* access modifiers changed from: 0000 */
    public void consumeAsyncInternal(@NotNull List<Purchase> list, @Nullable OnConsumeFinishedListener onConsumeFinishedListener, @Nullable OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        final Handler handler = new Handler();
        flagStartAsync("consume");
        final List<Purchase> list2 = list;
        final OnConsumeFinishedListener onConsumeFinishedListener2 = onConsumeFinishedListener;
        final OnConsumeMultiFinishedListener onConsumeMultiFinishedListener2 = onConsumeMultiFinishedListener;
        new Thread(new Runnable() {
            public void run() {
                final ArrayList arrayList = new ArrayList();
                for (Purchase purchase : list2) {
                    try {
                        IabHelper.this.consume(purchase);
                        arrayList.add(new IabResult(0, "Successful consume of sku " + purchase.getSku()));
                    } catch (IabException e) {
                        Logger.m1028e("consume(Purchase) failed.", (Throwable) e);
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
        Logger.m1025d("Disposing.");
        this.mSetupDone = false;
        if (this.mServiceConn != null) {
            Logger.m1025d("Unbinding from service.");
            if (this.mContext != null) {
                this.mContext.unbindService(this.mServiceConn);
            }
            this.mServiceConn = null;
            this.mService = null;
            this.mPurchaseListener = null;
        }
    }

    /* access modifiers changed from: 0000 */
    public void flagEndAsync() {
        Logger.m1026d("Ending async operation: ", this.mAsyncOperation);
        this.mAsyncOperation = "";
        this.mAsyncInProgress = false;
    }

    /* access modifiers changed from: 0000 */
    public void flagStartAsync(String str) {
        if (this.mAsyncInProgress) {
            throw new IllegalStateException("Can't start async operation (" + str + ") because another async operation(" + this.mAsyncOperation + ") is in progress.");
        }
        this.mAsyncOperation = str;
        this.mAsyncInProgress = true;
        Logger.m1026d("Starting async operation: ", str);
    }

    public String getPackageName() {
        return this.mContext.getPackageName();
    }

    /* access modifiers changed from: 0000 */
    public int getResponseCodeFromBundle(@NotNull Bundle bundle) {
        Object obj = bundle.get("RESPONSE_CODE");
        if (obj == null) {
            Logger.m1025d("Bundle with null response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            Logger.m1030e("In-app billing error: ", "Unexpected type for bundle response code.");
            Logger.m1030e("In-app billing error: ", obj.getClass().getName());
            throw new RuntimeException("Unexpected type for bundle response code: " + obj.getClass().getName());
        }
    }

    /* access modifiers changed from: 0000 */
    public int getResponseCodeFromIntent(@NotNull Intent intent) {
        Object obj = intent.getExtras().get("RESPONSE_CODE");
        if (obj == null) {
            Logger.m1027e("In-app billing error: Intent with no response code, assuming OK (known issue)");
            return 0;
        } else if (obj instanceof Integer) {
            return ((Integer) obj).intValue();
        } else {
            if (obj instanceof Long) {
                return (int) ((Long) obj).longValue();
            }
            Logger.m1027e("In-app billing error: Unexpected type for intent response code.");
            Logger.m1030e("In-app billing error: ", obj.getClass().getName());
            throw new RuntimeException("Unexpected type for intent response code: " + obj.getClass().getName());
        }
    }

    /* access modifiers changed from: protected */
    @Nullable
    public IInAppBillingService getServiceFromBinder(IBinder iBinder) {
        return Stub.asInterface(iBinder);
    }

    /* access modifiers changed from: protected */
    public Intent getServiceIntent() {
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
        if (intent == null) {
            Logger.m1027e("In-app billing error: Null data in IAB activity result.");
            IabResult iabResult = new IabResult(-1002, "Null data in IAB result");
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
            Logger.m1025d("Purchase successful.");
            processPurchaseSuccess(intent, stringExtra, stringExtra2);
            return true;
        } else if (i2 == -1) {
            Logger.m1026d("Purchase canceled - Response: ", getResponseDesc(responseCodeFromIntent));
            processPurchaseFail(responseCodeFromIntent);
            return true;
        } else if (i2 == 0) {
            Logger.m1026d("Purchase canceled - Response: ", getResponseDesc(responseCodeFromIntent));
            IabResult iabResult2 = new IabResult(-1005, "User canceled.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(iabResult2, null);
            return true;
        } else {
            Logger.m1027e("In-app billing error: Purchase failed. Result code: " + Integer.toString(i2) + ". Response: " + getResponseDesc(responseCodeFromIntent));
            IabResult iabResult3 = new IabResult(-1006, "Unknown purchase response.");
            if (this.mPurchaseListener == null) {
                return true;
            }
            this.mPurchaseListener.onIabPurchaseFinished(iabResult3, null);
            return true;
        }
    }

    /* access modifiers changed from: 0000 */
    public boolean isValidDataSignature(@Nullable String str, @NotNull String str2, @NotNull String str3) {
        if (str == null) {
            return true;
        }
        boolean verifyPurchase = Security.verifyPurchase(str, str2, str3);
        if (verifyPurchase) {
            return verifyPurchase;
        }
        Logger.m1034w("In-app billing warning: Purchase signature verification **FAILED**.");
        return verifyPurchase;
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener) {
        launchPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "");
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, "inapp", i, onIabPurchaseFinishedListener, str2);
    }

    public void launchPurchaseFlow(@NotNull Activity activity, String str, @NotNull String str2, int i, @Nullable OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        checkSetupDone("launchPurchaseFlow");
        flagStartAsync("launchPurchaseFlow");
        if (!str2.equals("subs") || this.mSubscriptionsSupported) {
            try {
                Logger.m1026d("Constructing buy intent for ", str, ", item type: ", str2);
                if (this.mService == null) {
                    IabResult iabResult = new IabResult(6, "Unable to buy item");
                    Logger.m1027e("In-app billing error: Unable to buy item, Error response: service is not connected.");
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                    }
                    flagEndAsync();
                    return;
                }
                Bundle buyIntent = this.mService.getBuyIntent(3, getPackageName(), str, str2, str3);
                int responseCodeFromBundle = getResponseCodeFromBundle(buyIntent);
                if (responseCodeFromBundle != 0) {
                    IabResult iabResult2 = new IabResult(responseCodeFromBundle, "Unable to buy item");
                    Logger.m1027e("In-app billing error: Unable to buy item, Error response: " + getResponseDesc(responseCodeFromBundle));
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult2, null);
                    }
                    flagEndAsync();
                    return;
                }
                PendingIntent pendingIntent = (PendingIntent) buyIntent.getParcelable("BUY_INTENT");
                Logger.m1026d("Launching buy intent for ", str, ". Request code: ", Integer.valueOf(i));
                this.mRequestCode = i;
                this.mPurchaseListener = onIabPurchaseFinishedListener;
                this.mPurchasingItemType = str2;
                activity.startIntentSenderForResult(pendingIntent.getIntentSender(), i, new Intent(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue());
                flagEndAsync();
            } catch (SendIntentException e) {
                IabResult iabResult3 = new IabResult(-1004, "Failed to send intent.");
                Logger.m1028e("In-app billing error: SendIntentException while launching purchase flow for sku " + str, (Throwable) e);
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult3, null);
                }
            } catch (RemoteException e2) {
                IabResult iabResult4 = new IabResult(-1001, "Remote exception while starting purchase flow");
                Logger.m1028e("In-app billing error: RemoteException while launching purchase flow for sku " + str, (Throwable) e2);
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult4, null);
                }
            }
        } else {
            IabResult iabResult5 = new IabResult(-1009, "Subscriptions are not available.");
            Logger.m1025d("Subscriptions are not available.");
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult5, null);
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
        Logger.m1026d("Result code was OK but in-app billing response was not OK: ", getResponseDesc(i));
        if (this.mPurchaseListener != null) {
            this.mPurchaseListener.onIabPurchaseFinished(new IabResult(i, "Problem purchashing item."), null);
        }
    }

    public void processPurchaseSuccess(@NotNull Intent intent, @Nullable String str, @Nullable String str2) {
        Logger.m1025d("Successful resultcode from purchase activity.");
        Logger.m1026d("Purchase data: ", str);
        Logger.m1026d("Data signature: ", str2);
        Logger.m1026d("Extras: ", intent.getExtras());
        Logger.m1026d("Expected item type: ", this.mPurchasingItemType);
        if (str == null || str2 == null) {
            Logger.m1027e("In-app billing error: BUG: either purchaseData or dataSignature is null.");
            Logger.m1026d("Extras: ", intent.getExtras());
            IabResult iabResult = new IabResult(-1008, "IAB returned null purchaseData or dataSignature");
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
            if (!isValidDataSignature(this.mSignatureBase64, str, str2)) {
                Logger.m1027e("In-app billing error: Purchase signature verification FAILED for sku " + sku);
                IabResult iabResult2 = new IabResult(-1003, "Signature verification failed for sku " + sku);
                if (this.mPurchaseListener != null) {
                    this.mPurchaseListener.onIabPurchaseFinished(iabResult2, purchase);
                    return;
                }
                return;
            }
            Logger.m1025d("Purchase signature successfully verified.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(new IabResult(0, "Success"), purchase);
            }
        } catch (JSONException e) {
            Logger.m1027e("In-app billing error: Failed to parse purchase data.");
            e.printStackTrace();
            IabResult iabResult3 = new IabResult(-1002, "Failed to parse purchase data.");
            if (this.mPurchaseListener != null) {
                this.mPurchaseListener.onIabPurchaseFinished(iabResult3, null);
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
                int querySkuDetails = querySkuDetails("inapp", inventory, list);
                if (querySkuDetails != 0) {
                    throw new IabException(querySkuDetails, "Error refreshing inventory (querying prices of items).");
                }
            }
            if (this.mSubscriptionsSupported) {
                int queryPurchases2 = queryPurchases(inventory, "subs");
                if (queryPurchases2 != 0) {
                    throw new IabException(queryPurchases2, "Error refreshing inventory (querying owned subscriptions).");
                } else if (z) {
                    int querySkuDetails2 = querySkuDetails("subs", inventory, list2);
                    if (querySkuDetails2 != 0) {
                        throw new IabException(querySkuDetails2, "Error refreshing inventory (querying prices of subscriptions).");
                    }
                }
            }
            return inventory;
        } catch (RemoteException e) {
            throw new IabException(-1001, "Remote exception while refreshing inventory.", e);
        } catch (JSONException e2) {
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
                final IabResult iabResult = new IabResult(0, "Inventory refresh successful.");
                final Inventory inventory = null;
                try {
                    inventory = IabHelper.this.queryInventory(z2, list2);
                } catch (IabException e) {
                    Logger.m1028e("queryInventory() failed.", (Throwable) e);
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

    /* access modifiers changed from: 0000 */
    public int queryPurchases(@NotNull Inventory inventory, String str) throws JSONException, RemoteException {
        boolean z;
        boolean z2;
        Logger.m1026d("Querying owned items, item type: ", str);
        Logger.m1026d("Package name: ", getPackageName());
        boolean z3 = false;
        String str2 = null;
        while (true) {
            Logger.m1026d("Calling getPurchases with continuation token: ", str2);
            if (this.mService == null) {
                Logger.m1025d("getPurchases() failed: service is not connected.");
                return 6;
            }
            Bundle purchases = this.mService.getPurchases(3, getPackageName(), str, str2);
            int responseCodeFromBundle = getResponseCodeFromBundle(purchases);
            Logger.m1026d("Owned items response: ", Integer.valueOf(responseCodeFromBundle));
            if (responseCodeFromBundle != 0) {
                Logger.m1026d("getPurchases() failed: ", getResponseDesc(responseCodeFromBundle));
                return responseCodeFromBundle;
            } else if (!purchases.containsKey("INAPP_PURCHASE_ITEM_LIST") || !purchases.containsKey("INAPP_PURCHASE_DATA_LIST") || !purchases.containsKey("INAPP_DATA_SIGNATURE_LIST")) {
                Logger.m1027e("In-app billing error: Bundle returned from getPurchases() doesn't contain required fields.");
            } else {
                ArrayList stringArrayList = purchases.getStringArrayList("INAPP_PURCHASE_ITEM_LIST");
                ArrayList stringArrayList2 = purchases.getStringArrayList("INAPP_PURCHASE_DATA_LIST");
                ArrayList stringArrayList3 = purchases.getStringArrayList("INAPP_DATA_SIGNATURE_LIST");
                int i = 0;
                while (true) {
                    z2 = z;
                    int i2 = i;
                    if (i2 >= stringArrayList2.size()) {
                        break;
                    }
                    String str3 = (String) stringArrayList2.get(i2);
                    String str4 = (String) stringArrayList3.get(i2);
                    String str5 = (String) stringArrayList.get(i2);
                    if (isValidDataSignature(this.mSignatureBase64, str3, str4)) {
                        Logger.m1026d("Sku is owned: ", str5);
                        Purchase purchase = new Purchase(str, str3, str4, this.appstore.getAppstoreName());
                        purchase.setSku(SkuManager.getInstance().getSku(this.appstore.getAppstoreName(), purchase.getSku()));
                        if (TextUtils.isEmpty(purchase.getToken())) {
                            Logger.m1034w("In-app billing warning: BUG: empty/null token!");
                            Logger.m1026d("Purchase data: ", str3);
                        }
                        inventory.addPurchase(purchase);
                        z = z2;
                    } else {
                        Logger.m1034w("In-app billing warning: Purchase signature verification **FAILED**. Not adding item.");
                        Logger.m1026d("   Purchase data: ", str3);
                        Logger.m1026d("   Signature: ", str4);
                        z = true;
                    }
                    i = i2 + 1;
                }
                str2 = purchases.getString("INAPP_CONTINUATION_TOKEN");
                Logger.m1026d("Continuation token: ", str2);
                if (TextUtils.isEmpty(str2)) {
                    return z2 ? -1003 : 0;
                }
                z3 = z2;
            }
        }
        Logger.m1027e("In-app billing error: Bundle returned from getPurchases() doesn't contain required fields.");
        return -1002;
    }

    /* access modifiers changed from: 0000 */
    public int querySkuDetails(String str, @NotNull Inventory inventory, @Nullable List<String> list) throws RemoteException, JSONException {
        Logger.m1025d("querySkuDetails() Querying SKU details.");
        SkuManager instance = SkuManager.getInstance();
        String appstoreName = this.appstore.getAppstoreName();
        TreeSet<String> treeSet = new TreeSet<>();
        for (String storeSku : inventory.getAllOwnedSkus(str)) {
            treeSet.add(instance.getStoreSku(appstoreName, storeSku));
        }
        if (list != null) {
            for (String storeSku2 : list) {
                treeSet.add(instance.getStoreSku(appstoreName, storeSku2));
            }
        }
        if (treeSet.isEmpty()) {
            Logger.m1025d("querySkuDetails(): nothing to do because there are no SKUs.");
            return 0;
        }
        ArrayList arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList(20);
        int i = 0;
        ArrayList arrayList3 = arrayList2;
        for (String add : treeSet) {
            arrayList3.add(add);
            int i2 = i + 1;
            if (arrayList3.size() == 20 || i2 == treeSet.size()) {
                arrayList.add(arrayList3);
                arrayList3 = new ArrayList(20);
                i = i2;
            } else {
                i = i2;
            }
        }
        Logger.m1026d("querySkuDetails() batches: ", Integer.valueOf(arrayList.size()), ", ", arrayList);
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            ArrayList arrayList4 = (ArrayList) it.next();
            Bundle bundle = new Bundle();
            bundle.putStringArrayList("ITEM_ID_LIST", arrayList4);
            if (this.mService == null) {
                Logger.m1027e("In-app billing error: unable to get sku details: service is not connected.");
                return -1002;
            }
            Bundle skuDetails = this.mService.getSkuDetails(3, this.mContext.getPackageName(), str, bundle);
            if (!skuDetails.containsKey("DETAILS_LIST")) {
                int responseCodeFromBundle = getResponseCodeFromBundle(skuDetails);
                if (responseCodeFromBundle != 0) {
                    Logger.m1026d("getSkuDetails() failed: ", getResponseDesc(responseCodeFromBundle));
                    return responseCodeFromBundle;
                }
                Logger.m1027e("In-app billing error: getSkuDetails() returned a bundle with neither an error nor a detail list.");
                return -1002;
            }
            Iterator it2 = skuDetails.getStringArrayList("DETAILS_LIST").iterator();
            while (it2.hasNext()) {
                SkuDetails skuDetails2 = new SkuDetails(str, (String) it2.next());
                skuDetails2.setSku(SkuManager.getInstance().getSku(appstoreName, skuDetails2.getSku()));
                Logger.m1026d("querySkuDetails() Got sku details: ", skuDetails2);
                inventory.addSkuDetails(skuDetails2);
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
        Logger.m1025d("Starting in-app billing setup.");
        this.mServiceConn = new ServiceConnection() {
            public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                Logger.m1025d("Billing service connected.");
                IabHelper.this.mService = IabHelper.this.getServiceFromBinder(iBinder);
                IabHelper.this.componentName = componentName;
                String packageName = IabHelper.this.mContext.getPackageName();
                try {
                    Logger.m1025d("Checking for in-app billing 3 support.");
                    int isBillingSupported = IabHelper.this.mService.isBillingSupported(3, packageName, "inapp");
                    if (isBillingSupported != 0) {
                        if (onIabSetupFinishedListener != null) {
                            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(isBillingSupported, "Error checking for billing v3 support."));
                        }
                        IabHelper.this.mSubscriptionsSupported = false;
                        return;
                    }
                    Logger.m1026d("In-app billing version 3 supported for ", packageName);
                    int isBillingSupported2 = IabHelper.this.mService.isBillingSupported(3, packageName, "subs");
                    if (isBillingSupported2 == 0) {
                        Logger.m1025d("Subscriptions AVAILABLE.");
                        IabHelper.this.mSubscriptionsSupported = true;
                    } else {
                        Logger.m1026d("Subscriptions NOT AVAILABLE. Response: ", Integer.valueOf(isBillingSupported2));
                    }
                    IabHelper.this.mSetupDone = true;
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(0, "Setup successful."));
                        Logger.m1025d("Setup successful.");
                    }
                } catch (RemoteException e) {
                    if (onIabSetupFinishedListener != null) {
                        onIabSetupFinishedListener.onIabSetupFinished(new IabResult(-1001, "RemoteException while setting up in-app billing."));
                    }
                    Logger.m1028e("RemoteException while setting up in-app billing", (Throwable) e);
                }
            }

            public void onServiceDisconnected(ComponentName componentName) {
                Logger.m1025d("Billing service disconnected.");
                IabHelper.this.mService = null;
            }
        };
        Intent serviceIntent = getServiceIntent();
        List queryIntentServices = this.mContext.getPackageManager().queryIntentServices(serviceIntent, 0);
        if (queryIntentServices != null && !queryIntentServices.isEmpty()) {
            this.mContext.bindService(serviceIntent, this.mServiceConn, 1);
        } else if (onIabSetupFinishedListener != null) {
            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(3, "Billing service unavailable on device."));
            Logger.m1025d("Billing service unavailable on device.");
        }
    }

    public boolean subscriptionsSupported() {
        return this.mSubscriptionsSupported;
    }
}
