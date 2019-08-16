package net.gogame.gowrap.p019ui.v2017_1;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.net.http.SslError;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.ConsoleMessage;
import android.webkit.CookieManager;
import android.webkit.SslErrorHandler;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebResourceResponse;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ProgressBar;
import com.facebook.share.internal.ShareConstants;
import java.io.IOException;
import java.util.Locale;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.p019ui.BackPressedListener;
import net.gogame.gowrap.p019ui.InternalWebViewContext;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.cpr.VideoEnabledWebChromeClient;
import net.gogame.gowrap.p019ui.cpr.VideoEnabledWebChromeClient.ToggledFullscreenCallback;
import net.gogame.gowrap.p019ui.cpr.VideoEnabledWebView;
import net.gogame.gowrap.p021io.utils.IOUtils;
import net.gogame.gowrap.support.NetworkUtils;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.v2017_1.AbstractWebViewFragment */
public abstract class AbstractWebViewFragment extends Fragment implements BackPressedListener {
    protected static final String CHARSET_NAME = "UTF-8";
    private static final String COMMUNITY_SCHEME = "community";
    protected static final String CONTENT_TYPE = "text/html; charset=utf-8";
    private static final boolean HANDLE_VIMEO_URLS = true;
    private static final boolean HANDLE_YOUTUBE_URLS = true;
    private static final String SHARE_URL_PREFIX = "share-";
    /* access modifiers changed from: private */
    public BackgroundMode backgroundMode = null;
    /* access modifiers changed from: private */
    public Context context = null;
    private String errorHtmlTemplate = null;
    private boolean errorHtmlTemplateInitialized = false;
    private final int layoutResourceId;
    private ProgressBar progressBar = null;
    private ProgressBar progressBar2 = null;
    private Bundle savedInstanceState;
    private VideoEnabledWebChromeClient webChromeClient = null;
    /* access modifiers changed from: private */
    public VideoEnabledWebView webView = null;
    private boolean webViewIsAvailable;

