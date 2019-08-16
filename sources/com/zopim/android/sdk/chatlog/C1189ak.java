package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.ak */
class C1189ak implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1188aj f827a;

    C1189ak(C1188aj ajVar) {
        this.f827a = ajVar;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f827a.f826a.mChat.endChat();
        this.f827a.f826a.close();
        if (this.f827a.f826a.mChatListener != null) {
            this.f827a.f826a.mChatListener.onChatEnded();
        }
    }
}
