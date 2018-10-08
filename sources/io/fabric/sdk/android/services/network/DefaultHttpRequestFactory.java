package io.fabric.sdk.android.services.network;

import io.fabric.sdk.android.DefaultLogger;
import io.fabric.sdk.android.Logger;
import java.util.Collections;
import java.util.Locale;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLSocketFactory;

public class DefaultHttpRequestFactory implements HttpRequestFactory {
    private static final String HTTPS = "https";
    private boolean attemptedSslInit;
    private final Logger logger;
    private PinningInfoProvider pinningInfo;
    private SSLSocketFactory sslSocketFactory;

    public DefaultHttpRequestFactory() {
        this(new DefaultLogger());
    }

    public DefaultHttpRequestFactory(Logger logger) {
        this.logger = logger;
    }

    private SSLSocketFactory getSSLSocketFactory() {
        SSLSocketFactory sSLSocketFactory;
        synchronized (this) {
            if (this.sslSocketFactory == null && !this.attemptedSslInit) {
                this.sslSocketFactory = initSSLSocketFactory();
            }
            sSLSocketFactory = this.sslSocketFactory;
        }
        return sSLSocketFactory;
    }

    private SSLSocketFactory initSSLSocketFactory() {
        SSLSocketFactory sSLSocketFactory;
        synchronized (this) {
            this.attemptedSslInit = true;
            try {
                sSLSocketFactory = NetworkUtils.getSSLSocketFactory(this.pinningInfo);
                this.logger.mo4753d("Fabric", "Custom SSL pinning enabled");
            } catch (Throwable e) {
                this.logger.mo4756e("Fabric", "Exception while validating pinned certs", e);
                sSLSocketFactory = null;
            }
        }
        return sSLSocketFactory;
    }

    private boolean isHttps(String str) {
        return str != null && str.toLowerCase(Locale.US).startsWith(HTTPS);
    }

    private void resetSSLSocketFactory() {
        synchronized (this) {
            this.attemptedSslInit = false;
            this.sslSocketFactory = null;
        }
    }

    public HttpRequest buildHttpRequest(HttpMethod httpMethod, String str) {
        return buildHttpRequest(httpMethod, str, Collections.emptyMap());
    }

    public HttpRequest buildHttpRequest(HttpMethod httpMethod, String str, Map<String, String> map) {
        HttpRequest httpRequest;
        switch (httpMethod) {
            case GET:
                httpRequest = HttpRequest.get((CharSequence) str, (Map) map, true);
                break;
            case POST:
                httpRequest = HttpRequest.post((CharSequence) str, (Map) map, true);
                break;
            case PUT:
                httpRequest = HttpRequest.put((CharSequence) str);
                break;
            case DELETE:
                httpRequest = HttpRequest.delete((CharSequence) str);
                break;
            default:
                throw new IllegalArgumentException("Unsupported HTTP method!");
        }
        if (isHttps(str) && this.pinningInfo != null) {
            SSLSocketFactory sSLSocketFactory = getSSLSocketFactory();
            if (sSLSocketFactory != null) {
                ((HttpsURLConnection) httpRequest.getConnection()).setSSLSocketFactory(sSLSocketFactory);
            }
        }
        return httpRequest;
    }

    public PinningInfoProvider getPinningInfoProvider() {
        return this.pinningInfo;
    }

    public void setPinningInfoProvider(PinningInfoProvider pinningInfoProvider) {
        if (this.pinningInfo != pinningInfoProvider) {
            this.pinningInfo = pinningInfoProvider;
            resetSSLSocketFactory();
        }
    }
}
