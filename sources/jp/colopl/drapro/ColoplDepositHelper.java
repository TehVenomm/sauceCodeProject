package jp.colopl.drapro;

import android.content.SharedPreferences;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.nearby.messages.Strategy;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import jp.colopl.iab.Purchase;
import jp.colopl.network.HttpPostAsyncTask;
import jp.colopl.network.HttpRequestListener;
import jp.colopl.util.Util;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

public class ColoplDepositHelper {
    private static final String DEPO_PREF_NAME = "depohelper";
    private static final String PURCHASE_JSON_KEY_ITEMTYPE = "itemType";
    private static final String PURCHASE_JSON_KEY_ORGJSON = "orgjson";
    private static final String PURCHASE_JSON_KEY_SIGNATURE = "signature";
    private final String TAG = "ColoplDeposit";
    private StartActivity mActivity;
    private SharedPreferences mDepositedPurchase = null;

    public interface PostDepositFinishedListener {
        void onPostDepositFinished(PostDepositResult postDepositResult);
    }

    public class PostDepositResult {
        int errorMessage;
        int errorTitle;
        Purchase purchase;
        String purchasedSku;
        String resultData;
        int statusCode;
        boolean successFlag;

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
            return this.statusCode == 100 || this.statusCode == Strategy.TTL_SECONDS_DEFAULT || this.statusCode == 203;
        }

        public void setErrorMessage(int i) {
            this.errorMessage = i;
        }

        public void setErrorTitle(int i) {
            this.errorTitle = i;
        }

