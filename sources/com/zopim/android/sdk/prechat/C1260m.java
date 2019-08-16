package com.zopim.android.sdk.prechat;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.prechat.m */
class C1260m implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1259l f952a;

    C1260m(C1259l lVar) {
        this.f952a = lVar;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f952a.f951a.mChat.endChat();
        this.f952a.f951a.close();
        if (this.f952a.f951a.mChatListener != null) {
            this.f952a.f951a.mChatListener.onChatEnded();
        }
    }
}
