package net.gogame.gopay.sdk.iab;

import android.webkit.JavascriptInterface;

/* renamed from: net.gogame.gopay.sdk.iab.g */
final class C1360g {
    /* renamed from: a */
    private int f3527a;
    /* renamed from: b */
    private String f3528b;
    /* renamed from: c */
    private String f3529c;
    /* renamed from: d */
    private C1352h f3530d;

    C1360g() {
    }

    @JavascriptInterface
    public final void CloseWindow() {
    }

    @JavascriptInterface
    public final boolean canRetry() {
        return this.f3527a == -1 || this.f3527a == -2 || this.f3527a == -6 || this.f3527a == -8 || this.f3527a == -7 || this.f3527a == -15 || this.f3527a == -11;
    }

    @JavascriptInterface
    public final int getErrorCode() {
        return this.f3527a;
    }

    @JavascriptInterface
    public final String getErrorMessage() {
        return this.f3528b;
    }

    public final String getFailedUrl() {
        return this.f3529c;
    }

    @JavascriptInterface
    public final void onButtonClick() {
        if (this.f3530d != null) {
            this.f3530d.mo4870a();
        }
    }

    public final void setCallback(C1352h c1352h) {
        this.f3530d = c1352h;
    }

    public final void setError(int i, String str) {
        this.f3527a = i;
        this.f3528b = str;
    }

    public final void setFailedUrl(String str) {
        this.f3529c = str;
    }
}
