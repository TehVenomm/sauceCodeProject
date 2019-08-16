package net.gogame.gopay.sdk.iab;

import android.annotation.TargetApi;
import android.graphics.Bitmap;
import android.net.Uri;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import net.gogame.gopay.sdk.support.C1415m;

/* renamed from: net.gogame.gopay.sdk.iab.aq */
final class C1373aq extends WebViewClient {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1063a;

    C1373aq(PurchaseActivity purchaseActivity) {
        this.f1063a = purchaseActivity;
    }

    public final void onPageFinished(WebView webView, String str) {
        super.onPageFinished(webView, str);
        this.f1063a.m810b();
        this.f1063a.f1029c = true;
        this.f1063a.f1018I = false;
        if (str.startsWith("gopay:///")) {
            PurchaseActivity.m812b(this.f1063a, str);
        }
    }

    public final void onPageStarted(WebView webView, String str, Bitmap bitmap) {
        this.f1063a.f1029c = false;
        this.f1063a.m792a();
        super.onPageStarted(webView, str, bitmap);
    }

    public final void onReceivedError(WebView webView, int i, String str, String str2) {
        this.f1063a.f1017H.setError(i, str);
        this.f1063a.f1017H.setFailedUrl(str2);
        String j = C1415m.m936j();
        if (j != null) {
            webView.loadData(j, "text/html; charset=UTF-8", null);
        }
    }

    @TargetApi(23)
    public final void onReceivedError(WebView webView, WebResourceRequest webResourceRequest, WebResourceError webResourceError) {
        if (webResourceRequest.isForMainFrame()) {
            Uri url = webResourceRequest.getUrl();
            this.f1063a.f1017H.setError(webResourceError.getErrorCode(), webResourceError.getDescription().toString());
            this.f1063a.f1017H.setFailedUrl(url.toString());
            new StringBuilder("URL1: ").append(url);
            String j = C1415m.m936j();
            if (j != null) {
                webView.loadData(j, "text/html; charset=UTF-8", null);
            }
        }
    }

    public final boolean shouldOverrideUrlLoading(WebView webView, String str) {
        this.f1063a.m792a();
        if (!str.startsWith("gopay:///")) {
            return super.shouldOverrideUrlLoading(webView, str);
        }
        PurchaseActivity.m812b(this.f1063a, str);
        return true;
    }
}
