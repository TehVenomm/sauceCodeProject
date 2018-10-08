package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.api.Chat;

public interface ChatListener {
    void onChatEnded();

    void onChatInitialized();

    void onChatLoaded(Chat chat);
}
