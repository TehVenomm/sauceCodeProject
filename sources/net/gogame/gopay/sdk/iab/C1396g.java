package net.gogame.gopay.sdk.iab;

import android.webkit.JavascriptInterface;

/* renamed from: net.gogame.gopay.sdk.iab.g */
final class C1396g {

    /* renamed from: a */
    private int f1106a;

    /* renamed from: b */
    private String f1107b;

    /* renamed from: c */
    private String f1108c;

    /* renamed from: d */
    private C1397h f1109d;

    C1396g() {
    }

    @JavascriptInterface
    public final void CloseWindow() {
    }

    @JavascriptInterface
    public final boolean canRetry() {
        return this.f1106a == -1 || this.f1106a == -2 || this.f1106a == -6 || this.f1106a == -8 || this.f1106a == -7 || this.f1106a == -15 || this.f1106a == -11;
    }

    @JavascriptInterface
    public final int getErrorCode() {
        return this.f1106a;
    }

    @JavascriptInterface
    public final String getErrorMessage() {
        return this.f1107b;
    }

    public final String getFailedUrl() {
        return this.f1108c;
    }

    @JavascriptInterface
    public final void onButtonClick() {
        if (this.f1109d != null) {
            this.f1109d.mo21518a();
        }
    }

    public final void setCallback(C1397h hVar) {
        this.f1109d = hVar;
    }

    public final void setError(int i, String str) {
        this.f1106a = i;
        this.f1107b = str;
    }

    public final void setFailedUrl(String str) {
        this.f1108c = str;
    }
}
