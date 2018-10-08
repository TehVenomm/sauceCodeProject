package net.gogame.gowrap.ui.cpr;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.util.AttributeSet;
import android.util.Log;
import android.view.GestureDetector;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import java.util.Map;

public class VideoEnabledWebView extends WebView {
    private boolean addedJavascriptInterface = false;
    private final GestureDetector gestureDetector = new GestureDetector(new CustomGestureDetector());
    private VideoEnabledWebChromeClient videoEnabledWebChromeClient;

    private class CustomGestureDetector extends SimpleOnGestureListener {
        private CustomGestureDetector() {
        }

        public boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
            return true;
        }
    }

    public class JavascriptInterface {

        /* renamed from: net.gogame.gowrap.ui.cpr.VideoEnabledWebView$JavascriptInterface$1 */
        class C11371 implements Runnable {
            C11371() {
            }

            public void run() {
                if (VideoEnabledWebView.this.videoEnabledWebChromeClient != null) {
                    VideoEnabledWebView.this.videoEnabledWebChromeClient.onHideCustomView();
                }
            }
        }

        @android.webkit.JavascriptInterface
        public void notifyVideoEnd() {
            Log.d("___", "GOT IT");
            new Handler(Looper.getMainLooper()).post(new C11371());
        }
    }

    public VideoEnabledWebView(Context context) {
        super(context);
    }

    public VideoEnabledWebView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public VideoEnabledWebView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    public boolean isVideoFullscreen() {
        return this.videoEnabledWebChromeClient != null && this.videoEnabledWebChromeClient.isVideoFullscreen();
    }

    @SuppressLint({"SetJavaScriptEnabled"})
    public void setWebChromeClient(WebChromeClient webChromeClient) {
        getSettings().setJavaScriptEnabled(true);
        if (webChromeClient instanceof VideoEnabledWebChromeClient) {
            this.videoEnabledWebChromeClient = (VideoEnabledWebChromeClient) webChromeClient;
        }
        super.setWebChromeClient(webChromeClient);
    }

    public void loadData(String str, String str2, String str3) {
        addJavascriptInterface();
        super.loadData(str, str2, str3);
    }

    public void loadDataWithBaseURL(String str, String str2, String str3, String str4, String str5) {
        addJavascriptInterface();
        super.loadDataWithBaseURL(str, str2, str3, str4, str5);
    }

    public void loadUrl(String str) {
        addJavascriptInterface();
        super.loadUrl(str);
    }

    public void loadUrl(String str, Map<String, String> map) {
        addJavascriptInterface();
        super.loadUrl(str, map);
    }

    private void addJavascriptInterface() {
        if (!this.addedJavascriptInterface) {
            addJavascriptInterface(new JavascriptInterface(), "_VideoEnabledWebView");
            this.addedJavascriptInterface = true;
        }
    }

    public boolean onTouchEvent(MotionEvent motionEvent) {
        return this.gestureDetector.onTouchEvent(motionEvent) || super.onTouchEvent(motionEvent);
    }
}
