package com.github.droidfu.http;

import android.content.Context;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Proxy;
import android.util.Log;
import com.github.droidfu.cachefu.HttpResponseCache;
import com.github.droidfu.http.ssl.EasySSLSocketFactory;
import com.github.droidfu.support.DiagnosticSupport;
import java.util.HashMap;
import org.apache.http.HttpEntity;
import org.apache.http.HttpHost;
import org.apache.http.HttpVersion;
import org.apache.http.conn.params.ConnManagerParams;
import org.apache.http.conn.params.ConnPerRouteBean;
import org.apache.http.conn.scheme.PlainSocketFactory;
import org.apache.http.conn.scheme.Scheme;
import org.apache.http.conn.scheme.SchemeRegistry;
import org.apache.http.conn.ssl.SSLSocketFactory;
import org.apache.http.impl.client.AbstractHttpClient;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.impl.conn.tsccm.ThreadSafeClientConnManager;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import org.apache.http.params.HttpProtocolParams;

public class BetterHttp {
    public static final String DEFAULT_HTTP_USER_AGENT = "Android/DroidFu";
    public static final int DEFAULT_MAX_CONNECTIONS = 4;
    public static final int DEFAULT_SOCKET_TIMEOUT = 30000;
    static final String LOG_TAG = "BetterHttp";
    private static Context appContext;
    private static HashMap<String, String> defaultHeaders = new HashMap();
    private static AbstractHttpClient httpClient;
    private static int maxConnections = 4;
    private static HttpResponseCache responseCache;
    private static int socketTimeout = 30000;

    public static BetterHttpRequest delete(String str) {
        return new HttpDelete(httpClient, str, defaultHeaders);
    }

    public static void enableResponseCache(int i, long j, int i2) {
        responseCache = new HttpResponseCache(i, j, i2);
    }

    public static void enableResponseCache(Context context, int i, long j, int i2, int i3) {
        enableResponseCache(i, j, i2);
        responseCache.enableDiskCache(context, i3);
    }

    public static BetterHttpRequest get(String str) {
        return get(str, false);
    }

    public static BetterHttpRequest get(String str, boolean z) {
        return (z && responseCache != null && responseCache.containsKey(str)) ? new CachedHttpRequest(str) : new HttpGet(httpClient, str, defaultHeaders);
    }

    public static HashMap<String, String> getDefaultHeaders() {
        return defaultHeaders;
    }

    public static AbstractHttpClient getHttpClient() {
        return httpClient;
    }

    public static HttpResponseCache getResponseCache() {
        return responseCache;
    }

    public static int getSocketTimeout() {
        return socketTimeout;
    }

    public static BetterHttpRequest post(String str) {
        return new HttpPost(httpClient, str, defaultHeaders);
    }

    public static BetterHttpRequest post(String str, HttpEntity httpEntity) {
        return new HttpPost(httpClient, str, httpEntity, defaultHeaders);
    }

    public static BetterHttpRequest put(String str) {
        return new HttpPut(httpClient, str, defaultHeaders);
    }

    public static BetterHttpRequest put(String str, HttpEntity httpEntity) {
        return new HttpPut(httpClient, str, httpEntity, defaultHeaders);
    }

    public static void setContext(Context context) {
        if (appContext == null) {
            appContext = context.getApplicationContext();
            appContext.registerReceiver(new ConnectionChangedBroadcastReceiver(), new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
        }
    }

    public static void setDefaultHeader(String str, String str2) {
        defaultHeaders.put(str, str2);
    }

    public static void setHttpClient(AbstractHttpClient abstractHttpClient) {
        httpClient = abstractHttpClient;
    }

    public static void setMaximumConnections(int i) {
        maxConnections = i;
    }

    public static void setPortForScheme(String str, int i) {
        httpClient.getConnectionManager().getSchemeRegistry().register(new Scheme(str, PlainSocketFactory.getSocketFactory(), i));
    }

    public static void setSocketTimeout(int i) {
        socketTimeout = i;
        HttpConnectionParams.setSoTimeout(httpClient.getParams(), i);
    }

    public static void setupHttpClient() {
        HttpParams basicHttpParams = new BasicHttpParams();
        ConnManagerParams.setTimeout(basicHttpParams, (long) socketTimeout);
        ConnManagerParams.setMaxConnectionsPerRoute(basicHttpParams, new ConnPerRouteBean(maxConnections));
        ConnManagerParams.setMaxTotalConnections(basicHttpParams, 4);
        HttpConnectionParams.setSoTimeout(basicHttpParams, socketTimeout);
        HttpConnectionParams.setTcpNoDelay(basicHttpParams, true);
        HttpProtocolParams.setVersion(basicHttpParams, HttpVersion.HTTP_1_1);
        HttpProtocolParams.setUserAgent(basicHttpParams, DEFAULT_HTTP_USER_AGENT);
        SchemeRegistry schemeRegistry = new SchemeRegistry();
        schemeRegistry.register(new Scheme("http", PlainSocketFactory.getSocketFactory(), 80));
        if (DiagnosticSupport.ANDROID_API_LEVEL >= 7) {
            schemeRegistry.register(new Scheme("https", SSLSocketFactory.getSocketFactory(), 443));
        } else {
            schemeRegistry.register(new Scheme("https", new EasySSLSocketFactory(), 443));
        }
        httpClient = new DefaultHttpClient(new ThreadSafeClientConnManager(basicHttpParams, schemeRegistry), basicHttpParams);
    }

    public static void updateProxySettings() {
        if (appContext != null) {
            HttpParams params = httpClient.getParams();
            NetworkInfo activeNetworkInfo = ((ConnectivityManager) appContext.getSystemService("connectivity")).getActiveNetworkInfo();
            if (activeNetworkInfo != null) {
                Log.i(LOG_TAG, activeNetworkInfo.toString());
                if (activeNetworkInfo.getType() == 0) {
                    String host = Proxy.getHost(appContext);
                    if (host == null) {
                        host = Proxy.getDefaultHost();
                    }
                    int port = Proxy.getPort(appContext);
                    if (port == -1) {
                        port = Proxy.getDefaultPort();
                    }
                    if (host == null || port <= -1) {
                        params.setParameter("http.route.default-proxy", null);
                        return;
                    } else {
                        params.setParameter("http.route.default-proxy", new HttpHost(host, port));
                        return;
                    }
                }
                params.setParameter("http.route.default-proxy", null);
            }
        }
    }
}
