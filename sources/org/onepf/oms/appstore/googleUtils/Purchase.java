package org.onepf.oms.appstore.googleUtils;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class Purchase implements Cloneable {
    @Nullable
    String appstoreName;
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

    public Purchase(@Nullable String str) {
        if (str == null) {
            throw new IllegalArgumentException("appstoreName must be defined");
        }
        this.appstoreName = str;
    }

    public Purchase(String str, String str2, String str3, String str4) throws JSONException {
        this.appstoreName = str4;
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

    public Object clone() {
        try {
            return super.clone();
        } catch (CloneNotSupportedException e) {
            throw new IllegalStateException("Somebody forgot to add Cloneable to class", e);
        }
    }

    @Nullable
    public String getAppstoreName() {
        return this.appstoreName;
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

    public void setDeveloperPayload(String str) {
        this.mDeveloperPayload = str;
    }

    public void setItemType(String str) {
        this.mItemType = str;
    }

    public void setOrderId(String str) {
        this.mOrderId = str;
    }

    public void setOriginalJson(String str) {
        this.mOriginalJson = str;
    }

    public void setPackageName(String str) {
        this.mPackageName = str;
    }

    public void setPurchaseState(int i) {
        this.mPurchaseState = i;
    }

    public void setPurchaseTime(long j) {
        this.mPurchaseTime = j;
    }

    public void setSku(String str) {
        this.mSku = str;
    }

    public void setToken(String str) {
        this.mToken = str;
    }

    @NotNull
    public String toString() {
        return "PurchaseInfo(type:" + this.mItemType + "): " + "{\"orderId\":" + this.mOrderId + ",\"packageName\":" + this.mPackageName + ",\"productId\":" + this.mSku + ",\"purchaseTime\":" + this.mPurchaseTime + ",\"purchaseState\":" + this.mPurchaseState + ",\"developerPayload\":" + this.mDeveloperPayload + ",\"token\":" + this.mToken + "}";
    }
}
