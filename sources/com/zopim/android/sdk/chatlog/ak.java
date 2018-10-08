package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

class ak implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ aj f783a;

    ak(aj ajVar) {
        this.f783a = ajVar;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f783a.f782a.mChat.endChat();
        this.f783a.f782a.close();
        if (this.f783a.f782a.mChatListener != null) {
            this.f783a.f782a.mChatListener.onChatEnded();
        }
    }
}
