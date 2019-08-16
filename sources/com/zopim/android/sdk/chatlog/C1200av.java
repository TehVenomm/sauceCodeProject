package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog.Type;

/* renamed from: com.zopim.android.sdk.chatlog.av */
class C1200av implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f841a;

    C1200av(ZopimChatLogFragment zopimChatLogFragment) {
        this.f841a = zopimChatLogFragment;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        if (LivechatChatLogPath.getInstance().countMessages(Type.CHAT_MSG_VISITOR, Type.CHAT_MSG_AGENT) <= 0 || this.f841a.mNoConnection) {
            this.f841a.mChat.endChat();
            this.f841a.close();
            if (this.f841a.mChatListener != null) {
                this.f841a.mChatListener.onChatEnded();
                return;
            }
            return;
        }
        this.f841a.showEmailTranscriptDialog();
    }
}
