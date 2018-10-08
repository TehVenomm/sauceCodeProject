package net.gogame.gopay.vip;

public class HttpException extends Exception {
    /* renamed from: a */
    private int f1267a;
    /* renamed from: b */
    private String f1268b;

    public HttpException(String str, int i, String str2) {
        super(str);
        this.f1267a = i;
        this.f1268b = str2;
    }

    public int getResponseCode() {
        return this.f1267a;
    }

    public void setResponseCode(int i) {
        this.f1267a = i;
    }

    public String getResponseMessage() {
        return this.f1268b;
    }

    public void setResponseMessage(String str) {
        this.f1268b = str;
    }
}
