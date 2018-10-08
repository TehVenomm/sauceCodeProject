package net.gogame.gowrap.support;

import org.json.JSONException;
import org.json.JSONObject;

public class InAppPurchaseData {
    private static final String AUTO_RENEWING = "autoRenewing";
    private static final String DEVELOPER_PAYLOAD = "developerPayload";
    private static final String ORDER_ID = "orderId";
    private static final String PACKAGE_NAME = "packageName";
    private static final String PRODUCT_ID = "productId";
    private static final String PURCHASE_STATUS = "purchaseStatus";
    private static final String PURCHASE_TIME = "purchaseTime";
    private static final String PURCHASE_TOKEN = "purchaseToken";
    private final Boolean autoRenewing;
    private final String developerPayload;
    private final String orderId;
    private final String packageName;
    private final String productId;
    private final Integer purchaseStatus;
    private final Long purchaseTime;
    private final String purchaseToken;

    public InAppPurchaseData(String str) throws JSONException {
        this(new JSONObject(str));
    }

    public InAppPurchaseData(JSONObject jSONObject) throws JSONException {
        if (jSONObject.has(AUTO_RENEWING)) {
            this.autoRenewing = Boolean.valueOf(jSONObject.optBoolean(AUTO_RENEWING, false));
        } else {
            this.autoRenewing = null;
        }
        this.orderId = jSONObject.optString("orderId", null);
        this.packageName = jSONObject.optString(PACKAGE_NAME, null);
        this.productId = jSONObject.optString("productId", null);
        if (jSONObject.has(PURCHASE_TIME)) {
            this.purchaseTime = Long.valueOf(jSONObject.optLong(PURCHASE_TIME));
        } else {
            this.purchaseTime = null;
        }
        if (jSONObject.has("purchaseStatus")) {
            this.purchaseStatus = Integer.valueOf(jSONObject.optInt("purchaseStatus"));
        } else {
            this.purchaseStatus = null;
        }
        this.developerPayload = jSONObject.optString(DEVELOPER_PAYLOAD, null);
        this.purchaseToken = jSONObject.optString("purchaseToken", null);
    }

    public Boolean getAutoRenewing() {
        return this.autoRenewing;
    }

    public String getOrderId() {
        return this.orderId;
    }

    public String getPackageName() {
        return this.packageName;
    }

    public String getProductId() {
        return this.productId;
    }

    public Long getPurchaseTime() {
        return this.purchaseTime;
    }

    public Integer getPurchaseStatus() {
        return this.purchaseStatus;
    }

    public String getDeveloperPayload() {
        return this.developerPayload;
    }

    public String getPurchaseToken() {
        return this.purchaseToken;
    }
}
