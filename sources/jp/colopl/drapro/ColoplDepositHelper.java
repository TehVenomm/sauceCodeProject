package p018jp.colopl.drapro;

import android.content.SharedPreferences;
import android.text.TextUtils;
import android.util.Log;
import com.facebook.share.internal.MessengerShareContentUtility;
import java.util.ArrayList;
import java.util.Map;
import java.util.Map.Entry;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;
import p018jp.colopl.iab.Purchase;
import p018jp.colopl.network.HttpPostAsyncTask;
import p018jp.colopl.network.HttpRequestListener;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.ColoplDepositHelper */
public class ColoplDepositHelper {
    private static final String DEPO_PREF_NAME = "depohelper";
    private static final String PURCHASE_JSON_KEY_ITEMTYPE = "itemType";
    private static final String PURCHASE_JSON_KEY_ORGJSON = "orgjson";
    private static final String PURCHASE_JSON_KEY_SIGNATURE = "signature";
    private final String TAG = "ColoplDeposit";
    /* access modifiers changed from: private */
    public StartActivity mActivity;
    private SharedPreferences mDepositedPurchase = null;

    /* renamed from: jp.colopl.drapro.ColoplDepositHelper$PostDepositFinishedListener */
    public interface PostDepositFinishedListener {
        void onPostDepositFinished(PostDepositResult postDepositResult);
    }

    /* renamed from: jp.colopl.drapro.ColoplDepositHelper$PostDepositResult */
    public class PostDepositResult {
        int errorMessage;
        int errorTitle;
        Purchase purchase;
        String purchasedSku;
        String resultData;
        int statusCode;
        boolean successFlag;

        public PostDepositResult() {
        }

        public int getErrorMessage() {
            return this.errorMessage;
        }

        public int getErrorTitle() {
            return this.errorTitle;
        }

        public Purchase getPurchase() {
            return this.purchase;
        }

        public String getPurchasedSku() {
            return this.purchasedSku;
        }

        public String getResultData() {
            return this.resultData;
        }

        public int getStatusCode() {
            return this.statusCode;
        }

        public boolean getSuccess() {
            return this.successFlag;
        }

        public boolean isAlreadyCancelled() {
            return this.statusCode == 401;
        }

        public boolean isValidStatusCode() {
            return this.statusCode == 100 || this.statusCode == 300 || this.statusCode == 203;
        }

        public void setErrorMessage(int i) {
            this.errorMessage = i;
        }

        public void setErrorTitle(int i) {
            this.errorTitle = i;
        }

        public void setPurchase(Purchase purchase2) {
            this.purchase = purchase2;
        }

        public void setPurchasedSku(String str) {
            this.purchasedSku = str;
        }

        public void setResultData(String str) {
            this.resultData = str;
        }

        public void setStatusCode(int i) {
            this.statusCode = i;
        }

        public void setSuccess(boolean z) {
            this.successFlag = z;
        }
    }

    /* renamed from: jp.colopl.drapro.ColoplDepositHelper$PrepareDepositFinishedListener */
    public interface PrepareDepositFinishedListener {
        void onPrepareDepositFinished(PrepareResult prepareResult);
    }

    /* renamed from: jp.colopl.drapro.ColoplDepositHelper$PrepareResult */
    public class PrepareResult {
        int errorMessage;
        int errorTitle;
        String itemId;
        String payload;
        int statusCode;
        boolean successFlag;

        public PrepareResult() {
        }

        public PrepareResult(boolean z, int i, String str, String str2) {
            this.successFlag = z;
            this.statusCode = i;
            this.itemId = str;
            this.payload = str2;
        }

        public int getErrorMessage() {
            return this.errorMessage;
        }

        public int getErrorTitle() {
            return this.errorTitle;
        }

        public String getItemId() {
            return this.itemId;
        }

        public String getPayload() {
            return this.payload;
        }

        public int getStatusCode() {
            return this.statusCode;
        }

        public boolean getSuccess() {
            return this.successFlag;
        }

        public boolean isValidStatusCode() {
            return this.statusCode == 100;
        }

        public void setErrorMessage(int i) {
            this.errorMessage = i;
        }

        public void setErrorTitle(int i) {
            this.errorTitle = i;
        }

        public void setItemId(String str) {
            this.itemId = str;
        }

        public void setPayload(String str) {
            this.payload = str;
        }

        public void setStatusCode(int i) {
            this.statusCode = i;
        }

        public void setSuccess(boolean z) {
            this.successFlag = z;
        }
    }

    public ColoplDepositHelper(StartActivity startActivity) {
        this.mActivity = startActivity;
        this.mDepositedPurchase = startActivity.getSharedPreferences(DEPO_PREF_NAME, 0);
    }

