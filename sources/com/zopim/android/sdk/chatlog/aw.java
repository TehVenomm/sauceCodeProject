package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

class aw implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f798a;

    aw(ZopimChatLogFragment zopimChatLogFragment) {
        this.f798a = zopimChatLogFragment;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        dialogInterface.dismiss();
        this.f798a.mChat.endChat();
        this.f798a.close();
        if (this.f798a.mChatListener != null) {
            this.f798a.mChatListener.onChatEnded();
        }
    }
}