    /* renamed from: net.gogame.gowrap.ui.v2017_1.AbstractWebViewFragment$4 */
    static /* synthetic */ class C14634 {

        /* renamed from: $SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode */
        static final /* synthetic */ int[] f1187x21f53947 = new int[BackgroundMode.values().length];

        static {
            try {
                f1187x21f53947[BackgroundMode.TRANSPARENT.ordinal()] = 1;
            } catch (NoSuchFieldError e) {
            }
            try {
                f1187x21f53947[BackgroundMode.DEFAULT.ordinal()] = 2;
            } catch (NoSuchFieldError e2) {
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_1.AbstractWebViewFragment$BackgroundMode */
    protected enum BackgroundMode {
        DEFAULT,
        TRANSPARENT
    }

    /* access modifiers changed from: protected */
    public abstract boolean doHandleUri(Uri uri);

    /* access modifiers changed from: protected */
    public abstract BackgroundMode getBackgroundMode(Uri uri);

    /* access modifiers changed from: protected */
    public abstract void init(Bundle bundle);

    public AbstractWebViewFragment(int i) {
        this.layoutResourceId = i;
    }

    /* access modifiers changed from: protected */
    public WebView getWebView() {
        return this.webView;
    }

    /* access modifiers changed from: protected */
    public boolean isWebViewAvailable() {
        return this.webViewIsAvailable;
    }

    public String getCurrentUrl() {
        if (this.webView == null) {
            return null;
        }
        return this.webView.getUrl();
    }

    /* access modifiers changed from: private */
    public String composeHtml(Context context2, String str, String str2) {
        if (!this.errorHtmlTemplateInitialized) {
            try {
                this.errorHtmlTemplate = IOUtils.assetToString(context2, new String[]{"net/gogame/gowrap/webview-error-template.html", "net/gogame/gowrap/webview-error-template-default.html"}, "UTF-8");
            } catch (IOException e) {
            }
            this.errorHtmlTemplateInitialized = true;
        }
        if (this.errorHtmlTemplate == null) {
            return "";
        }
        return String.format(Locale.getDefault(), this.errorHtmlTemplate, new Object[]{StringUtils.escapeHtml(str), StringUtils.escapeHtml(str2)});
    }

    /* access modifiers changed from: protected */
    public UIContext getUIContext() {
        if (this.context instanceof UIContext) {
            return (UIContext) this.context;
        }
        return null;
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(this.layoutResourceId, viewGroup, false);
        this.progressBar = (ProgressBar) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_progressBar);
        this.progressBar2 = (ProgressBar) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_progressBar2);
        this.webView = (VideoEnabledWebView) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_main_webview);
        this.webViewIsAvailable = true;
        this.webView.setBackgroundColor(0);
        View findViewById = inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_webview_non_video_layout);
        ViewGroup viewGroup2 = (ViewGroup) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_webview_video_layout);
        View inflate2 = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_view_loading_video, viewGroup, false);
        WebSettings settings = this.webView.getSettings();
        settings.setDefaultTextEncodingName("utf-8");
        settings.setJavaScriptEnabled(true);
        settings.setJavaScriptCanOpenWindowsAutomatically(false);
        settings.setDomStorageEnabled(true);
        settings.setGeolocationEnabled(true);
        if (VERSION.SDK_INT >= 17) {
            settings.setMediaPlaybackRequiresUserGesture(false);
        }
        if (VERSION.SDK_INT >= 21) {
            settings.setMixedContentMode(0);
        }
        CookieManager.getInstance().setAcceptCookie(true);
        this.webChromeClient = new VideoEnabledWebChromeClient(findViewById, viewGroup2, inflate2, this.webView) {
            public boolean onConsoleMessage(ConsoleMessage consoleMessage) {
                Log.v(Constants.TAG, String.format("%s %s [%s:%d]", new Object[]{consoleMessage.messageLevel(), consoleMessage.message(), consoleMessage.sourceId(), Integer.valueOf(consoleMessage.lineNumber())}));
                return super.onConsoleMessage(consoleMessage);
            }

            public void onProgressChanged(WebView webView, int i) {
                super.onProgressChanged(webView, i);
                if (AbstractWebViewFragment.this.context instanceof UIContext) {
                    UIContext uIContext = (UIContext) AbstractWebViewFragment.this.context;
                    if (i == 100) {
                        uIContext.onLoadingFinished();
                    } else {
                        uIContext.onLoadingStarted();
                    }
                    AbstractWebViewFragment.this.setProgress(i);
                }
            }
        };
        this.webChromeClient.setOnToggledFullscreen(new ToggledFullscreenCallback() {
            public void toggledFullscreen(boolean z) {
                if (AbstractWebViewFragment.this.context instanceof UIContext) {
                    UIContext uIContext = (UIContext) AbstractWebViewFragment.this.context;
                    if (z) {
                        uIContext.enterFullscreen(Integer.valueOf(6));
                    } else {
                        uIContext.exitFullscreen();
                    }
                }
            }
        });
        this.webView.setWebChromeClient(this.webChromeClient);
        this.webView.setWebViewClient(new WebViewClient() {
            private String currentUrl = null;

            private boolean handleUri(Uri uri) {
                if (StringUtils.isEquals(uri.getScheme(), "market") || uri.toString().startsWith("https://play.google.com/")) {
                    if (AbstractWebViewFragment.this.context != null) {
                        AbstractWebViewFragment.this.context.startActivity(new Intent("android.intent.action.VIEW", uri));
                        return true;
                    }
                } else if (StringUtils.startsWith(uri.getScheme(), AbstractWebViewFragment.SHARE_URL_PREFIX)) {
                    if (AbstractWebViewFragment.this.context != null) {
                        String substring = uri.toString().substring(AbstractWebViewFragment.SHARE_URL_PREFIX.length());
                        Intent intent = new Intent();
                        intent.setAction("android.intent.action.SEND");
                        intent.putExtra("android.intent.extra.TEXT", substring);
                        intent.setType("text/plain");
                        AbstractWebViewFragment.this.context.startActivity(Intent.createChooser(intent, AbstractWebViewFragment.this.getString(C1423R.string.net_gogame_gowrap_share_prompt)));
                        return true;
                    }
                } else if (StringUtils.isEquals(uri.getScheme(), AbstractWebViewFragment.COMMUNITY_SCHEME)) {
                    String schemeSpecificPart = uri.getSchemeSpecificPart();
                    LocaleConfiguration localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration(AbstractWebViewFragment.this.context);
                    if (localeConfiguration == null) {
                        return true;
                    }
                    String str = null;
                    if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_HOME)) {
                        str = localeConfiguration.getWhatsNewUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_FACEBOOK)) {
                        str = localeConfiguration.getFacebookUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_TWITTER)) {
                        str = localeConfiguration.getTwitterUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_INSTAGRAM)) {
                        str = localeConfiguration.getInstagramUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_YOUTUBE)) {
                        str = localeConfiguration.getYoutubeUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_FORUM)) {
                        str = localeConfiguration.getForumUrl();
                    } else if (StringUtils.isEquals(schemeSpecificPart, InternalConstants.COMMUNITY_WIKI)) {
                        str = localeConfiguration.getWikiUrl();
                    }
                    if (str == null) {
                        return true;
                    }
                    AbstractWebViewFragment.this.webView.loadUrl(str);
                    return true;
                } else if (GoWrapImpl.INSTANCE.handleCustomUri(uri)) {
                    return true;
                } else {
                    if (StringUtils.isEquals(uri.getHost(), "www.youtube.com") || StringUtils.isEquals(uri.getHost(), "youtu.be") || StringUtils.endsWith(uri.getHost(), ".youtube.com") || StringUtils.endsWith(uri.getHost(), ".youtu.be")) {
                        AbstractWebViewFragment.this.startActivity(new Intent("android.intent.action.VIEW", uri));
                        return true;
                    } else if (StringUtils.isEquals(uri.getHost(), "vimeo.com") || StringUtils.endsWith(uri.getHost(), ".vimeo.com")) {
                        AbstractWebViewFragment.this.startActivity(new Intent("android.intent.action.VIEW", uri));
                        return true;
                    } else if (AbstractWebViewFragment.this.doHandleUri(uri)) {
                        return true;
                    }
                }
                return false;
            }

            @TargetApi(24)
            public boolean shouldOverrideUrlLoading(WebView webView, WebResourceRequest webResourceRequest) {
                if (handleUri(webResourceRequest.getUrl())) {
                    return true;
                }
                return super.shouldOverrideUrlLoading(webView, webResourceRequest);
            }

            public boolean shouldOverrideUrlLoading(WebView webView, String str) {
                if (handleUri(Uri.parse(str))) {
                    return true;
                }
                return super.shouldOverrideUrlLoading(webView, str);
            }

            private void handleError(WebView webView, String str, String str2) {
                if (StringUtils.isEquals(str, this.currentUrl) && NetworkUtils.isNetworkAvailable(webView.getContext())) {
                    try {
                        webView.stopLoading();
                    } catch (Exception e) {
                    }
                    if (AbstractWebViewFragment.this.context != null) {
                        AbstractWebViewFragment.this.doLoadHtml(AbstractWebViewFragment.this.composeHtml(AbstractWebViewFragment.this.context, str2, str), str);
                    }
                }
            }

            public void onPageStarted(WebView webView, String str, Bitmap bitmap) {
                this.currentUrl = str;
                Uri parse = Uri.parse(str);
                if (!(!StringUtils.isEquals(parse.getScheme(), ShareConstants.WEB_DIALOG_PARAM_DATA)) || NetworkUtils.isNetworkAvailable(webView.getContext())) {
                    BackgroundMode backgroundMode = AbstractWebViewFragment.this.getBackgroundMode(parse);
                    if (AbstractWebViewFragment.this.backgroundMode != backgroundMode) {
                        switch (C14634.f1187x21f53947[backgroundMode.ordinal()]) {
                            case 1:
                                webView.setBackgroundColor(0);
                                break;
                            default:
                                webView.setBackgroundColor(-1);
                                break;
                        }
                        AbstractWebViewFragment.this.backgroundMode = backgroundMode;
                    }
                    super.onPageStarted(webView, str, bitmap);
                    return;
                }
                webView.stopLoading();
                if (AbstractWebViewFragment.this.context != null) {
                    AbstractWebViewFragment.this.doLoadHtml(AbstractWebViewFragment.this.composeHtml(AbstractWebViewFragment.this.context, webView.getContext().getResources().getString(C1423R.string.net_gogame_gowrap_network_no_connection_message), ""), str);
                }
            }

            public void onReceivedError(WebView webView, int i, String str, String str2) {
                handleError(webView, str2, String.format(Locale.getDefault(), "%s (%d)", new Object[]{str, Integer.valueOf(i)}));
                super.onReceivedError(webView, i, str, str2);
            }

            @TargetApi(23)
            public void onReceivedError(WebView webView, WebResourceRequest webResourceRequest, WebResourceError webResourceError) {
                handleError(webView, webResourceRequest.getUrl().toString(), String.format(Locale.getDefault(), "%s (%d)", new Object[]{webResourceError.getDescription(), Integer.valueOf(webResourceError.getErrorCode())}));
                super.onReceivedError(webView, webResourceRequest, webResourceError);
            }

            @TargetApi(23)
            public void onReceivedHttpError(WebView webView, WebResourceRequest webResourceRequest, WebResourceResponse webResourceResponse) {
                handleError(webView, webResourceRequest.getUrl().toString(), String.format(Locale.getDefault(), "%d %s", new Object[]{Integer.valueOf(webResourceResponse.getStatusCode()), webResourceResponse.getReasonPhrase()}));
                super.onReceivedHttpError(webView, webResourceRequest, webResourceResponse);
            }

            public void onReceivedSslError(WebView webView, SslErrorHandler sslErrorHandler, SslError sslError) {
                handleError(webView, sslError.getUrl(), webView.getContext().getResources().getString(C1423R.string.net_gogame_gowrap_network_ssl_error_message));
                super.onReceivedSslError(webView, sslErrorHandler, sslError);
            }
        });
        if (this.savedInstanceState != null) {
            this.webView.restoreState(this.savedInstanceState);
        } else {
            init(getArguments());
        }
        return inflate;
    }

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        this.context = activity;
    }

    public void onAttach(Context context2) {
        super.onAttach(context2);
        this.context = context2;
    }

    public void onDetach() {
        super.onDetach();
        destroyWebView();
        this.context = null;
    }

    public boolean onBackPressed() {
        if (this.webChromeClient.onBackPressed()) {
            return true;
        }
        if (!this.webView.canGoBack()) {
            return false;
        }
        this.webView.goBack();
        return true;
    }

    public void onResume() {
        this.webView.onResume();
        super.onResume();
    }

    public void onPause() {
        super.onPause();
        if (this.context instanceof UIContext) {
            ((UIContext) this.context).onLoadingFinished();
        }
        this.webView.onPause();
        Bundle bundle = new Bundle();
        this.webView.saveState(bundle);
        this.savedInstanceState = bundle;
    }

    public void onDestroyView() {
        destroyWebView();
        super.onDestroyView();
    }

    public void onDestroy() {
        destroyWebView();
        super.onDestroy();
    }

    private void destroyWebView() {
        if (this.webView != null) {
            this.webViewIsAvailable = false;
            this.webView.stopLoading();
            this.webView.loadData("<html></html>", "text/plain", "UTF-8");
            this.webView.reload();
            this.webView.loadUrl("about:blank");
            if (this.webView.getParent() instanceof ViewGroup) {
                ((ViewGroup) this.webView.getParent()).removeView(this.webView);
            }
            this.webView.removeAllViews();
            this.webView.destroy();
            this.webView = null;
        }
    }

    /* access modifiers changed from: private */
    public void doLoadHtml(String str, String str2) {
        if (this instanceof InternalWebViewContext) {
            ((InternalWebViewContext) this).loadHtml(str, str2);
        } else if (this.context instanceof UIContext) {
            ((UIContext) this.context).loadHtml(str, str2);
        } else if (str2 != null) {
            this.webView.loadDataWithBaseURL(str2, str, CONTENT_TYPE, "UTF-8", str2);
        } else {
            this.webView.loadData(str, CONTENT_TYPE, "UTF-8");
        }
    }

    /* access modifiers changed from: private */
    public void setProgress(int i) {
        doSetProgress(this.progressBar, i);
        doSetProgress(this.progressBar2, i);
    }

    private void doSetProgress(ProgressBar progressBar3, int i) {
        if (progressBar3 != null) {
            if (i != 100) {
                if (progressBar3.getVisibility() != 0) {
                    progressBar3.setVisibility(0);
                }
                progressBar3.setProgress(i);
            } else if (progressBar3.getVisibility() != 8) {
                progressBar3.setVisibility(8);
            }
        }
    }
}
