package net.gogame.gopay.vip;

import com.appsflyer.AppsFlyerProperties;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class PurchaseEvent implements BaseEvent {
    public static final String EVENT_TYPE = "PurchaseEvent";

    /* renamed from: a */
    private String f1323a;

    /* renamed from: b */
    private String f1324b;

    /* renamed from: c */
    private String f1325c;

    /* renamed from: d */
    private String f1326d;

    /* renamed from: e */
    private double f1327e;

    /* renamed from: f */
    private long f1328f;

    /* renamed from: g */
    private String f1329g;

    /* renamed from: h */
    private VerificationStatus f1330h;

    /* renamed from: i */
    private boolean f1331i;

    public enum VerificationStatus {
        NOT_VERIFIED(0),
        VERIFICATION_SUCCEEDED(1),
        VERIFICATION_FAILED(2);
        

        /* renamed from: a */
        private final int f1333a;

        private VerificationStatus(int i) {
            this.f1333a = i;
        }

        public int getValue() {
            return this.f1333a;
        }

        public static VerificationStatus fromValue(int i) {
            VerificationStatus[] values;
            for (VerificationStatus verificationStatus : values()) {
                if (verificationStatus.getValue() == i) {
                    return verificationStatus;
                }
            }
            return null;
        }
    }

    public String getReferenceId() {
        return this.f1323a;
    }

    public void setReferenceId(String str) {
        this.f1323a = str;
    }

    public String getGuid() {
        return this.f1324b;
    }

    public void setGuid(String str) {
        this.f1324b = str;
    }

    public String getProductId() {
        return this.f1325c;
    }

    public void setProductId(String str) {
        this.f1325c = str;
    }

    public String getCurrencyCode() {
        return this.f1326d;
    }

    public void setCurrencyCode(String str) {
        this.f1326d = str;
    }

    public double getPrice() {
        return this.f1327e;
    }

    public void setPrice(double d) {
        this.f1327e = d;
    }

    public long getTimestamp() {
        return this.f1328f;
    }

    public void setTimestamp(long j) {
        this.f1328f = j;
    }

    public String getOrderId() {
        return this.f1329g;
    }

    public void setOrderId(String str) {
        this.f1329g = str;
    }

    public VerificationStatus getVerificationStatus() {
        return this.f1330h;
    }

    public void setVerificationStatus(VerificationStatus verificationStatus) {
        this.f1330h = verificationStatus;
    }

    public boolean isSandbox() {
        return this.f1331i;
    }

    public void setSandbox(boolean z) {
        this.f1331i = z;
    }

    public JSONObject marshal() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("@eventType", EVENT_TYPE);
        jSONObject.put("referenceId", this.f1323a);
        jSONObject.put("guid", this.f1324b);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, this.f1325c);
        jSONObject.put(AppsFlyerProperties.CURRENCY_CODE, this.f1326d);
        jSONObject.put(Param.PRICE, this.f1327e);
        jSONObject.put("timestamp", this.f1328f);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, this.f1329g);
        if (this.f1330h != null) {
            jSONObject.put("verificationStatus", this.f1330h.getValue());
        }
        jSONObject.put("sandbox", this.f1331i);
        return jSONObject;
    }

    public void unmarshal(JSONObject jSONObject) throws JSONException {
        this.f1323a = jSONObject.optString("referenceId", null);
        this.f1324b = jSONObject.optString("guid", null);
        this.f1325c = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, null);
        this.f1326d = jSONObject.optString(AppsFlyerProperties.CURRENCY_CODE, null);
        this.f1327e = jSONObject.optDouble(Param.PRICE);
        this.f1328f = jSONObject.optLong("timestamp");
        this.f1329g = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, null);
        this.f1330h = VerificationStatus.fromValue(jSONObject.optInt("verificationStatus", VerificationStatus.NOT_VERIFIED.getValue()));
        this.f1331i = jSONObject.optBoolean("sandbox");
    }
}
