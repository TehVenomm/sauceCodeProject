package p018jp.colopl.iab;

import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: jp.colopl.iab.Purchase */
public class Purchase {
    String mDeveloperPayload;
    String mItemType;
    String mOrderId;
    String mOriginalJson;
    String mPackageName;
    int mPurchaseState;
    long mPurchaseTime;
    String mSignature;
    String mSku;
    String mToken;

    public Purchase(String str, String str2, String str3) throws JSONException {
        this.mItemType = str;
        this.mOriginalJson = str2;
        JSONObject jSONObject = new JSONObject(this.mOriginalJson);
        this.mOrderId = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID);
        this.mPackageName = jSONObject.optString("packageName");
        this.mSku = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
        this.mPurchaseTime = jSONObject.optLong("purchaseTime");
        this.mPurchaseState = jSONObject.optInt("purchaseState");
        this.mDeveloperPayload = jSONObject.optString("developerPayload");
        this.mToken = jSONObject.optString("token", jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN));
        this.mSignature = str3;
    }

    public String getDeveloperPayload() {
        return this.mDeveloperPayload;
    }

    public String getItemType() {
        return this.mItemType;
    }

    public String getOrderId() {
        return this.mOrderId;
    }

    public String getOriginalJson() {
        return this.mOriginalJson;
    }

    public String getPackageName() {
        return this.mPackageName;
    }

    public int getPurchaseState() {
        return this.mPurchaseState;
    }

    public long getPurchaseTime() {
        return this.mPurchaseTime;
    }

    public String getSignature() {
        return this.mSignature;
    }

    public String getSku() {
        return this.mSku;
    }

    public String getToken() {
        return this.mToken;
    }

    public String toLog() {
        return "mItemType:" + this.mItemType + " mPackageName: " + this.mPackageName + " mSku: " + this.mSku;
    }

    public String toString() {
        return "PurchaseInfo(type:" + this.mItemType + "):" + this.mOriginalJson;
    }
}
