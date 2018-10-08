package net.gogame.gowrap.ui.v2017_1;

import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.C1110R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.WebViewContext;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;

public class WebViewFragment extends AbstractWebViewFragment implements WebViewContext {
    public static final String START_URL_ARGUMENT = "startUrl";
    private String currentRequestedUrl = null;

    public WebViewFragment() {
        super(C1110R.layout.net_gogame_gowrap_fragment_webview);
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

    protected void init(Bundle bundle) {
        loadUrl(bundle.getString(START_URL_ARGUMENT));
    }

    protected boolean doHandleUri(Uri uri) {
        if (!StringUtils.isEquals(uri.toString(), this.currentRequestedUrl)) {
            try {
                if (ExternalAppLauncher.openUrlInExternalBrowser(getActivity(), uri.toString())) {
                    return true;
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        return false;
    }

    protected BackgroundMode getBackgroundMode(Uri uri) {
        if (uri.getHost() == null || !uri.getHost().endsWith(".gogame.net")) {
            return BackgroundMode.DEFAULT;
        }
        return BackgroundMode.TRANSPARENT;
    }
}
