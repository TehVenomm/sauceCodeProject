package net.gogame.gopay.vip;

public class UnauthorizedException extends Exception {

    /* renamed from: a */
    private int f1353a;

    /* renamed from: b */
    private String f1354b;

    public UnauthorizedException(String str, int i, String str2) {
        super(str);
        this.f1353a = i;
        this.f1354b = str2;
    }

    public int getResponseCode() {
        return this.f1353a;
    }

    public void setResponseCode(int i) {
        this.f1353a = i;
    }

    public String getResponseMessage() {
        return this.f1354b;
    }

    public void setResponseMessage(String str) {
        this.f1354b = str;
    }
}
