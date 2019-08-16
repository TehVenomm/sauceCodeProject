package net.gogame.gopay.vip;

public class HttpException extends Exception {

    /* renamed from: a */
    private int f1333a;

    /* renamed from: b */
    private String f1334b;

    public HttpException(String str, int i, String str2) {
        super(str);
        this.f1333a = i;
        this.f1334b = str2;
    }

    public int getResponseCode() {
        return this.f1333a;
    }

    public void setResponseCode(int i) {
        this.f1333a = i;
    }

    public String getResponseMessage() {
        return this.f1334b;
    }

    public void setResponseMessage(String str) {
        this.f1334b = str;
    }
}
