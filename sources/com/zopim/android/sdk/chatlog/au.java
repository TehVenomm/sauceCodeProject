package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

class au implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f796a;

    au(ZopimChatLogFragment zopimChatLogFragment) {
        this.f796a = zopimChatLogFragment;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        dialogInterface.dismiss();
    }
}
