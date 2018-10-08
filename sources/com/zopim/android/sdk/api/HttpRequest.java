package com.zopim.android.sdk.api;

import com.google.android.gms.nearby.messages.Strategy;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.util.concurrent.TimeUnit;

interface HttpRequest {
    /* renamed from: a */
    public static final long f622a = TimeUnit.SECONDS.toMillis(10);

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
            return (i < 200 || i >= Strategy.TTL_SECONDS_DEFAULT) ? (i < Strategy.TTL_SECONDS_DEFAULT || i >= 400) ? (i < 400 || i >= 500) ? (i < 500 || i >= SettingsJsonConstants.ANALYTICS_FLUSH_INTERVAL_SECS_DEFAULT) ? UNKNOWN : SERVER_ERROR : CLIENT_ERROR : REDIRECT : SUCCESS;
        }
    }
}
