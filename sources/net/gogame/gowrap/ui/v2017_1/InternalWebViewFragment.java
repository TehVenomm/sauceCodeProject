package net.gogame.gowrap.ui.v2017_1;

import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.ui.InternalWebViewContext;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;

public class InternalWebViewFragment extends AbstractWebViewFragment implements InternalWebViewContext {
    public static final String BASE_URL_ARGUMENT = "baseUrl";
    public static final String START_HTML_ARGUMENT = "startHtml";

    public InternalWebViewFragment() {
        super(C1426R.layout.net_gogame_gowrap_fragment_webview_internal);
    }

    public static InternalWebViewFragment newFragmentWithHtml(String str, String str2) {
        InternalWebViewFragment internalWebViewFragment = new InternalWebViewFragment();
        Bundle bundle = new Bundle();
        bundle.putString(START_HTML_ARGUMENT, str);
        bundle.putString(BASE_URL_ARGUMENT, str2);
        internalWebViewFragment.setArguments(bundle);
        return internalWebViewFragment;
    }

    public boolean loadHtml(String str, String str2) {
        if (!isWebViewAvailable() || getWebView() == null) {
            return false;
        }
        if (str2 != null) {
            getWebView().loadDataWithBaseURL(str2, str, "text/html; charset=utf-8", "UTF-8", str2);
        } else {
            getWebView().loadData(str, "text/html; charset=utf-8", "UTF-8");
        }
        return true;
    }

    protected void init(Bundle bundle) {
        loadHtml(bundle.getString(START_HTML_ARGUMENT), bundle.getString(BASE_URL_ARGUMENT));
    }

    protected boolean doHandleUri(Uri uri) {
        try {
            if (ExternalAppLauncher.openUrlInExternalBrowser(getActivity(), uri.toString())) {
                return true;
            }
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
        if (getUIContext() == null) {
            return false;
        }
        getUIContext().loadUrl(uri.toString(), false);
        return true;
    }

    protected BackgroundMode getBackgroundMode(Uri uri) {
        return BackgroundMode.TRANSPARENT;
    }

    public boolean onBackPressed() {
        return false;
    }
}
