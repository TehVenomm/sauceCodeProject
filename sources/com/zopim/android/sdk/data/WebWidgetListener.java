package com.zopim.android.sdk.data;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import android.util.Log;
import android.webkit.JavascriptInterface;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

public class WebWidgetListener extends WebViewClient {
    private static final boolean DEBUG = false;
    private static final String DELIMITER = "z://";
    private static final String ENCODING = "utf-8";
    private static final String LOG_TAG = WebWidgetListener.class.getSimpleName();
    private static final Executor MY_EXECUTOR = new C0873k(Executors.newCachedThreadPool());

    @TargetApi(11)
    private void executePathUpdate(C0871i c0871i, String str) {
        try {
            if (VERSION.SDK_INT >= 11) {
                c0871i.executeOnExecutor(MY_EXECUTOR, new String[]{str});
                return;
            }
            c0871i.execute(new String[]{str});
        } catch (IllegalStateException e) {
            Log.w(LOG_TAG, "Could not execute path update due to a state error");
            e.printStackTrace();
        }
    }

    @JavascriptInterface
    public void msg(String str) {
        executePathUpdate(new C0871i(), str);
    }

    public boolean shouldOverrideUrlLoading(WebView webView, String str) {
        if (str == null || !str.contains(DELIMITER)) {
            Log.d(LOG_TAG, "Not interested in handling URL loading");
        } else {
            try {
                String decode;
                try {
                    decode = URLDecoder.decode(str.substring(str.indexOf(DELIMITER) + DELIMITER.length()), ENCODING);
                } catch (UnsupportedEncodingException e) {
                    Log.e(LOG_TAG, "Error encoding " + decode);
                    e.printStackTrace();
                }
                executePathUpdate(new C0871i(), decode);
            } catch (IndexOutOfBoundsException e2) {
                Log.w(LOG_TAG, "Error parsing url. " + e2.getMessage());
            }
        }
        return true;
    }
}
