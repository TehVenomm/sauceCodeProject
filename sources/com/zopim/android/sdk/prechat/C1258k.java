package com.zopim.android.sdk.prechat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* renamed from: com.zopim.android.sdk.prechat.k */
class C1258k extends BroadcastReceiver {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f950a;

    C1258k(ZopimChatFragment zopimChatFragment) {
        this.f950a = zopimChatFragment;
    }

    public void onReceive(Context context, Intent intent) {
        this.f950a.onChatInitializationFailed();
        this.f950a.showCouldNotConnectError();
    }
}
