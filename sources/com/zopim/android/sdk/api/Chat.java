package com.zopim.android.sdk.api;

public interface Chat extends ChatApi {
    ChatConfig getConfig();

    boolean hasEnded();

    void resetTimeout();
}
