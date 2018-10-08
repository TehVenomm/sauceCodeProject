package net.gogame.gopay.vip;

public class HttpException extends Exception {
    /* renamed from: a */
    private int f3655a;
    /* renamed from: b */
    private String f3656b;

    public HttpException(String str, int i, String str2) {
        super(str);
        this.f3655a = i;
        this.f3656b = str2;
    }

    public int getResponseCode() {
        return this.f3655a;
    }

    public void setResponseCode(int i) {
        this.f3655a = i;
    }

    public String getResponseMessage() {
        return this.f3656b;
    }

    public void setResponseMessage(String str) {
        this.f3656b = str;
    }
}
