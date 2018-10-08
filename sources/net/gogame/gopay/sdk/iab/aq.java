package net.gogame.gopay.sdk.iab;

import android.annotation.TargetApi;
import android.graphics.Bitmap;
import android.net.Uri;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import net.gogame.gopay.sdk.support.C1084m;

final class aq extends WebViewClient {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1070a;

    aq(PurchaseActivity purchaseActivity) {
        this.f1070a = purchaseActivity;
    }

    public final void onPageFinished(WebView webView, String str) {
        super.onPageFinished(webView, str);
        this.f1070a.m802b();
        this.f1070a.f1024c = true;
        this.f1070a.f1013I = false;
        if (str.startsWith("gopay:///")) {
            PurchaseActivity.m804b(this.f1070a, str);
        }
    }

    public final void onPageStarted(WebView webView, String str, Bitmap bitmap) {
        this.f1070a.f1024c = false;
        this.f1070a.m784a();
        super.onPageStarted(webView, str, bitmap);
    }

    public final void onReceivedError(WebView webView, int i, String str, String str2) {
        this.f1070a.f1012H.setError(i, str);
        this.f1070a.f1012H.setFailedUrl(str2);
        String j = C1084m.m939j();
        if (j != null) {
            webView.loadData(j, "text/html; charset=UTF-8", null);
        }
    }

    @TargetApi(23)
    public final void onReceivedError(WebView webView, WebResourceRequest webResourceRequest, WebResourceError webResourceError) {
        if (webResourceRequest.isForMainFrame()) {
            Uri url = webResourceRequest.getUrl();
            this.f1070a.f1012H.setError(webResourceError.getErrorCode(), webResourceError.getDescription().toString());
            this.f1070a.f1012H.setFailedUrl(url.toString());
            new StringBuilder("URL1: ").append(url);
            String j = C1084m.m939j();
            if (j != null) {
                webView.loadData(j, "text/html; charset=UTF-8", null);
            }
        }
    }

    public final boolean shouldOverrideUrlLoading(WebView webView, String str) {
        this.f1070a.m784a();
        if (!str.startsWith("gopay:///")) {
            return super.shouldOverrideUrlLoading(webView, str);
        }
        PurchaseActivity.m804b(this.f1070a, str);
        return true;
    }
}
