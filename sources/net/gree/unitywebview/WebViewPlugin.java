package net.gree.unitywebview;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.view.ViewGroup.LayoutParams;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebSettings.PluginState;
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
    /* access modifiers changed from: private */
    public int bottomMargin;
    /* access modifiers changed from: private */
    public HashMap<String, ArrayList<String>> cookies;
    /* access modifiers changed from: private */
    public boolean isDestroyed;
    /* access modifiers changed from: private */
    public FrameLayout layout = null;
    /* access modifiers changed from: private */
    public int leftMargin;
    private long mDownTime;
    /* access modifiers changed from: private */
    public WebView mWebView;
    /* access modifiers changed from: private */
    public int rightMargin;
    /* access modifiers changed from: private */
    public int topMargin;

    public void Destroy() {
        this.isDestroyed = true;
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.this.layout.removeView(WebViewPlugin.this.mWebView);
                    WebViewPlugin.this.mWebView = null;
                }
                if (WebViewPlugin.this.layout != null) {
                    WebViewPlugin.this.layout = null;
                }
            }
        });
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
        this.cookies = new HashMap<>();
        activity.runOnUiThread(new Runnable() {
            @SuppressLint({"WrongConstant"})
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
                WebViewPlugin.this.mWebView.setWebViewClient(new WebViewClient() {
                    /* access modifiers changed from: 0000 */
                    @SuppressLint({"WrongConstant"})
                    public void ChangeActivity(String str) {
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
                                        str3 = str3 + it.next() + ";";
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

                    /* access modifiers changed from: 0000 */
                    public boolean isWebViewActivityUrl(String str) {
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
                settings.setPluginState(PluginState.ON);
            }
        });
    }

    public void LoadURL(final String str) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.this.mWebView.getSettings().setDefaultTextEncodingName("utf-8");
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
            @SuppressLint({"WrongConstant"})
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
