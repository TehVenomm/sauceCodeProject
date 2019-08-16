package p017io.fabric.sdk.android.services.network;

import java.util.Collections;
import java.util.Locale;
import java.util.Map;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLSocketFactory;
import p017io.fabric.sdk.android.DefaultLogger;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Logger;

/* renamed from: io.fabric.sdk.android.services.network.DefaultHttpRequestFactory */
public class DefaultHttpRequestFactory implements HttpRequestFactory {
    private static final String HTTPS = "https";
    private boolean attemptedSslInit;
    private final Logger logger;
    private PinningInfoProvider pinningInfo;
    private SSLSocketFactory sslSocketFactory;

    public DefaultHttpRequestFactory() {
        this(new DefaultLogger());
    }

    public DefaultHttpRequestFactory(Logger logger2) {
        this.logger = logger2;
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
                this.logger.mo20969d(Fabric.TAG, "Custom SSL pinning enabled");
            } catch (Exception e) {
                this.logger.mo20972e(Fabric.TAG, "Exception while validating pinned certs", e);
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
        HttpRequest delete;
        switch (httpMethod) {
            case GET:
                delete = HttpRequest.get((CharSequence) str, map, true);
                break;
            case POST:
                delete = HttpRequest.post((CharSequence) str, map, true);
                break;
            case PUT:
                delete = HttpRequest.put((CharSequence) str);
                break;
            case DELETE:
                delete = HttpRequest.delete((CharSequence) str);
                break;
            default:
                throw new IllegalArgumentException("Unsupported HTTP method!");
        }
        if (isHttps(str) && this.pinningInfo != null) {
            SSLSocketFactory sSLSocketFactory = getSSLSocketFactory();
            if (sSLSocketFactory != null) {
                ((HttpsURLConnection) delete.getConnection()).setSSLSocketFactory(sSLSocketFactory);
            }
        }
        return delete;
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
