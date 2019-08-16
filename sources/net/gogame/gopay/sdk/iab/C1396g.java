package net.gogame.gopay.sdk.iab;

import android.webkit.JavascriptInterface;

/* renamed from: net.gogame.gopay.sdk.iab.g */
final class C1396g {

    /* renamed from: a */
    private int f1100a;

    /* renamed from: b */
    private String f1101b;

    /* renamed from: c */
    private String f1102c;

    /* renamed from: d */
    private C1397h f1103d;

    C1396g() {
    }

    @JavascriptInterface
    public final void CloseWindow() {
    }

    @JavascriptInterface
    public final boolean canRetry() {
        return this.f1100a == -1 || this.f1100a == -2 || this.f1100a == -6 || this.f1100a == -8 || this.f1100a == -7 || this.f1100a == -15 || this.f1100a == -11;
    }

    @JavascriptInterface
    public final int getErrorCode() {
        return this.f1100a;
    }

    @JavascriptInterface
    public final String getErrorMessage() {
        return this.f1101b;
    }

    public final String getFailedUrl() {
        return this.f1102c;
    }

    @JavascriptInterface
    public final void onButtonClick() {
        if (this.f1103d != null) {
            this.f1103d.mo21518a();
        }
    }

    public final void setCallback(C1397h hVar) {
        this.f1103d = hVar;
    }

    public final void setError(int i, String str) {
        this.f1100a = i;
        this.f1101b = str;
    }

    public final void setFailedUrl(String str) {
        this.f1102c = str;
    }
}
