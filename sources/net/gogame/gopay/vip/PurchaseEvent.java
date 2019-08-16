package net.gogame.gopay.vip;

import com.appsflyer.AppsFlyerProperties;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class PurchaseEvent implements BaseEvent {
    public static final String EVENT_TYPE = "PurchaseEvent";

    /* renamed from: a */
    private String f1335a;

    /* renamed from: b */
    private String f1336b;

    /* renamed from: c */
    private String f1337c;

    /* renamed from: d */
    private String f1338d;

    /* renamed from: e */
    private double f1339e;

    /* renamed from: f */
    private long f1340f;

    /* renamed from: g */
    private String f1341g;

    /* renamed from: h */
    private VerificationStatus f1342h;

    /* renamed from: i */
    private boolean f1343i;

    public enum VerificationStatus {
        NOT_VERIFIED(0),
        VERIFICATION_SUCCEEDED(1),
        VERIFICATION_FAILED(2);
        

        /* renamed from: a */
        private final int f1345a;

        private VerificationStatus(int i) {
            this.f1345a = i;
        }

        public int getValue() {
            return this.f1345a;
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
        return this.f1335a;
    }

    public void setReferenceId(String str) {
        this.f1335a = str;
    }

    public String getGuid() {
        return this.f1336b;
    }

    public void setGuid(String str) {
        this.f1336b = str;
    }

    public String getProductId() {
        return this.f1337c;
    }

    public void setProductId(String str) {
        this.f1337c = str;
    }

    public String getCurrencyCode() {
        return this.f1338d;
    }

    public void setCurrencyCode(String str) {
        this.f1338d = str;
    }

    public double getPrice() {
        return this.f1339e;
    }

    public void setPrice(double d) {
        this.f1339e = d;
    }

    public long getTimestamp() {
        return this.f1340f;
    }

    public void setTimestamp(long j) {
        this.f1340f = j;
    }

    public String getOrderId() {
        return this.f1341g;
    }

    public void setOrderId(String str) {
        this.f1341g = str;
    }

    public VerificationStatus getVerificationStatus() {
        return this.f1342h;
    }

    public void setVerificationStatus(VerificationStatus verificationStatus) {
        this.f1342h = verificationStatus;
    }

    public boolean isSandbox() {
        return this.f1343i;
    }

    public void setSandbox(boolean z) {
        this.f1343i = z;
    }

    public JSONObject marshal() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("@eventType", EVENT_TYPE);
        jSONObject.put("referenceId", this.f1335a);
        jSONObject.put("guid", this.f1336b);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, this.f1337c);
        jSONObject.put(AppsFlyerProperties.CURRENCY_CODE, this.f1338d);
        jSONObject.put(Param.PRICE, this.f1339e);
        jSONObject.put("timestamp", this.f1340f);
        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, this.f1341g);
        if (this.f1342h != null) {
            jSONObject.put("verificationStatus", this.f1342h.getValue());
        }
        jSONObject.put("sandbox", this.f1343i);
        return jSONObject;
    }

    public void unmarshal(JSONObject jSONObject) throws JSONException {
        this.f1335a = jSONObject.optString("referenceId", null);
        this.f1336b = jSONObject.optString("guid", null);
        this.f1337c = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, null);
        this.f1338d = jSONObject.optString(AppsFlyerProperties.CURRENCY_CODE, null);
        this.f1339e = jSONObject.optDouble(Param.PRICE);
        this.f1340f = jSONObject.optLong("timestamp");
        this.f1341g = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID, null);
        this.f1342h = VerificationStatus.fromValue(jSONObject.optInt("verificationStatus", VerificationStatus.NOT_VERIFIED.getValue()));
        this.f1343i = jSONObject.optBoolean("sandbox");
    }
}
