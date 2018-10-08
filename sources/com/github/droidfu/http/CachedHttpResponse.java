package com.github.droidfu.http;

import com.github.droidfu.cachefu.HttpResponseCache;
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import org.apache.http.HttpResponse;

public class CachedHttpResponse implements BetterHttpResponse {
    private ResponseData cachedData;
    private HttpResponseCache responseCache = BetterHttp.getResponseCache();

    public static final class ResponseData {
        private byte[] responseBody;
        private int statusCode;

        public ResponseData(int i, byte[] bArr) {
            this.statusCode = i;
            this.responseBody = bArr;
        }

        public byte[] getResponseBody() {
            return this.responseBody;
        }

        public int getStatusCode() {
            return this.statusCode;
        }
    }

    public CachedHttpResponse(String str) {
        this.cachedData = (ResponseData) this.responseCache.get(str);
    }

    public String getHeader(String str) {
        return null;
    }

    public InputStream getResponseBody() throws IOException {
        return new ByteArrayInputStream(this.cachedData.responseBody);
    }

    public byte[] getResponseBodyAsBytes() throws IOException {
        return this.cachedData.responseBody;
    }

    public String getResponseBodyAsString() throws IOException {
        return new String(this.cachedData.responseBody);
    }

    public int getStatusCode() {
        return this.cachedData.statusCode;
    }

    public HttpResponse unwrap() {
        return null;
    }
}
