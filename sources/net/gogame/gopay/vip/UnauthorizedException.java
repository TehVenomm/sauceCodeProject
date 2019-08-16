package net.gogame.gopay.vip;

public class UnauthorizedException extends Exception {

    /* renamed from: a */
    private int f1339a;

    /* renamed from: b */
    private String f1340b;

    public UnauthorizedException(String str, int i, String str2) {
        super(str);
        this.f1339a = i;
        this.f1340b = str2;
    }

    public int getResponseCode() {
        return this.f1339a;
    }

    public void setResponseCode(int i) {
        this.f1339a = i;
    }

    public String getResponseMessage() {
        return this.f1340b;
    }

    public void setResponseMessage(String str) {
        this.f1340b = str;
    }
}
