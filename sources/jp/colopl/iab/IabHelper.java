package p018jp.colopl.iab;

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
import android.text.TextUtils;
import android.util.Log;
import com.android.vending.billing.IInAppBillingService;
import com.android.vending.billing.IInAppBillingService.Stub;
import com.appsflyer.share.Constants;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import org.json.JSONException;
import org.onepf.oms.appstore.GooglePlay;

/* renamed from: jp.colopl.iab.IabHelper */
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

    /* renamed from: jp.colopl.iab.IabHelper$OnConsumeFinishedListener */
    public interface OnConsumeFinishedListener {
        void onConsumeFinished(Purchase purchase, IabResult iabResult);
    }

    /* renamed from: jp.colopl.iab.IabHelper$OnConsumeMultiFinishedListener */
    public interface OnConsumeMultiFinishedListener {
        void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2);
    }

    /* renamed from: jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener */
    public interface OnIabPurchaseFinishedListener {
        void onIabPurchaseFinished(IabResult iabResult, Purchase purchase);
    }

    /* renamed from: jp.colopl.iab.IabHelper$OnIabSetupFinishedListener */
    public interface OnIabSetupFinishedListener {
        void onIabSetupFinished(IabResult iabResult);
    }

    /* renamed from: jp.colopl.iab.IabHelper$QueryInventoryFinishedListener */
    public interface QueryInventoryFinishedListener {
        void onQueryInventoryFinished(IabResult iabResult, Inventory inventory);
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
        }
        int i2 = -1000 - i;
        return (i2 < 0 || i2 >= split2.length) ? String.valueOf(i) + ":Unknown IAB Helper Error" : split2[i2];
    }

    public boolean IsSetupDone() {
        return this.mSetupDone;
    }

    /* access modifiers changed from: 0000 */
    public void checkSetupDone(String str) {
        if (!this.mSetupDone) {
            logError("Illegal state for operation (" + str + "): IAB helper is not set up.");
            throw new IllegalStateException("IAB helper is not set up. Can't perform operation: " + str);
        }
    }

    /* access modifiers changed from: 0000 */
    public void consume(Purchase purchase) throws IabException {
        checkSetupDone("consume");
        if (!purchase.mItemType.equals("inapp")) {
            throw new IabException(-1010, "Items of type '" + purchase.mItemType + "' can't be consumed.");
        }
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
            } else {
                logDebug("Error consuming consuming sku " + sku + ". " + getResponseDesc(consumePurchase));
                throw new IabException(consumePurchase, "Error consuming sku " + sku);
            }
        } catch (RemoteException e) {
            throw new IabException(-1001, "Remote exception while consuming. PurchaseInfo: " + purchase, e);
        }
    }

    public void consumeAsync(List<Purchase> list, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
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
    public void consumeAsyncInternal(List<Purchase> list, OnConsumeFinishedListener onConsumeFinishedListener, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        final Handler handler = new Handler(Looper.getMainLooper());
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

    /* access modifiers changed from: 0000 */
    public void flagEndAsync() {
        logDebug("Ending async operation: " + this.mAsyncOperation);
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
        logDebug("Starting async operation: " + str);
    }

    /* access modifiers changed from: 0000 */
    public int getResponseCodeFromBundle(Bundle bundle) {
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

    /* access modifiers changed from: 0000 */
    public int getResponseCodeFromIntent(Intent intent) {
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

    /* JADX WARNING: Removed duplicated region for block: B:35:0x015a  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public boolean handleActivityResult(int r10, int r11, android.content.Intent r12) {
        /*
            r9 = this;
            r0 = 0
            r5 = -1
            r8 = -1002(0xfffffffffffffc16, float:NaN)
            r1 = 1
            r7 = 0
            int r2 = r9.mRequestCode
            if (r10 == r2) goto L_0x000b
        L_0x000a:
            return r0
        L_0x000b:
            java.lang.String r2 = "handleActivityResult"
            r9.checkSetupDone(r2)
            r9.flagEndAsync()
            if (r12 != 0) goto L_0x002c
            java.lang.String r0 = "Null data in IAB activity result."
            r9.logError(r0)
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            java.lang.String r2 = "Null data in IAB result"
            r0.<init>(r8, r2)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            if (r2 == 0) goto L_0x002a
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
        L_0x002a:
            r0 = r1
            goto L_0x000a
        L_0x002c:
            int r2 = r9.getResponseCodeFromIntent(r12)
            java.lang.String r3 = "INAPP_PURCHASE_DATA"
            java.lang.String r3 = r12.getStringExtra(r3)
            java.lang.String r4 = "INAPP_DATA_SIGNATURE"
            java.lang.String r4 = r12.getStringExtra(r4)
            if (r11 != r5) goto L_0x0162
            if (r2 != 0) goto L_0x0162
            java.lang.String r2 = "Successful resultcode from purchase activity."
            r9.logDebug(r2)
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r5 = "Purchase data: "
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            r9.logDebug(r2)
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r5 = "Data signature: "
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.StringBuilder r2 = r2.append(r4)
            java.lang.String r2 = r2.toString()
            r9.logDebug(r2)
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r5 = "Extras: "
            java.lang.StringBuilder r2 = r2.append(r5)
            android.os.Bundle r5 = r12.getExtras()
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.String r2 = r2.toString()
            r9.logDebug(r2)
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r5 = "Expected item type: "
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.String r5 = r9.mPurchasingItemType
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.String r2 = r2.toString()
            r9.logDebug(r2)
            if (r3 == 0) goto L_0x00a7
            if (r4 != 0) goto L_0x00df
        L_0x00a7:
            java.lang.String r0 = "BUG: either purchaseData or dataSignature is null."
            r9.logError(r0)
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.String r2 = "Extras: "
            java.lang.StringBuilder r0 = r0.append(r2)
            android.os.Bundle r2 = r12.getExtras()
            java.lang.String r2 = r2.toString()
            java.lang.StringBuilder r0 = r0.append(r2)
            java.lang.String r0 = r0.toString()
            r9.logDebug(r0)
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            r2 = -1008(0xfffffffffffffc10, float:NaN)
            java.lang.String r3 = "IAB returned null purchaseData or dataSignature"
            r0.<init>(r2, r3)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            if (r2 == 0) goto L_0x00dc
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
        L_0x00dc:
            r0 = r1
            goto L_0x000a
        L_0x00df:
            jp.colopl.iab.Purchase r2 = new jp.colopl.iab.Purchase     // Catch:{ JSONException -> 0x01fa }
            java.lang.String r5 = r9.mPurchasingItemType     // Catch:{ JSONException -> 0x01fa }
            r2.<init>(r5, r3, r4)     // Catch:{ JSONException -> 0x01fa }
            java.lang.String r5 = r2.getSku()     // Catch:{ JSONException -> 0x0146 }
            java.lang.String r6 = r9.mSignatureBase64     // Catch:{ JSONException -> 0x0146 }
            boolean r3 = p018jp.colopl.iab.Security.verifyPurchase(r6, r3, r4)     // Catch:{ JSONException -> 0x0146 }
            if (r3 != 0) goto L_0x012e
            java.lang.StringBuilder r0 = new java.lang.StringBuilder     // Catch:{ JSONException -> 0x0146 }
            r0.<init>()     // Catch:{ JSONException -> 0x0146 }
            java.lang.String r3 = "Purchase signature verification FAILED for sku "
            java.lang.StringBuilder r0 = r0.append(r3)     // Catch:{ JSONException -> 0x0146 }
            java.lang.StringBuilder r0 = r0.append(r5)     // Catch:{ JSONException -> 0x0146 }
            java.lang.String r0 = r0.toString()     // Catch:{ JSONException -> 0x0146 }
            r9.logError(r0)     // Catch:{ JSONException -> 0x0146 }
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult     // Catch:{ JSONException -> 0x0146 }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ JSONException -> 0x0146 }
            r3.<init>()     // Catch:{ JSONException -> 0x0146 }
            r4 = -1003(0xfffffffffffffc15, float:NaN)
            java.lang.String r6 = "Signature verification failed for sku "
            java.lang.StringBuilder r3 = r3.append(r6)     // Catch:{ JSONException -> 0x0146 }
            java.lang.StringBuilder r3 = r3.append(r5)     // Catch:{ JSONException -> 0x0146 }
            java.lang.String r3 = r3.toString()     // Catch:{ JSONException -> 0x0146 }
            r0.<init>(r4, r3)     // Catch:{ JSONException -> 0x0146 }
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r3 = r9.mPurchaseListener     // Catch:{ JSONException -> 0x0146 }
            if (r3 == 0) goto L_0x012b
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r3 = r9.mPurchaseListener     // Catch:{ JSONException -> 0x0146 }
            r3.onIabPurchaseFinished(r0, r2)     // Catch:{ JSONException -> 0x0146 }
        L_0x012b:
            r0 = r1
            goto L_0x000a
        L_0x012e:
            java.lang.String r3 = "Purchase signature successfully verified."
            r9.logDebug(r3)     // Catch:{ JSONException -> 0x0146 }
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r3 = r9.mPurchaseListener
            if (r3 == 0) goto L_0x0143
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r3 = r9.mPurchaseListener
            jp.colopl.iab.IabResult r4 = new jp.colopl.iab.IabResult
            java.lang.String r5 = "Success"
            r4.<init>(r0, r5)
            r3.onIabPurchaseFinished(r4, r2)
        L_0x0143:
            r0 = r1
            goto L_0x000a
        L_0x0146:
            r0 = move-exception
        L_0x0147:
            java.lang.String r2 = "Failed to parse purchase data."
            r9.logError(r2)
            r0.printStackTrace()
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            java.lang.String r2 = "Failed to parse purchase data."
            r0.<init>(r8, r2)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            if (r2 == 0) goto L_0x015f
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
        L_0x015f:
            r0 = r1
            goto L_0x000a
        L_0x0162:
            if (r11 != r5) goto L_0x018f
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.String r3 = "Result code was OK but in-app billing response was not OK: "
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r3 = getResponseDesc(r2)
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r0 = r0.toString()
            r9.logDebug(r0)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r0 = r9.mPurchaseListener
            if (r0 == 0) goto L_0x0143
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            java.lang.String r3 = "Problem purchashing item."
            r0.<init>(r2, r3)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
            goto L_0x0143
        L_0x018f:
            if (r11 != 0) goto L_0x01be
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.String r3 = "Purchase canceled - Response: "
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r2 = getResponseDesc(r2)
            java.lang.StringBuilder r0 = r0.append(r2)
            java.lang.String r0 = r0.toString()
            r9.logDebug(r0)
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            r2 = -1005(0xfffffffffffffc13, float:NaN)
            java.lang.String r3 = "User canceled."
            r0.<init>(r2, r3)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            if (r2 == 0) goto L_0x0143
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
            goto L_0x0143
        L_0x01be:
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.String r3 = "Purchase failed. Result code: "
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r3 = java.lang.Integer.toString(r11)
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r3 = ". Response: "
            java.lang.StringBuilder r0 = r0.append(r3)
            java.lang.String r2 = getResponseDesc(r2)
            java.lang.StringBuilder r0 = r0.append(r2)
            java.lang.String r0 = r0.toString()
            r9.logError(r0)
            jp.colopl.iab.IabResult r0 = new jp.colopl.iab.IabResult
            r2 = -1006(0xfffffffffffffc12, float:NaN)
            java.lang.String r3 = "Unknown purchase response."
            r0.<init>(r2, r3)
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            if (r2 == 0) goto L_0x0143
            jp.colopl.iab.IabHelper$OnIabPurchaseFinishedListener r2 = r9.mPurchaseListener
            r2.onIabPurchaseFinished(r0, r7)
            goto L_0x0143
        L_0x01fa:
            r0 = move-exception
            goto L_0x0147
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.iab.IabHelper.handleActivityResult(int, int, android.content.Intent):boolean");
    }

    public void launchPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "", str2);
    }

    public void launchPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2, String str3) {
        launchPurchaseFlow(activity, str, "inapp", i, onIabPurchaseFinishedListener, str2, str3);
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3, String str4) {
        Bundle buyIntent;
        checkSetupDone("launchPurchaseFlow");
        flagStartAsync("launchPurchaseFlow");
        if (!str2.equals("subs") || this.mSubscriptionsSupported) {
            try {
                logDebug("Constructing buy intent for " + str + ", item type: " + str2);
                Bundle bundle = new Bundle();
                if (!(str4 == null || str4.length() == 0)) {
                    bundle.putString("accountId", str4);
                }
                if (this.mV6IAPSupported) {
                    logDebug("Call mService.getBuyIntentExtraParams, hash=" + str4);
                    buyIntent = this.mService.getBuyIntentExtraParams(6, this.mContext.getPackageName(), str, str2, str3, bundle);
                } else {
                    logDebug("Call mService.getBuyIntent");
                    buyIntent = this.mService.getBuyIntent(3, this.mContext.getPackageName(), str, str2, str3);
                }
                int responseCodeFromBundle = getResponseCodeFromBundle(buyIntent);
                if (responseCodeFromBundle != 0) {
                    logError("Unable to buy item, Error response: " + getResponseDesc(responseCodeFromBundle));
                    IabResult iabResult = new IabResult(responseCodeFromBundle, "Unable to buy item");
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult, null);
                        return;
                    }
                    return;
                }
                PendingIntent pendingIntent = (PendingIntent) buyIntent.getParcelable("BUY_INTENT");
                logDebug("Launching buy intent for " + str + ". Request code: " + i);
                this.mRequestCode = i;
                this.mPurchaseListener = onIabPurchaseFinishedListener;
                this.mPurchasingItemType = str2;
                activity.startIntentSenderForResult(pendingIntent.getIntentSender(), i, new Intent(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue(), Integer.valueOf(0).intValue());
            } catch (SendIntentException e) {
                logError("SendIntentException while launching purchase flow for sku " + str);
                e.printStackTrace();
                IabResult iabResult2 = new IabResult(-1004, "Failed to send intent.");
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult2, null);
                }
            } catch (RemoteException e2) {
                logError("RemoteException while launching purchase flow for sku " + str);
                e2.printStackTrace();
                IabResult iabResult3 = new IabResult(-1001, "Remote exception while starting purchase flow");
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult3, null);
                }
            }
        } else {
            IabResult iabResult4 = new IabResult(-1009, "Subscriptions are not available.");
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(iabResult4, null);
            }
        }
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2) {
        launchSubscriptionPurchaseFlow(activity, str, i, onIabPurchaseFinishedListener, "", str2);
    }

    public void launchSubscriptionPurchaseFlow(Activity activity, String str, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str2, String str3) {
        launchPurchaseFlow(activity, str, "subs", i, onIabPurchaseFinishedListener, str2, str3);
    }

    /* access modifiers changed from: 0000 */
    public void logDebug(String str) {
        if (this.mDebugLog) {
            Log.d(this.mDebugTag, str);
        }
    }

    /* access modifiers changed from: 0000 */
    public void logError(String str) {
        Log.e(this.mDebugTag, "In-app billing error: " + str);
    }

    /* access modifiers changed from: 0000 */
    public void logWarn(String str) {
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
                    int querySkuDetails2 = querySkuDetails("subs", inventory, list);
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
                final IabResult iabResult = new IabResult(0, "Inventory refresh successful.");
                final Inventory inventory = null;
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

    /* access modifiers changed from: 0000 */
    public int queryPurchases(Inventory inventory, String str) throws JSONException, RemoteException {
        boolean z;
        logDebug("Querying owned items, item type: " + str);
        logDebug("Package name: " + this.mContext.getPackageName());
        String str2 = null;
        if (this.mService == null) {
            return -1008;
        }
        boolean z2 = false;
        while (true) {
            logDebug("Calling getPurchases with continuation token: " + str2);
            Bundle purchases = this.mService.getPurchases(3, this.mContext.getPackageName(), str, str2);
            int responseCodeFromBundle = getResponseCodeFromBundle(purchases);
            logDebug("Owned items response: " + String.valueOf(responseCodeFromBundle));
            if (responseCodeFromBundle != 0) {
                logDebug("getPurchases() failed: " + getResponseDesc(responseCodeFromBundle));
                return responseCodeFromBundle;
            } else if (!purchases.containsKey("INAPP_PURCHASE_ITEM_LIST") || !purchases.containsKey("INAPP_PURCHASE_DATA_LIST") || !purchases.containsKey("INAPP_DATA_SIGNATURE_LIST")) {
                logError("Bundle returned from getPurchases() doesn't contain required fields.");
            } else {
                ArrayList stringArrayList = purchases.getStringArrayList("INAPP_PURCHASE_ITEM_LIST");
                ArrayList stringArrayList2 = purchases.getStringArrayList("INAPP_PURCHASE_DATA_LIST");
                ArrayList stringArrayList3 = purchases.getStringArrayList("INAPP_DATA_SIGNATURE_LIST");
                boolean z3 = z2;
                for (int i = 0; i < stringArrayList2.size(); i++) {
                    String str3 = (String) stringArrayList2.get(i);
                    String str4 = (String) stringArrayList3.get(i);
                    String str5 = (String) stringArrayList.get(i);
                    if (Security.verifyPurchase(this.mSignatureBase64, str3, str4)) {
                        logDebug("Sku is owned: " + str5);
                        Purchase purchase = new Purchase(str, str3, str4);
                        if (TextUtils.isEmpty(purchase.getToken())) {
                            logWarn("BUG: empty/null token!");
                            logDebug("Purchase data: " + str3);
                        }
                        inventory.addPurchase(purchase);
                        z = z3;
                    } else {
                        logWarn("Purchase signature verification **FAILED**. Not adding item.");
                        logDebug("   Purchase data: " + str3);
                        logDebug("   Signature: " + str4);
                        z = true;
                    }
                    z3 = z;
                }
                str2 = purchases.getString("INAPP_CONTINUATION_TOKEN");
                logDebug("Continuation token: " + str2);
                if (TextUtils.isEmpty(str2)) {
                    return z3 ? -1003 : 0;
                }
                z2 = z3;
            }
        }
        logError("Bundle returned from getPurchases() doesn't contain required fields.");
        return -1002;
    }

    /* access modifiers changed from: 0000 */
    public int querySkuDetails(String str, Inventory inventory, List<String> list) throws RemoteException, JSONException {
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
            if (!skuDetails.containsKey("DETAILS_LIST")) {
                int responseCodeFromBundle = getResponseCodeFromBundle(skuDetails);
                if (responseCodeFromBundle != 0) {
                    logDebug("getSkuDetails() failed: " + getResponseDesc(responseCodeFromBundle));
                    return responseCodeFromBundle;
                }
                logError("getSkuDetails() returned a bundle with neither an error nor a detail list.");
                return -1002;
            }
            arrayList2.addAll(skuDetails.getStringArrayList("DETAILS_LIST"));
            i += 20;
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
                    } else {
                        IabHelper.this.mV6IAPSupported = true;
                    }
                    IabHelper iabHelper = IabHelper.this;
                    StringBuilder append = new StringBuilder().append("In-app billing version ");
                    if (IabHelper.this.mV6IAPSupported) {
                        i = 6;
                    }
                    iabHelper.logDebug(append.append(i).append(" supported for ").append(packageName).toString());
                    int isBillingSupported2 = IabHelper.this.mService.isBillingSupported(3, packageName, "subs");
                    if (isBillingSupported2 == 0) {
                        IabHelper.this.logDebug("Subscriptions AVAILABLE.");
                        IabHelper.this.mSubscriptionsSupported = true;
                    } else {
                        IabHelper.this.logDebug("Subscriptions NOT AVAILABLE. Response: " + isBillingSupported2);
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
