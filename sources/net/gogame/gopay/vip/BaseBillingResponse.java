package net.gogame.gopay.vip;

public class BaseBillingResponse {
    /* renamed from: a */
    private boolean f1262a;
    /* renamed from: b */
    private int f1263b;
    /* renamed from: c */
    private String f1264c;

    public boolean isStatus() {
        return this.f1262a;
    }

    public void setStatus(boolean z) {
        this.f1262a = z;
    }

    public int getStatusCode() {
        return this.f1263b;
    }

    public void setStatusCode(int i) {
        this.f1263b = i;
    }

    public String getStatusMessage() {
        return this.f1264c;
    }

    public void setStatusMessage(String str) {
        this.f1264c = str;
    }
}
