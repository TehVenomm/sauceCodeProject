package net.gogame.gopay.vip;

public class HttpException extends Exception {

    /* renamed from: a */
    private int f1321a;

    /* renamed from: b */
    private String f1322b;

    public HttpException(String str, int i, String str2) {
        super(str);
        this.f1321a = i;
        this.f1322b = str2;
    }

    public int getResponseCode() {
        return this.f1321a;
    }

    public void setResponseCode(int i) {
        this.f1321a = i;
    }

    public String getResponseMessage() {
        return this.f1322b;
    }

    public void setResponseMessage(String str) {
        this.f1322b = str;
    }
}
