package com.zopim.android.sdk.prechat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* renamed from: com.zopim.android.sdk.prechat.k */
class C0889k extends BroadcastReceiver {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f906a;

    C0889k(ZopimChatFragment zopimChatFragment) {
        this.f906a = zopimChatFragment;
    }

    public void onReceive(Context context, Intent intent) {
        this.f906a.onChatInitializationFailed();
        this.f906a.showCouldNotConnectError();
    }
}