    public static String getJsonStringFromPurchase(Purchase purchase) {
        String originalJson = purchase.getOriginalJson();
        String itemType = purchase.getItemType();
        String signature = purchase.getSignature();
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("itemType", itemType);
            jSONObject.put(PURCHASE_JSON_KEY_SIGNATURE, signature);
            jSONObject.put(PURCHASE_JSON_KEY_ORGJSON, originalJson);
            return jSONObject.toString();
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }

    public static Purchase getPurchaseFromJsonString(String str) {
        try {
            JSONObject jSONObject = new JSONObject(str);
            return new Purchase(jSONObject.getString("itemType"), jSONObject.getString(PURCHASE_JSON_KEY_ORGJSON), jSONObject.getString(PURCHASE_JSON_KEY_SIGNATURE));
        } catch (JSONException e) {
            e.printStackTrace();
            return null;
        }
    }

    public boolean addUndepositedPurchase(Purchase purchase) {
        if (purchase == null) {
            return false;
        }
        String orderId = purchase.getOrderId();
        String jsonStringFromPurchase = getJsonStringFromPurchase(purchase);
        if (TextUtils.isEmpty(orderId) || TextUtils.isEmpty(jsonStringFromPurchase)) {
            return false;
        }
        return this.mDepositedPurchase.edit().putString(orderId, jsonStringFromPurchase).commit();
    }

    public ArrayList<Purchase> getUndepositedPurchase() {
        Map all = this.mDepositedPurchase.getAll();
        ArrayList<Purchase> arrayList = new ArrayList<>();
        for (Entry entry : all.entrySet()) {
            Purchase purchaseFromJsonString = getPurchaseFromJsonString((String) entry.getValue());
            if (purchaseFromJsonString == null) {
                removeUndepositedPurchaseByOrderId((String) entry.getKey());
            } else {
                arrayList.add(purchaseFromJsonString);
            }
        }
        return arrayList;
    }

    public void postDepositAsync(final Purchase purchase, final PostDepositFinishedListener postDepositFinishedListener) {
        ArrayList arrayList = new ArrayList(3);
        arrayList.add(new BasicNameValuePair("mainToken", this.mActivity.getConfig().getSession().getSid()));
        arrayList.add(new BasicNameValuePair("signedData", purchase.getOriginalJson()));
        arrayList.add(new BasicNameValuePair(PURCHASE_JSON_KEY_SIGNATURE, purchase.getSignature()));
        arrayList.add(new BasicNameValuePair("iabver", "3"));
        arrayList.add(new BasicNameValuePair("apv", String.valueOf(this.mActivity.getConfig().getVersionCode())));
        Util.dLog("ColoplDeposit", "[IABV3] " + purchase + "  signature=" + purchase.getSignature() + " signedData=" + purchase.getOriginalJson() + " apv=" + String.valueOf(this.mActivity.getConfig().getVersionCode()) + "mainToken=" + this.mActivity.getConfig().getSession().getSid());
        AnalyticsHelper.trackPageView("/depo/post");
        HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(this.mActivity, NetworkHelper.getItemShopDepositUrl(), arrayList);
        httpPostAsyncTask.setListener(new HttpRequestListener() {
            public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exc) {
                Log.e("ColoplDeposit", "[IABV3] postDepositAsync.onReceiveError : " + exc.getMessage());
                PostDepositResult postDepositResult = new PostDepositResult();
                postDepositResult.setSuccess(false);
                postDepositResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                postDepositResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                if (postDepositFinishedListener != null) {
                    postDepositFinishedListener.onPostDepositFinished(postDepositResult);
                }
            }

            public void onReceiveResponse(HttpPostAsyncTask httpPostAsyncTask, String str) {
                Util.dLog("ColoplDeposit", "[IABV3] postDepositAsync.onReceiveResponse : " + str);
                PostDepositResult postDepositResult = new PostDepositResult();
                postDepositResult.setPurchase(purchase);
                postDepositResult.setSuccess(true);
                postDepositResult.setPurchasedSku(purchase.getSku());
                try {
                    JSONObject jSONObject = new JSONObject(str);
                    postDepositResult.setStatusCode(jSONObject.getInt("code"));
                    postDepositResult.setResultData(jSONObject.toString());
                    Util.dLog("ColoplDeposit", "[IABV3] postDepositAsync json response : " + jSONObject.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                    postDepositResult.setSuccess(false);
                    postDepositResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    postDepositResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                }
                if (postDepositResult.isValidStatusCode()) {
                    ColoplDepositHelper.this.removeUndepositedPurchase(purchase);
                    postDepositResult.setSuccess(true);
                } else if (postDepositResult.isAlreadyCancelled()) {
                    ColoplDepositHelper.this.removeUndepositedPurchase(purchase);
                    postDepositResult.setSuccess(true);
                } else {
                    postDepositResult.setSuccess(false);
                    postDepositResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    postDepositResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                }
                if (postDepositFinishedListener != null) {
                    postDepositFinishedListener.onPostDepositFinished(postDepositResult);
                }
            }
        });
        httpPostAsyncTask.execute(new Void[0]);
    }

