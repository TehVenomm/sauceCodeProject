package com.github.droidfu.http;

import java.util.HashMap;
import org.apache.http.impl.client.AbstractHttpClient;

class HttpDelete extends BetterHttpRequestBase {
    HttpDelete(AbstractHttpClient abstractHttpClient, String str, HashMap<String, String> hashMap) {
        super(abstractHttpClient);
        this.request = new org.apache.http.client.methods.HttpDelete(str);
        for (String str2 : hashMap.keySet()) {
            this.request.setHeader(str2, (String) hashMap.get(str2));
        }
    }
}
