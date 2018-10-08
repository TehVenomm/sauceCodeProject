package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import java.util.LinkedHashMap;

class am extends ChatLogObserver {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f785a;

    am(ZopimChatLogFragment zopimChatLogFragment) {
        this.f785a = zopimChatLogFragment;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        this.f785a.mHandler.post(new an(this, linkedHashMap));
    }
}
