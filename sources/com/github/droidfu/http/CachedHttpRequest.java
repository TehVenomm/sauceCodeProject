package com.github.droidfu.http;

import java.net.ConnectException;
import org.apache.http.client.methods.HttpUriRequest;

public class CachedHttpRequest implements BetterHttpRequest {
    private String url;

    public CachedHttpRequest(String str) {
        this.url = str;
    }

    public BetterHttpRequest expecting(Integer... numArr) {
        return this;
    }

    public String getRequestUrl() {
        return this.url;
    }

    public BetterHttpRequest retries(int i) {
        return this;
    }

    public BetterHttpResponse send() throws ConnectException {
        return new CachedHttpResponse(this.url);
    }

    public HttpUriRequest unwrap() {
        return null;
    }

    public BetterHttpRequest withTimeout(int i) {
        return this;
    }
}
