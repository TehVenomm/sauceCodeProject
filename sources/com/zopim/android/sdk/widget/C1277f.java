package com.zopim.android.sdk.widget;

import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Type;
import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.widget.f */
class C1277f extends ChatLogObserver {

    /* renamed from: a */
    final /* synthetic */ ChatWidgetService f981a;

    C1277f(ChatWidgetService chatWidgetService) {
        this.f981a = chatWidgetService;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        int countMessages = LivechatChatLogPath.getInstance().countMessages(Type.CHAT_MSG_AGENT) - this.f981a.mInitialAgentMessageCount;
        if (countMessages > this.f981a.mUnreadCount) {
            this.f981a.mUnreadCount = countMessages;
            this.f981a.mAnimationHandler.post(new C1278g(this, countMessages));
        }
    }
}
