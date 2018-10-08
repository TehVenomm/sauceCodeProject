package net.gogame.gowrap.support;

import java.util.Locale;

public class HttpException extends Exception {
    private final int statusCode;
    private final String statusMessage;

    public HttpException(int i, String str) {
        super(String.format(Locale.ENGLISH, "%d %s", new Object[]{Integer.valueOf(i), str}));
        this.statusCode = i;
        this.statusMessage = str;
    }

    public int getStatusCode() {
        return this.statusCode;
    }

    public String getStatusMessage() {
        return this.statusMessage;
    }
}
