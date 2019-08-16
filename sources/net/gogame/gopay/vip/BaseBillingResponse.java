package net.gogame.gopay.vip;

public class BaseBillingResponse {

    /* renamed from: a */
    private boolean f1329a;

    /* renamed from: b */
    private int f1330b;

    /* renamed from: c */
    private String f1331c;

    public boolean isStatus() {
        return this.f1329a;
    }

    public void setStatus(boolean z) {
        this.f1329a = z;
    }

    public int getStatusCode() {
        return this.f1330b;
    }

    public void setStatusCode(int i) {
        this.f1330b = i;
    }

    public String getStatusMessage() {
        return this.f1331c;
    }

    public void setStatusMessage(String str) {
        this.f1331c = str;
    }
}
