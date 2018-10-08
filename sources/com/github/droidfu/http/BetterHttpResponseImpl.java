package com.github.droidfu.http;

import java.io.IOException;
import java.io.InputStream;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.entity.BufferedHttpEntity;
import org.apache.http.util.EntityUtils;

public class BetterHttpResponseImpl implements BetterHttpResponse {
    private HttpEntity entity;
    private HttpResponse response;

    public BetterHttpResponseImpl(HttpResponse httpResponse) throws IOException {
        this.response = httpResponse;
        HttpEntity entity = httpResponse.getEntity();
        if (entity != null) {
            this.entity = new BufferedHttpEntity(entity);
        }
    }

    public String getHeader(String str) {
        return !this.response.containsHeader(str) ? null : this.response.getFirstHeader(str).getValue();
    }

    public InputStream getResponseBody() throws IOException {
        return this.entity.getContent();
    }

    public byte[] getResponseBodyAsBytes() throws IOException {
        return EntityUtils.toByteArray(this.entity);
    }

    public String getResponseBodyAsString() throws IOException {
        return EntityUtils.toString(this.entity);
    }

    public int getStatusCode() {
        return this.response.getStatusLine().getStatusCode();
    }

    public HttpResponse unwrap() {
        return this.response;
    }
}
