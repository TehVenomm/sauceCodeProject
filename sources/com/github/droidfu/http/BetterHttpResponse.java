package com.github.droidfu.http;

import java.io.IOException;
import java.io.InputStream;
import org.apache.http.HttpResponse;

public interface BetterHttpResponse {
    String getHeader(String str);

    InputStream getResponseBody() throws IOException;

    byte[] getResponseBodyAsBytes() throws IOException;

    String getResponseBodyAsString() throws IOException;

    int getStatusCode();

    HttpResponse unwrap();
}
