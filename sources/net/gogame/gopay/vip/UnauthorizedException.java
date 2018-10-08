package net.gogame.gopay.vip;

public class UnauthorizedException extends Exception {
    /* renamed from: a */
    private int f1284a;
    /* renamed from: b */
    private String f1285b;

    public UnauthorizedException(String str, int i, String str2) {
        super(str);
        this.f1284a = i;
        this.f1285b = str2;
    }

    public int getResponseCode() {
        return this.f1284a;
    }

    public void setResponseCode(int i) {
        this.f1284a = i;
    }

    public String getResponseMessage() {
        return this.f1285b;
    }

    public void setResponseMessage(String str) {
        this.f1285b = str;
    }
}
