package net.gogame.gowrap.p019ui.v2017_1;

import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.p019ui.WebViewContext;
import net.gogame.gowrap.p019ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.v2017_1.WebViewFragment */
public class WebViewFragment extends AbstractWebViewFragment implements WebViewContext {
    public static final String START_URL_ARGUMENT = "startUrl";
    private String currentRequestedUrl = null;

    public WebViewFragment() {
        super(C1423R.C1425layout.net_gogame_gowrap_fragment_webview);
    }

    public static WebViewFragment newFragmentWithUrl(String str) {
        WebViewFragment webViewFragment = new WebViewFragment();
        Bundle bundle = new Bundle();
        bundle.putString(START_URL_ARGUMENT, str);
        webViewFragment.setArguments(bundle);
        return webViewFragment;
    }

    public boolean loadUrl(String str) {
        if (!isWebViewAvailable() || getWebView() == null) {
            return false;
        }
        getWebView().loadUrl("javascript:window.location.href='" + str + "'");
        this.currentRequestedUrl = str;
        return true;
    }

    /* access modifiers changed from: protected */
    public void init(Bundle bundle) {
        loadUrl(bundle.getString(START_URL_ARGUMENT));
    }

    /* access modifiers changed from: protected */
    public boolean doHandleUri(Uri uri) {
        if (!StringUtils.isEquals(uri.toString(), this.currentRequestedUrl)) {
            try {
                if (ExternalAppLauncher.openUrlInExternalBrowser(getActivity(), uri.toString())) {
                    return true;
                }
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        return false;
    }

    /* access modifiers changed from: protected */
    public BackgroundMode getBackgroundMode(Uri uri) {
        if (uri.getHost() == null || !uri.getHost().endsWith(".gogame.net")) {
            return BackgroundMode.DEFAULT;
        }
        return BackgroundMode.TRANSPARENT;
    }
}
