package com.zopim.android.sdk.widget;

import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Type;
import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.widget.f */
class C0907f extends ChatLogObserver {
    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f937a;

    C0907f(ChatWidgetService chatWidgetService) {
        this.f937a = chatWidgetService;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        int countMessages = LivechatChatLogPath.getInstance().countMessages(Type.CHAT_MSG_AGENT) - this.f937a.mInitialAgentMessageCount;
        if (countMessages > this.f937a.mUnreadCount) {
            this.f937a.mUnreadCount = countMessages;
            this.f937a.mAnimationHandler.post(new C0908g(this, countMessages));
        }
    }
}
