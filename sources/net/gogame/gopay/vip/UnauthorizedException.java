package net.gogame.gopay.vip;

public class UnauthorizedException extends Exception {
    /* renamed from: a */
    private int f3672a;
    /* renamed from: b */
    private String f3673b;

    public UnauthorizedException(String str, int i, String str2) {
        super(str);
        this.f3672a = i;
        this.f3673b = str2;
    }

    public int getResponseCode() {
        return this.f3672a;
    }

    public void setResponseCode(int i) {
        this.f3672a = i;
    }

    public String getResponseMessage() {
        return this.f3673b;
    }

    public void setResponseMessage(String str) {
        this.f3673b = str;
    }
}
