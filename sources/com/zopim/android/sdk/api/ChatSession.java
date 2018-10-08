package com.zopim.android.sdk.api;

import java.util.concurrent.TimeUnit;

public interface ChatSession {
    public static final String ACTION_CHAT_INITIALIZATION_TIMEOUT = "chat.action.INITIALIZATION_TIMEOUT";
    public static final String ACTION_CHAT_SESSION_TIMEOUT = "chat.action.TIMEOUT";
    public static final long DEFAULT_CHAT_INITIALIZATION_TIMEOUT = TimeUnit.SECONDS.toMillis(40);
    public static final long DEFAULT_CHAT_SESSION_TIMEOUT = TimeUnit.MINUTES.toMillis(5);
    public static final long DEFAULT_RECONNECT_TIMEOUT = TimeUnit.MINUTES.toMillis(2);
}
