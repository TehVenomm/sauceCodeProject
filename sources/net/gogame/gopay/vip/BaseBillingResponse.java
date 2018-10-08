package net.gogame.gopay.vip;

public class BaseBillingResponse {
    /* renamed from: a */
    private boolean f3650a;
    /* renamed from: b */
    private int f3651b;
    /* renamed from: c */
    private String f3652c;

    public boolean isStatus() {
        return this.f3650a;
    }

    public void setStatus(boolean z) {
        this.f3650a = z;
    }

    public int getStatusCode() {
        return this.f3651b;
    }

    public void setStatusCode(int i) {
        this.f3651b = i;
    }

    public String getStatusMessage() {
        return this.f3652c;
    }

    public void setStatusMessage(String str) {
        this.f3652c = str;
    }
}
