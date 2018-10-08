package jp.colopl.iab;

import com.facebook.share.internal.ShareConstants;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class SkuDetails {
    String mCurrency;
    String mDescription;
    String mItemType;
    String mJson;
    String mPrice;
    double mPriceMicros;
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
        this.mType = jSONObject.optString(ShareConstants.MEDIA_TYPE);
        this.mPrice = jSONObject.optString(Param.PRICE);
        this.mPriceMicros = jSONObject.optDouble("price_amount_micros", 0.0d);
        this.mCurrency = jSONObject.optString("price_currency_code");
        this.mTitle = jSONObject.optString("title");
        this.mDescription = jSONObject.optString("description");
    }

    public String getCurrency() {
        return this.mCurrency;
    }

    public String getDescription() {
        return this.mDescription;
    }

    public String getPrice() {
        return this.mPrice;
    }

    public double getPriceMicros() {
        return this.mPriceMicros;
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

    public String toString() {
        return "SkuDetails:" + this.mJson;
    }
}
