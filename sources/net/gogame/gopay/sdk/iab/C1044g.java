package net.gogame.gopay.sdk.iab;

import android.webkit.JavascriptInterface;

/* renamed from: net.gogame.gopay.sdk.iab.g */
final class C1044g {
    /* renamed from: a */
    private int f1139a;
    /* renamed from: b */
    private String f1140b;
    /* renamed from: c */
    private String f1141c;
    /* renamed from: d */
    private C1036h f1142d;

    C1044g() {
    }

    @JavascriptInterface
    public final void CloseWindow() {
    }

    @JavascriptInterface
    public final boolean canRetry() {
        return this.f1139a == -1 || this.f1139a == -2 || this.f1139a == -6 || this.f1139a == -8 || this.f1139a == -7 || this.f1139a == -15 || this.f1139a == -11;
    }

    @JavascriptInterface
    public final int getErrorCode() {
        return this.f1139a;
    }

    @JavascriptInterface
    public final String getErrorMessage() {
        return this.f1140b;
    }

    public final String getFailedUrl() {
        return this.f1141c;
    }

    @JavascriptInterface
    public final void onButtonClick() {
        if (this.f1142d != null) {
            this.f1142d.mo4422a();
        }
    }

    public final void setCallback(C1036h c1036h) {
        this.f1142d = c1036h;
    }

    public final void setError(int i, String str) {
        this.f1139a = i;
        this.f1140b = str;
    }

    public final void setFailedUrl(String str) {
        this.f1141c = str;
    }
}
