package com.zopim.android.sdk.api;

import java.util.concurrent.TimeUnit;

interface HttpRequest {

    /* renamed from: a */
    public static final long f666a = TimeUnit.SECONDS.toMillis(10);

    public interface ProgressListener {
        void onProgressUpdate(int i);
    }

    public enum Status {
        SUCCESS,
        REDIRECT,
        CLIENT_ERROR,
        SERVER_ERROR,
        UNKNOWN;

        public static Status getStatus(int i) {
            return (i < 200 || i >= 300) ? (i < 300 || i >= 400) ? (i < 400 || i >= 500) ? (i < 500 || i >= 600) ? UNKNOWN : SERVER_ERROR : CLIENT_ERROR : REDIRECT : SUCCESS;
        }
    }
}