    public void prepareDepositAsync(String str, final PrepareDepositFinishedListener prepareDepositFinishedListener) {
        final String lowerCase = str.toLowerCase();
        String packageName = this.mActivity.getPackageName();
        final String valueOf = String.valueOf(System.currentTimeMillis());
        if (lowerCase.startsWith(packageName + ".g.")) {
            valueOf = valueOf + ":" + InAppBillingHelper.userId;
        }
        ArrayList arrayList = new ArrayList(3);
        arrayList.add(new BasicNameValuePair("mainToken", this.mActivity.getConfig().getSession().getSid()));
        arrayList.add(new BasicNameValuePair(MessengerShareContentUtility.ATTACHMENT_PAYLOAD, valueOf));
        arrayList.add(new BasicNameValuePair("itemId", lowerCase));
        String itemShopRequestUrl = NetworkHelper.getItemShopRequestUrl();
        Log.i("ColoplDeposit", "[IABV3] prepareDepositAsync, requesting url: " + itemShopRequestUrl + ", mainToken: " + this.mActivity.getConfig().getSession().getSid() + ", payload: " + valueOf + ", itemId: " + lowerCase);
        AnalyticsHelper.trackPageView("/depo/pre");
        HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(this.mActivity, itemShopRequestUrl, arrayList);
        httpPostAsyncTask.setListener(new HttpRequestListener() {
            public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exc) {
                Log.e("ColoplDeposit", "[IABV3] prepareDepositAsync.onReceiveError : " + exc.getMessage());
                PrepareResult prepareResult = new PrepareResult();
                prepareResult.setSuccess(false);
                prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("network_error", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("network_error_occurred", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                if (prepareDepositFinishedListener != null) {
                    prepareDepositFinishedListener.onPrepareDepositFinished(prepareResult);
                }
            }

            /* JADX WARNING: Removed duplicated region for block: B:10:0x0065  */
            /* JADX WARNING: Removed duplicated region for block: B:14:0x00cc  */
            /* JADX WARNING: Removed duplicated region for block: B:17:? A[RETURN, SYNTHETIC] */
            /* JADX WARNING: Removed duplicated region for block: B:7:0x0054  */
            /* Code decompiled incorrectly, please refer to instructions dump. */
            public void onReceiveResponse(p018jp.colopl.network.HttpPostAsyncTask r10, java.lang.String r11) {
                /*
                    r9 = this;
                    r8 = 1
                    r7 = 0
                    r2 = 0
                    java.lang.String r0 = "ColoplDeposit"
                    java.lang.StringBuilder r1 = new java.lang.StringBuilder
                    r1.<init>()
                    java.lang.String r3 = "[IABV3] prepareDepositAsync.onReceiveResponse : "
                    java.lang.StringBuilder r1 = r1.append(r3)
                    java.lang.StringBuilder r1 = r1.append(r11)
                    java.lang.String r1 = r1.toString()
                    p018jp.colopl.util.Util.dLog(r0, r1)
                    jp.colopl.drapro.ColoplDepositHelper$PrepareResult r3 = new jp.colopl.drapro.ColoplDepositHelper$PrepareResult
                    jp.colopl.drapro.ColoplDepositHelper r0 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    r3.<init>()
                    r3.setSuccess(r8)
                    org.json.JSONObject r0 = new org.json.JSONObject     // Catch:{ JSONException -> 0x006b }
                    r0.<init>(r11)     // Catch:{ JSONException -> 0x006b }
                    java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ JSONException -> 0x006b }
                    r1.<init>()     // Catch:{ JSONException -> 0x006b }
                    r4 = 0
                    java.lang.String r5 = "json string: "
                    java.lang.StringBuilder r1 = r1.append(r5)     // Catch:{ JSONException -> 0x006b }
                    java.lang.String r5 = r0.toString()     // Catch:{ JSONException -> 0x006b }
                    java.lang.StringBuilder r1 = r1.append(r5)     // Catch:{ JSONException -> 0x006b }
                    java.lang.String r1 = r1.toString()     // Catch:{ JSONException -> 0x006b }
                    p018jp.colopl.util.Util.dLog(r4, r1)     // Catch:{ JSONException -> 0x006b }
                    java.lang.String r1 = "code"
                    int r0 = r0.getInt(r1)     // Catch:{ JSONException -> 0x006b }
                    r3.setStatusCode(r0)     // Catch:{ JSONException -> 0x0125 }
                L_0x004e:
                    boolean r1 = r3.isValidStatusCode()
                    if (r1 == 0) goto L_0x00cc
                    r3.setSuccess(r8)
                    java.lang.String r0 = r1
                    r3.setItemId(r0)
                    java.lang.String r0 = r0
                    r3.setPayload(r0)
                L_0x0061:
                    jp.colopl.drapro.ColoplDepositHelper$PrepareDepositFinishedListener r0 = r9
                    if (r0 == 0) goto L_0x006a
                    jp.colopl.drapro.ColoplDepositHelper$PrepareDepositFinishedListener r0 = r9
                    r0.onPrepareDepositFinished(r3)
                L_0x006a:
                    return
                L_0x006b:
                    r1 = move-exception
                    r0 = r2
                L_0x006d:
                    java.lang.StringBuilder r4 = new java.lang.StringBuilder
                    r4.<init>()
                    java.lang.String r5 = "[IABV3] onReceiveResponse json recieve error! "
                    java.lang.StringBuilder r4 = r4.append(r5)
                    java.lang.String r5 = r1.getMessage()
                    java.lang.StringBuilder r4 = r4.append(r5)
                    java.lang.String r4 = r4.toString()
                    p018jp.colopl.util.Util.dLog(r7, r4)
                    r1.printStackTrace()
                    r3.setSuccess(r2)
                    jp.colopl.drapro.ColoplDepositHelper r1 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r1 = r1.mActivity
                    android.content.res.Resources r1 = r1.getResources()
                    java.lang.String r4 = "payment_server_error_title"
                    java.lang.String r5 = "string"
                    jp.colopl.drapro.ColoplDepositHelper r6 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r6 = r6.mActivity
                    java.lang.String r6 = r6.getPackageName()
                    int r1 = r1.getIdentifier(r4, r5, r6)
                    r3.setErrorTitle(r1)
                    jp.colopl.drapro.ColoplDepositHelper r1 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r1 = r1.mActivity
                    android.content.res.Resources r1 = r1.getResources()
                    java.lang.String r4 = "payment_server_error_title"
                    java.lang.String r5 = "string"
                    jp.colopl.drapro.ColoplDepositHelper r6 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r6 = r6.mActivity
                    java.lang.String r6 = r6.getPackageName()
                    int r1 = r1.getIdentifier(r4, r5, r6)
                    r3.setErrorMessage(r1)
                    goto L_0x004e
                L_0x00cc:
                    java.lang.StringBuilder r1 = new java.lang.StringBuilder
                    r1.<init>()
                    java.lang.String r4 = "[IABV3] onReceiveResponse statusCode not 100 error! "
                    java.lang.StringBuilder r1 = r1.append(r4)
                    java.lang.StringBuilder r0 = r1.append(r0)
                    java.lang.String r0 = r0.toString()
                    android.util.Log.e(r7, r0)
                    r3.setSuccess(r2)
                    jp.colopl.drapro.ColoplDepositHelper r0 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r0 = r0.mActivity
                    android.content.res.Resources r0 = r0.getResources()
                    java.lang.String r1 = "payment_server_error_title"
                    java.lang.String r2 = "string"
                    jp.colopl.drapro.ColoplDepositHelper r4 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r4 = r4.mActivity
                    java.lang.String r4 = r4.getPackageName()
                    int r0 = r0.getIdentifier(r1, r2, r4)
                    r3.setErrorTitle(r0)
                    jp.colopl.drapro.ColoplDepositHelper r0 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r0 = r0.mActivity
                    android.content.res.Resources r0 = r0.getResources()
                    java.lang.String r1 = "payment_server_error_message"
                    java.lang.String r2 = "string"
                    jp.colopl.drapro.ColoplDepositHelper r4 = p018jp.colopl.drapro.ColoplDepositHelper.this
                    jp.colopl.drapro.StartActivity r4 = r4.mActivity
                    java.lang.String r4 = r4.getPackageName()
                    int r0 = r0.getIdentifier(r1, r2, r4)
                    r3.setErrorMessage(r0)
                    goto L_0x0061
                L_0x0125:
                    r1 = move-exception
                    goto L_0x006d
                */
                throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.drapro.ColoplDepositHelper.C12961.onReceiveResponse(jp.colopl.network.HttpPostAsyncTask, java.lang.String):void");
            }
        });
        httpPostAsyncTask.execute(new Void[0]);
    }

    public boolean removeUndepositedPurchase(Purchase purchase) {
        if (purchase == null) {
            return false;
        }
        String orderId = purchase.getOrderId();
        if (!TextUtils.isEmpty(orderId)) {
            return removeUndepositedPurchaseByOrderId(orderId);
        }
        return false;
    }

    public boolean removeUndepositedPurchaseByOrderId(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        return this.mDepositedPurchase.edit().remove(str).commit();
    }
}
