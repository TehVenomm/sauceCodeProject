package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.aw */
class C1201aw implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f842a;

    C1201aw(ZopimChatLogFragment zopimChatLogFragment) {
        this.f842a = zopimChatLogFragment;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        dialogInterface.dismiss();
        this.f842a.mChat.endChat();
        this.f842a.close();
        if (this.f842a.mChatListener != null) {
            this.f842a.mChatListener.onChatEnded();
        }
    }
}
