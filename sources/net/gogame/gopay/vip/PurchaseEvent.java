package net.gogame.gopay.vip;

import com.appsflyer.AppsFlyerProperties;
import com.google.android.gms.measurement.AppMeasurement;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class PurchaseEvent implements BaseEvent {
    public static final String EVENT_TYPE = "PurchaseEvent";
    /* renamed from: a */
    private String f3659a;
    /* renamed from: b */
    private String f3660b;
    /* renamed from: c */
    private String f3661c;
    /* renamed from: d */
    private String f3662d;
    /* renamed from: e */
    private double f3663e;
    /* renamed from: f */
    private long f3664f;
    /* renamed from: g */
    private String f3665g;
    /* renamed from: h */
    private VerificationStatus f3666h;
    /* renamed from: i */
    private boolean f3667i;

    public enum VerificationStatus {
        NOT_VERIFIED(0),
        VERIFICATION_SUCCEEDED(1),
        VERIFICATION_FAILED(2);
        
        /* renamed from: a */
        private final int f3658a;

        private VerificationStatus(int i) {
            this.f3658a = i;
        }

        public int getValue() {
            return this.f3658a;
        }

        public static VerificationStatus fromValue(int i) {
            for (VerificationStatus verificationStatus : values()) {
                if (verificationStatus.getValue() == i) {
                    return verificationStatus;
                }
            }
            return null;
        }
    }

    public String getReferenceId() {
        return this.f3659a;
    }

    public void setReferenceId(String str) {
        this.f3659a = str;
    }

    public String getGuid() {
        return this.f3660b;
    }

    public void setGuid(String str) {
        this.f3660b = str;
    }

    public String getProductId() {
        return this.f3661c;
    }

    public void setProductId(String str) {
        this.f3661c = str;
    }

    public String getCurrencyCode() {
        return this.f3662d;
    }

    public void setCurrencyCode(String str) {
        this.f3662d = str;
    }

    public double getPrice() {
        return this.f3663e;
    }

    public void setPrice(double d) {
        this.f3663e = d;
    }

    public long getTimestamp() {
        return this.f3664f;
    }

    public void setTimestamp(long j) {
        this.f3664f = j;
    }

    public String getOrderId() {
        return this.f3665g;
    }

    public void setOrderId(String str) {
        this.f3665g = str;
    }

    public VerificationStatus getVerificationStatus() {
        return this.f3666h;
    }

    public void setVerificationStatus(VerificationStatus verificationStatus) {
        this.f3666h = verificationStatus;
    }

    public boolean isSandbox() {
        return this.f3667i;
    }

    public void setSandbox(boolean z) {
        this.f3667i = z;
    }

    public JSONObject marshal() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("@eventType", EVENT_TYPE);
        jSONObject.put("referenceId", this.f3659a);
        jSONObject.put("guid", this.f3660b);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, this.f3661c);
        jSONObject.put(AppsFlyerProperties.CURRENCY_CODE, this.f3662d);
        jSONObject.put(Param.PRICE, this.f3663e);
        jSONObject.put(AppMeasurement.Param.TIMESTAMP, this.f3664f);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, this.f3665g);
        if (this.f3666h != null) {
            jSONObject.put("verificationStatus", this.f3666h.getValue());
        }
        jSONObject.put("sandbox", this.f3667i);
        return jSONObject;
    }

    public void unmarshal(JSONObject jSONObject) throws JSONException {
        this.f3659a = jSONObject.optString("referenceId", null);
        this.f3660b = jSONObject.optString("guid", null);
        this.f3661c = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, null);
        this.f3662d = jSONObject.optString(AppsFlyerProperties.CURRENCY_CODE, null);
        this.f3663e = jSONObject.optDouble(Param.PRICE);
        this.f3664f = jSONObject.optLong(AppMeasurement.Param.TIMESTAMP);
        this.f3665g = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, null);
        this.f3666h = VerificationStatus.fromValue(jSONObject.optInt("verificationStatus", VerificationStatus.NOT_VERIFIED.getValue()));
        this.f3667i = jSONObject.optBoolean("sandbox");
    }
}
