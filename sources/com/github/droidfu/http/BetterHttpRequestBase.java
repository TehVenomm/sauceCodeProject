package com.github.droidfu.http;

import android.util.Log;
import com.github.droidfu.cachefu.HttpResponseCache;
import com.github.droidfu.http.CachedHttpResponse.ResponseData;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpResponseException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.AbstractHttpClient;
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
            BetterHttpResponseImpl betterHttpResponseImpl = new BetterHttpResponseImpl(httpResponse);
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

    /* JADX WARNING: Removed duplicated region for block: B:28:0x007e  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.github.droidfu.http.BetterHttpResponse send() throws java.net.ConnectException {
        /*
            r6 = this;
            com.github.droidfu.http.BetterHttpRequestRetryHandler r2 = new com.github.droidfu.http.BetterHttpRequestRetryHandler
            int r0 = r6.maxRetries
            r2.<init>(r0)
            org.apache.http.impl.client.AbstractHttpClient r0 = r6.httpClient
            r0.setHttpRequestRetryHandler(r2)
            org.apache.http.protocol.BasicHttpContext r3 = new org.apache.http.protocol.BasicHttpContext
            r3.<init>()
            r1 = 1
            r0 = 0
        L_0x0013:
            if (r1 != 0) goto L_0x001e
            java.net.ConnectException r1 = new java.net.ConnectException
            r1.<init>()
            r1.initCause(r0)
            throw r1
        L_0x001e:
            org.apache.http.impl.client.AbstractHttpClient r0 = r6.httpClient     // Catch:{ IOException -> 0x0036, NullPointerException -> 0x0049 }
            org.apache.http.client.methods.HttpUriRequest r1 = r6.request     // Catch:{ IOException -> 0x0036, NullPointerException -> 0x0049 }
            java.lang.Object r0 = r0.execute(r1, r6, r3)     // Catch:{ IOException -> 0x0036, NullPointerException -> 0x0049 }
            com.github.droidfu.http.BetterHttpResponse r0 = (com.github.droidfu.http.BetterHttpResponse) r0     // Catch:{ IOException -> 0x0036, NullPointerException -> 0x0049 }
            int r1 = r6.oldTimeout
            int r2 = com.github.droidfu.http.BetterHttp.getSocketTimeout()
            if (r1 == r2) goto L_0x0035
            int r1 = r6.oldTimeout
            com.github.droidfu.http.BetterHttp.setSocketTimeout(r1)
        L_0x0035:
            return r0
        L_0x0036:
            r0 = move-exception
            boolean r1 = r6.retryRequest(r2, r0, r3)     // Catch:{ all -> 0x0075 }
            int r4 = r6.oldTimeout
            int r5 = com.github.droidfu.http.BetterHttp.getSocketTimeout()
            if (r4 == r5) goto L_0x0013
            int r4 = r6.oldTimeout
            com.github.droidfu.http.BetterHttp.setSocketTimeout(r4)
            goto L_0x0013
        L_0x0049:
            r0 = move-exception
            r1 = r0
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ all -> 0x0084 }
            java.lang.String r0 = "NPE in HttpClient"
            r4.<init>(r0)     // Catch:{ all -> 0x0084 }
            java.io.IOException r0 = new java.io.IOException     // Catch:{ all -> 0x0084 }
            java.lang.String r1 = r1.getMessage()     // Catch:{ all -> 0x0084 }
            java.lang.StringBuilder r1 = r4.append(r1)     // Catch:{ all -> 0x0084 }
            java.lang.String r1 = r1.toString()     // Catch:{ all -> 0x0084 }
            r0.<init>(r1)     // Catch:{ all -> 0x0084 }
            boolean r1 = r6.retryRequest(r2, r0, r3)     // Catch:{ all -> 0x0075 }
            int r4 = r6.oldTimeout
            int r5 = com.github.droidfu.http.BetterHttp.getSocketTimeout()
            if (r4 == r5) goto L_0x0013
            int r4 = r6.oldTimeout
            com.github.droidfu.http.BetterHttp.setSocketTimeout(r4)
            goto L_0x0013
        L_0x0075:
            r0 = move-exception
        L_0x0076:
            int r1 = r6.oldTimeout
            int r2 = com.github.droidfu.http.BetterHttp.getSocketTimeout()
            if (r1 == r2) goto L_0x0083
            int r1 = r6.oldTimeout
            com.github.droidfu.http.BetterHttp.setSocketTimeout(r1)
        L_0x0083:
            throw r0
        L_0x0084:
            r0 = move-exception
            goto L_0x0076
        */
        throw new UnsupportedOperationException("Method not decompiled: com.github.droidfu.http.BetterHttpRequestBase.send():com.github.droidfu.http.BetterHttpResponse");
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
