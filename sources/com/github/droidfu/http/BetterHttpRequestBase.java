package com.github.droidfu.http;

import android.util.Log;
import com.github.droidfu.cachefu.HttpResponseCache;
import com.github.droidfu.http.CachedHttpResponse.ResponseData;
import java.io.IOException;
import java.net.ConnectException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.AbstractHttpClient;
import org.apache.http.protocol.BasicHttpContext;
import org.apache.http.protocol.HttpContext;

public abstract class BetterHttpRequestBase implements BetterHttpRequest, ResponseHandler<BetterHttpResponse> {
    protected static final String HTTP_CONTENT_TYPE_HEADER = "Content-Type";
    private static final int MAX_RETRIES = 5;
    private int executionCount;
    protected List<Integer> expectedStatusCodes = new ArrayList();
    protected AbstractHttpClient httpClient;
    protected int maxRetries = 5;
    private int oldTimeout;
    protected HttpUriRequest request;

    BetterHttpRequestBase(AbstractHttpClient abstractHttpClient) {
        this.httpClient = abstractHttpClient;
    }

    private boolean retryRequest(BetterHttpRequestRetryHandler betterHttpRequestRetryHandler, IOException iOException, HttpContext httpContext) {
        Log.e("BetterHttp", "Intercepting exception that wasn't handled by HttpClient");
        this.executionCount = Math.max(this.executionCount, betterHttpRequestRetryHandler.getTimesRetried());
        int i = this.executionCount + 1;
        this.executionCount = i;
        return betterHttpRequestRetryHandler.retryRequest(iOException, i, httpContext);
    }

    public BetterHttpRequestBase expecting(Integer... numArr) {
        this.expectedStatusCodes = Arrays.asList(numArr);
        return this;
    }

    public String getRequestUrl() {
        return this.request.getURI().toString();
    }

    public BetterHttpResponse handleResponse(HttpResponse httpResponse) throws IOException {
        int statusCode = httpResponse.getStatusLine().getStatusCode();
        if (this.expectedStatusCodes == null || this.expectedStatusCodes.isEmpty() || this.expectedStatusCodes.contains(Integer.valueOf(statusCode))) {
            BetterHttpResponse betterHttpResponseImpl = new BetterHttpResponseImpl(httpResponse);
            HttpResponseCache responseCache = BetterHttp.getResponseCache();
            if (responseCache != null) {
                responseCache.put(getRequestUrl(), new ResponseData(statusCode, betterHttpResponseImpl.getResponseBodyAsBytes()));
            }
            return betterHttpResponseImpl;
        }
        throw new HttpResponseException(statusCode, "Unexpected status code: " + statusCode);
    }

    public BetterHttpRequestBase retries(int i) {
        if (i < 0) {
            this.maxRetries = 0;
        } else if (i > 5) {
            this.maxRetries = 5;
        } else {
            this.maxRetries = i;
        }
        return this;
    }

    public BetterHttpResponse send() throws ConnectException {
        BetterHttpRequestRetryHandler betterHttpRequestRetryHandler = new BetterHttpRequestRetryHandler(this.maxRetries);
        this.httpClient.setHttpRequestRetryHandler(betterHttpRequestRetryHandler);
        HttpContext basicHttpContext = new BasicHttpContext();
        boolean z = true;
        Throwable th = null;
        while (z) {
            try {
                BetterHttpResponse betterHttpResponse = (BetterHttpResponse) this.httpClient.execute(this.request, this, basicHttpContext);
                if (this.oldTimeout != BetterHttp.getSocketTimeout()) {
                    BetterHttp.setSocketTimeout(this.oldTimeout);
                }
                return betterHttpResponse;
            } catch (IOException e) {
                th = e;
                z = retryRequest(betterHttpRequestRetryHandler, th, basicHttpContext);
                if (this.oldTimeout != BetterHttp.getSocketTimeout()) {
                    BetterHttp.setSocketTimeout(this.oldTimeout);
                }
            } catch (NullPointerException e2) {
                th = new IOException("NPE in HttpClient" + e2.getMessage());
                z = retryRequest(betterHttpRequestRetryHandler, th, basicHttpContext);
                if (this.oldTimeout != BetterHttp.getSocketTimeout()) {
                    BetterHttp.setSocketTimeout(this.oldTimeout);
                }
            } catch (Throwable th2) {
                th = th2;
            }
        }
        ConnectException connectException = new ConnectException();
        connectException.initCause(th);
        throw connectException;
        if (this.oldTimeout != BetterHttp.getSocketTimeout()) {
            BetterHttp.setSocketTimeout(this.oldTimeout);
        }
        throw th;
    }

    public HttpUriRequest unwrap() {
        return this.request;
    }

    public BetterHttpRequest withTimeout(int i) {
        this.oldTimeout = this.httpClient.getParams().getIntParameter("http.socket.timeout", 30000);
        BetterHttp.setSocketTimeout(i);
        return this;
    }
}
