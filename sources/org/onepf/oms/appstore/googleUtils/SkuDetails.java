package org.onepf.oms.appstore.googleUtils;

import com.google.firebase.analytics.FirebaseAnalytics.Param;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class SkuDetails {
    String mDescription;
    String mItemType;
    String mJson;
    String mPrice;
    String mSku;
    String mTitle;
    String mType;

    public SkuDetails(String str) throws JSONException {
        this("inapp", str);
    }

    public SkuDetails(String str, String str2) throws JSONException {
        this.mItemType = str;
        this.mJson = str2;
        JSONObject jSONObject = new JSONObject(this.mJson);
        this.mSku = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
        this.mType = jSONObject.optString("type");
        this.mPrice = jSONObject.optString(Param.PRICE);
        this.mTitle = jSONObject.optString("title");
        this.mDescription = jSONObject.optString("description");
    }

    public SkuDetails(String str, String str2, String str3) {
        this.mItemType = "inapp";
        this.mSku = str;
        this.mTitle = str2;
        this.mPrice = str3;
    }

    public SkuDetails(String str, String str2, String str3, String str4, String str5) {
        this.mItemType = str;
        this.mSku = str2;
        this.mTitle = str3;
        this.mPrice = str4;
        this.mDescription = str5;
    }

    public String getDescription() {
        return this.mDescription;
    }

    public String getItemType() {
        return this.mItemType;
    }

    public String getJson() {
        return this.mJson;
    }

    public String getPrice() {
        return this.mPrice;
    }

    public String getSku() {
        return this.mSku;
    }

    public String getTitle() {
        return this.mTitle;
    }

    public String getType() {
        return this.mType;
    }

    public void setSku(String str) {
        this.mSku = str;
    }

    public String toString() {
        return String.format("SkuDetails: type = %s, SKU = %s, title = %s, price = %s, description = %s", new Object[]{this.mItemType, this.mSku, this.mTitle, this.mPrice, this.mDescription});
    }
}
