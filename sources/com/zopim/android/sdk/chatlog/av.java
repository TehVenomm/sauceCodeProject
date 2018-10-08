package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog.Type;

class av implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f797a;

    av(ZopimChatLogFragment zopimChatLogFragment) {
        this.f797a = zopimChatLogFragment;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        if (LivechatChatLogPath.getInstance().countMessages(Type.CHAT_MSG_VISITOR, Type.CHAT_MSG_AGENT) <= 0 || this.f797a.mNoConnection) {
            this.f797a.mChat.endChat();
            this.f797a.close();
            if (this.f797a.mChatListener != null) {
                this.f797a.mChatListener.onChatEnded();
                return;
            }
            return;
        }
        this.f797a.showEmailTranscriptDialog();
    }
}
