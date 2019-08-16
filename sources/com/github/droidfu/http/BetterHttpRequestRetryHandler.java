package com.github.droidfu.http;

import android.os.SystemClock;
import android.util.Log;
import java.io.IOException;
import java.io.InterruptedIOException;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.HashSet;
import javax.net.ssl.SSLHandshakeException;
import org.apache.http.NoHttpResponseException;
import org.apache.http.client.HttpRequestRetryHandler;
import org.apache.http.protocol.HttpContext;

public class BetterHttpRequestRetryHandler implements HttpRequestRetryHandler {
    private static final int RETRY_SLEEP_TIME_MILLIS = 1500;
    private static HashSet<Class<?>> exceptionBlacklist = new HashSet<>();
    private static HashSet<Class<?>> exceptionWhitelist = new HashSet<>();
    private int maxRetries;
    private int timesRetried;

    static {
        exceptionWhitelist.add(NoHttpResponseException.class);
        exceptionWhitelist.add(UnknownHostException.class);
        exceptionWhitelist.add(SocketException.class);
        exceptionBlacklist.add(InterruptedIOException.class);
        exceptionBlacklist.add(SSLHandshakeException.class);
    }

    public BetterHttpRequestRetryHandler(int i) {
        this.maxRetries = i;
    }

    public int getTimesRetried() {
        return this.timesRetried;
    }

    public boolean retryRequest(IOException iOException, int i, HttpContext httpContext) {
        boolean z = false;
        this.timesRetried = i;
        Boolean bool = (Boolean) httpContext.getAttribute("http.request_sent");
        boolean z2 = bool != null && bool.booleanValue();
        if (i <= this.maxRetries && !exceptionBlacklist.contains(iOException.getClass())) {
            if (exceptionWhitelist.contains(iOException.getClass())) {
                z = true;
            } else if (!z2) {
                z = true;
            }
        }
        if (z) {
            Log.e("BetterHttp", "request failed (" + iOException.getClass().getCanonicalName() + ": " + iOException.getMessage() + " / attempt " + i + "), will retry in " + 1.5d + " seconds");
            SystemClock.sleep(1500);
        } else {
            Log.e("BetterHttp", "request failed after " + i + " attempts");
            iOException.printStackTrace();
        }
        return z;
    }
}