        public void setPurchase(Purchase purchase) {
            this.purchase = purchase;
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

    public interface PrepareDepositFinishedListener {
        void onPrepareDepositFinished(PrepareResult prepareResult);
    }

    public class PrepareResult {
        int errorMessage;
        int errorTitle;
        String itemId;
        String payload;
        int statusCode;
        boolean successFlag;

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
        Object orderId = purchase.getOrderId();
        Object jsonStringFromPurchase = getJsonStringFromPurchase(purchase);
        return (TextUtils.isEmpty(orderId) || TextUtils.isEmpty(jsonStringFromPurchase)) ? false : this.mDepositedPurchase.edit().putString(orderId, jsonStringFromPurchase).commit();
    }

    public ArrayList<Purchase> getUndepositedPurchase() {
        Map all = this.mDepositedPurchase.getAll();
        ArrayList<Purchase> arrayList = new ArrayList();
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
        List arrayList = new ArrayList(3);
        arrayList.add(new BasicNameValuePair("mainToken", this.mActivity.getConfig().getSession().getSid()));
        arrayList.add(new BasicNameValuePair("signedData", purchase.getOriginalJson()));
        arrayList.add(new BasicNameValuePair(PURCHASE_JSON_KEY_SIGNATURE, purchase.getSignature()));
        arrayList.add(new BasicNameValuePair("iabver", "3"));
        arrayList.add(new BasicNameValuePair("apv", String.valueOf(this.mActivity.getConfig().getVersionCode())));
        Util.dLog("ColoplDeposit", "[IABV3] " + purchase + "  signature=" + purchase.getSignature() + " signedData=" + purchase.getOriginalJson() + " apv=" + String.valueOf(this.mActivity.getConfig().getVersionCode()) + "mainToken=" + this.mActivity.getConfig().getSession().getSid());
        AnalyticsHelper.trackPageView("/depo/post");
        HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(this.mActivity, NetworkHelper.getItemShopDepositUrl(), arrayList);
        httpPostAsyncTask.setListener(new HttpRequestListener() {
            public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exception) {
                Log.e("ColoplDeposit", "[IABV3] postDepositAsync.onReceiveError : " + exception.getMessage());
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
        final String toLowerCase = str.toLowerCase();
        String packageName = this.mActivity.getPackageName();
        String valueOf = String.valueOf(System.currentTimeMillis());
        if (toLowerCase.startsWith(packageName + ".g.")) {
            valueOf = valueOf + ":" + InAppBillingHelper.userId;
        }
        List arrayList = new ArrayList(3);
        arrayList.add(new BasicNameValuePair("mainToken", this.mActivity.getConfig().getSession().getSid()));
        arrayList.add(new BasicNameValuePair("payload", valueOf));
        arrayList.add(new BasicNameValuePair("itemId", toLowerCase));
        String itemShopRequestUrl = NetworkHelper.getItemShopRequestUrl();
        Log.i("ColoplDeposit", "[IABV3] prepareDepositAsync, requesting url: " + itemShopRequestUrl + ", mainToken: " + this.mActivity.getConfig().getSession().getSid() + ", payload: " + valueOf + ", itemId: " + toLowerCase);
        AnalyticsHelper.trackPageView("/depo/pre");
        HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(this.mActivity, itemShopRequestUrl, arrayList);
        httpPostAsyncTask.setListener(new HttpRequestListener() {
            public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exception) {
                Log.e("ColoplDeposit", "[IABV3] prepareDepositAsync.onReceiveError : " + exception.getMessage());
                PrepareResult prepareResult = new PrepareResult();
                prepareResult.setSuccess(false);
                prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("network_error", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("network_error_occurred", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                if (prepareDepositFinishedListener != null) {
                    prepareDepositFinishedListener.onPrepareDepositFinished(prepareResult);
                }
            }

            public void onReceiveResponse(HttpPostAsyncTask httpPostAsyncTask, String str) {
                int i;
                JSONException e;
                Util.dLog("ColoplDeposit", "[IABV3] prepareDepositAsync.onReceiveResponse : " + str);
                PrepareResult prepareResult = new PrepareResult();
                prepareResult.setSuccess(true);
                try {
                    JSONObject jSONObject = new JSONObject(str);
                    Util.dLog(null, "json string: " + jSONObject.toString());
                    i = jSONObject.getInt("code");
                    try {
                        prepareResult.setStatusCode(i);
                    } catch (JSONException e2) {
                        e = e2;
                        Util.dLog(null, "[IABV3] onReceiveResponse json recieve error! " + e.getMessage());
                        e.printStackTrace();
                        prepareResult.setSuccess(false);
                        prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                        prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                        if (prepareResult.isValidStatusCode()) {
                            Log.e(null, "[IABV3] onReceiveResponse statusCode not 100 error! " + i);
                            prepareResult.setSuccess(false);
                            prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                            prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                        } else {
                            prepareResult.setSuccess(true);
                            prepareResult.setItemId(toLowerCase);
                            prepareResult.setPayload(valueOf);
                        }
                        if (prepareDepositFinishedListener == null) {
                            prepareDepositFinishedListener.onPrepareDepositFinished(prepareResult);
                        }
                    }
                } catch (JSONException e3) {
                    e = e3;
                    i = 0;
                    Util.dLog(null, "[IABV3] onReceiveResponse json recieve error! " + e.getMessage());
                    e.printStackTrace();
                    prepareResult.setSuccess(false);
                    prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    if (prepareResult.isValidStatusCode()) {
                        prepareResult.setSuccess(true);
                        prepareResult.setItemId(toLowerCase);
                        prepareResult.setPayload(valueOf);
                    } else {
                        Log.e(null, "[IABV3] onReceiveResponse statusCode not 100 error! " + i);
                        prepareResult.setSuccess(false);
                        prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                        prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    }
                    if (prepareDepositFinishedListener == null) {
                        prepareDepositFinishedListener.onPrepareDepositFinished(prepareResult);
                    }
                }
                if (prepareResult.isValidStatusCode()) {
                    prepareResult.setSuccess(true);
                    prepareResult.setItemId(toLowerCase);
                    prepareResult.setPayload(valueOf);
                } else {
                    Log.e(null, "[IABV3] onReceiveResponse statusCode not 100 error! " + i);
                    prepareResult.setSuccess(false);
                    prepareResult.setErrorTitle(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_title", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                    prepareResult.setErrorMessage(ColoplDepositHelper.this.mActivity.getResources().getIdentifier("payment_server_error_message", "string", ColoplDepositHelper.this.mActivity.getPackageName()));
                }
                if (prepareDepositFinishedListener == null) {
                    prepareDepositFinishedListener.onPrepareDepositFinished(prepareResult);
                }
            }
        });
        httpPostAsyncTask.execute(new Void[0]);
    }

    public boolean removeUndepositedPurchase(Purchase purchase) {
        if (purchase == null) {
            return false;
        }
        Object orderId = purchase.getOrderId();
        return !TextUtils.isEmpty(orderId) ? removeUndepositedPurchaseByOrderId(orderId) : false;
    }

    public boolean removeUndepositedPurchaseByOrderId(String str) {
        return TextUtils.isEmpty(str) ? false : this.mDepositedPurchase.edit().remove(str).commit();
    }
}
