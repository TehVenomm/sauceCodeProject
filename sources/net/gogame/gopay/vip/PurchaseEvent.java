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
    private String f1271a;
    /* renamed from: b */
    private String f1272b;
    /* renamed from: c */
    private String f1273c;
    /* renamed from: d */
    private String f1274d;
    /* renamed from: e */
    private double f1275e;
    /* renamed from: f */
    private long f1276f;
    /* renamed from: g */
    private String f1277g;
    /* renamed from: h */
    private VerificationStatus f1278h;
    /* renamed from: i */
    private boolean f1279i;

    public enum VerificationStatus {
        NOT_VERIFIED(0),
        VERIFICATION_SUCCEEDED(1),
        VERIFICATION_FAILED(2);
        
        /* renamed from: a */
        private final int f1270a;

        private VerificationStatus(int i) {
            this.f1270a = i;
        }

        public int getValue() {
            return this.f1270a;
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
        return this.f1271a;
    }

    public void setReferenceId(String str) {
        this.f1271a = str;
    }

    public String getGuid() {
        return this.f1272b;
    }

    public void setGuid(String str) {
        this.f1272b = str;
    }

    public String getProductId() {
        return this.f1273c;
    }

    public void setProductId(String str) {
        this.f1273c = str;
    }

    public String getCurrencyCode() {
        return this.f1274d;
    }

    public void setCurrencyCode(String str) {
        this.f1274d = str;
    }

    public double getPrice() {
        return this.f1275e;
    }

    public void setPrice(double d) {
        this.f1275e = d;
    }

    public long getTimestamp() {
        return this.f1276f;
    }

    public void setTimestamp(long j) {
        this.f1276f = j;
    }

    public String getOrderId() {
        return this.f1277g;
    }

    public void setOrderId(String str) {
        this.f1277g = str;
    }

    public VerificationStatus getVerificationStatus() {
        return this.f1278h;
    }

    public void setVerificationStatus(VerificationStatus verificationStatus) {
        this.f1278h = verificationStatus;
    }

    public boolean isSandbox() {
        return this.f1279i;
    }

    public void setSandbox(boolean z) {
        this.f1279i = z;
    }

    public JSONObject marshal() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("@eventType", EVENT_TYPE);
        jSONObject.put("referenceId", this.f1271a);
        jSONObject.put("guid", this.f1272b);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, this.f1273c);
        jSONObject.put(AppsFlyerProperties.CURRENCY_CODE, this.f1274d);
        jSONObject.put(Param.PRICE, this.f1275e);
        jSONObject.put(AppMeasurement.Param.TIMESTAMP, this.f1276f);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, this.f1277g);
        if (this.f1278h != null) {
            jSONObject.put("verificationStatus", this.f1278h.getValue());
        }
        jSONObject.put("sandbox", this.f1279i);
        return jSONObject;
    }

    public void unmarshal(JSONObject jSONObject) throws JSONException {
        this.f1271a = jSONObject.optString("referenceId", null);
        this.f1272b = jSONObject.optString("guid", null);
        this.f1273c = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, null);
        this.f1274d = jSONObject.optString(AppsFlyerProperties.CURRENCY_CODE, null);
        this.f1275e = jSONObject.optDouble(Param.PRICE);
        this.f1276f = jSONObject.optLong(AppMeasurement.Param.TIMESTAMP);
        this.f1277g = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, null);
        this.f1278h = VerificationStatus.fromValue(jSONObject.optInt("verificationStatus", VerificationStatus.NOT_VERIFIED.getValue()));
        this.f1279i = jSONObject.optBoolean("sandbox");
    }
}
