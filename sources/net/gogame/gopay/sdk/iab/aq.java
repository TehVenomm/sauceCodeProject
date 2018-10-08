package net.gogame.gopay.sdk.iab;

import android.annotation.TargetApi;
import android.graphics.Bitmap;
import android.net.Uri;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import net.gogame.gopay.sdk.support.C1400m;

final class aq extends WebViewClient {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3458a;

    aq(PurchaseActivity purchaseActivity) {
        this.f3458a = purchaseActivity;
    }

    public final void onPageFinished(WebView webView, String str) {
        super.onPageFinished(webView, str);
        this.f3458a.m3827b();
        this.f3458a.f3412c = true;
        this.f3458a.f3401I = false;
        if (str.startsWith("gopay:///")) {
            PurchaseActivity.m3829b(this.f3458a, str);
        }
    }

    public final void onPageStarted(WebView webView, String str, Bitmap bitmap) {
        this.f3458a.f3412c = false;
        this.f3458a.m3809a();
        super.onPageStarted(webView, str, bitmap);
    }

    public final void onReceivedError(WebView webView, int i, String str, String str2) {
        this.f3458a.f3400H.setError(i, str);
        this.f3458a.f3400H.setFailedUrl(str2);
        String j = C1400m.m3964j();
        if (j != null) {
            webView.loadData(j, "text/html; charset=UTF-8", null);
        }
    }

    @TargetApi(23)
    public final void onReceivedError(WebView webView, WebResourceRequest webResourceRequest, WebResourceError webResourceError) {
        if (webResourceRequest.isForMainFrame()) {
            Uri url = webResourceRequest.getUrl();
            this.f3458a.f3400H.setError(webResourceError.getErrorCode(), webResourceError.getDescription().toString());
            this.f3458a.f3400H.setFailedUrl(url.toString());
            new StringBuilder("URL1: ").append(url);
            String j = C1400m.m3964j();
            if (j != null) {
                webView.loadData(j, "text/html; charset=UTF-8", null);
            }
        }
    }

    public final boolean shouldOverrideUrlLoading(WebView webView, String str) {
        this.f3458a.m3809a();
        if (!str.startsWith("gopay:///")) {
            return super.shouldOverrideUrlLoading(webView, str);
        }
        PurchaseActivity.m3829b(this.f3458a, str);
        return true;
    }
}
