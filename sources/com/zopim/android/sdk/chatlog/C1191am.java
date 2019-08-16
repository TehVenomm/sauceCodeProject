package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.chatlog.am */
class C1191am extends ChatLogObserver {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f829a;

    C1191am(ZopimChatLogFragment zopimChatLogFragment) {
        this.f829a = zopimChatLogFragment;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        this.f829a.mHandler.post(new C1192an(this, linkedHashMap));
    }
}
