package net.gree.unitywebview;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.view.ViewGroup.LayoutParams;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import com.appsflyer.share.Constants;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;

public class WebViewPlugin {
    private int bottomMargin;
    private HashMap<String, ArrayList<String>> cookies;
    private boolean isDestroyed;
    private FrameLayout layout = null;
    private int leftMargin;
    private long mDownTime;
    private WebView mWebView;
    private int rightMargin;
    private int topMargin;

    /* renamed from: net.gree.unitywebview.WebViewPlugin$2 */
    class C15772 implements Runnable {
        C15772() {
        }

        public void run() {
            if (WebViewPlugin.this.mWebView != null) {
                WebViewPlugin.this.layout.removeView(WebViewPlugin.this.mWebView);
                WebViewPlugin.this.mWebView = null;
            }
            if (WebViewPlugin.this.layout != null) {
                WebViewPlugin.this.layout = null;
            }
        }
    }

    public void Destroy() {
        this.isDestroyed = true;
        UnityPlayer.currentActivity.runOnUiThread(new C15772());
    }

    public void EvaluateJS(final String str) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.this.mWebView.loadUrl("javascript:" + str);
                }
            }
        });
    }

    public void Init(final String str) {
        final Activity activity = UnityPlayer.currentActivity;
        this.cookies = new HashMap();
        activity.runOnUiThread(new Runnable() {
            public void run() {
                WebViewPlugin.this.mWebView = new WebView(activity);
                WebViewPlugin.this.mWebView.setVisibility(8);
                WebViewPlugin.this.mWebView.setFocusable(true);
                WebViewPlugin.this.mWebView.setFocusableInTouchMode(true);
                if (WebViewPlugin.this.layout == null) {
                    WebViewPlugin.this.layout = new FrameLayout(activity);
                    activity.addContentView(WebViewPlugin.this.layout, new LayoutParams(-1, -1));
                    WebViewPlugin.this.layout.setFocusable(true);
                    WebViewPlugin.this.layout.setFocusableInTouchMode(true);
                }
                WebViewPlugin.this.layout.addView(WebViewPlugin.this.mWebView, new FrameLayout.LayoutParams(-1, -1, 0));
                WebViewPlugin.this.mWebView.setWebChromeClient(new WebChromeClient());
                WebView access$1 = WebViewPlugin.this.mWebView;
                final String str = str;
                access$1.setWebViewClient(new WebViewClient() {
                    void ChangeActivity(String str) {
                        if (!WebViewPlugin.this.isDestroyed) {
                            Activity activity = UnityPlayer.currentActivity;
                            Intent intent = new Intent();
                            intent.putExtra("url", str);
                            intent.putExtra("gameobject", str);
                            intent.putExtra("margin_left", WebViewPlugin.this.leftMargin);
                            intent.putExtra("margin_right", WebViewPlugin.this.rightMargin);
                            intent.putExtra("margin_top", WebViewPlugin.this.topMargin);
                            intent.putExtra("margin_bottom", WebViewPlugin.this.bottomMargin);
                            String replace = str.substring(0, str.indexOf(Constants.URL_PATH_DELIMITER, 10)).replace("http://", "").replace("https://", "");
                            String str2 = "";
                            for (Entry entry : WebViewPlugin.this.cookies.entrySet()) {
                                if (((String) entry.getKey()).indexOf(replace) >= 0) {
                                    Iterator it = ((ArrayList) entry.getValue()).iterator();
                                    String str3 = str2;
                                    while (it.hasNext()) {
                                        str3 = new StringBuilder(String.valueOf(str3)).append((String) it.next()).append(";").toString();
                                    }
                                    str2 = str3;
                                }
                            }
                            intent.putExtra("cookie", str2);
                            intent.setFlags(65536);
                            intent.setClassName("jp.colopl.wcat", "jp.colopl.wcat.WebViewActivity");
                            activity.startActivity(intent);
                        }
                    }

                    boolean isWebViewActivityUrl(String str) {
                        return str.indexOf("/opinion") >= 0;
                    }

                    public void onLoadResource(WebView webView, String str) {
                        if (isWebViewActivityUrl(str)) {
                            ChangeActivity(str);
                        } else {
                            super.onLoadResource(webView, str);
                        }
                    }

                    public void onPageStarted(WebView webView, String str, Bitmap bitmap) {
                        if (isWebViewActivityUrl(str)) {
                            webView.stopLoading();
                            ChangeActivity(str);
                            return;
                        }
                        super.onPageStarted(webView, str, bitmap);
                    }

                    public boolean shouldOverrideUrlLoading(WebView webView, String str) {
                        if (!isWebViewActivityUrl(str)) {
                            return false;
                        }
                        ChangeActivity(str);
                        return true;
                    }
                });
                WebViewPlugin.this.mWebView.addJavascriptInterface(new WebViewPluginInterface(str), "Unity");
                WebSettings settings = WebViewPlugin.this.mWebView.getSettings();
                settings.setSupportZoom(false);
                settings.setJavaScriptEnabled(true);
                settings.setPluginsEnabled(true);
            }
        });
    }

    public void LoadURL(final String str) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.this.mWebView.loadUrl(str);
                }
            }
        });
    }

    public void SetCookie(String str, String str2) {
        CookieSyncManager.createInstance(UnityPlayer.currentActivity);
        CookieManager.getInstance().setCookie(str, str2);
        CookieSyncManager.getInstance().sync();
        if (this.cookies.containsKey(str)) {
            ((ArrayList) this.cookies.get(str)).add(str2);
            return;
        }
        ArrayList arrayList = new ArrayList();
        arrayList.add(str2);
        this.cookies.put(str, arrayList);
    }

    public void SetMargins(int i, int i2, int i3, int i4) {
        this.leftMargin = i;
        this.topMargin = i2;
        this.rightMargin = i3;
        this.bottomMargin = i4;
        final FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(-1, -1, 0);
        layoutParams.setMargins(i, i2, i3, i4);
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.this.mWebView.setLayoutParams(layoutParams);
                }
            }
        });
    }

    public void SetVisibility(final boolean z) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView == null) {
                    return;
                }
                if (z) {
                    WebViewPlugin.this.mWebView.setVisibility(0);
                    WebViewPlugin.this.layout.requestFocus();
                    WebViewPlugin.this.mWebView.requestFocus();
                    return;
                }
                WebViewPlugin.this.mWebView.setVisibility(8);
            }
        });
    }
}
