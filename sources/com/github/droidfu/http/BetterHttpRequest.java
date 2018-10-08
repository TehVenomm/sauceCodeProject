package com.github.droidfu.http;

import java.net.ConnectException;
import org.apache.http.client.methods.HttpUriRequest;

public interface BetterHttpRequest {
    BetterHttpRequest expecting(Integer... numArr);

    String getRequestUrl();

    BetterHttpRequest retries(int i);

    BetterHttpResponse send() throws ConnectException;

    HttpUriRequest unwrap();

    BetterHttpRequest withTimeout(int i);
}
