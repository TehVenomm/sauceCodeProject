package net.gogame.gopay.vip;

public class BaseBillingResponse {

    /* renamed from: a */
    private boolean f1317a;

    /* renamed from: b */
    private int f1318b;

    /* renamed from: c */
    private String f1319c;

    public boolean isStatus() {
        return this.f1317a;
    }

    public void setStatus(boolean z) {
        this.f1317a = z;
    }

    public int getStatusCode() {
        return this.f1318b;
    }

    public void setStatusCode(int i) {
        this.f1318b = i;
    }

    public String getStatusMessage() {
        return this.f1319c;
    }

    public void setStatusMessage(String str) {
        this.f1319c = str;
    }
}
