package com.zopim.android.sdk.prechat;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.prechat.m */
class C0890m implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C0889l f908a;

    C0890m(C0889l c0889l) {
        this.f908a = c0889l;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f908a.f907a.mChat.endChat();
        this.f908a.f907a.close();
        if (this.f908a.f907a.mChatListener != null) {
            this.f908a.f907a.mChatListener.onChatEnded();
        }
    }
}
