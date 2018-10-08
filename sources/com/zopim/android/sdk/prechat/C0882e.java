package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.e */
class C0882e implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f900a;

    C0882e(ZopimChatFragment zopimChatFragment) {
        this.f900a = zopimChatFragment;
    }

    public void run() {
        this.f900a.mChat.endChat();
    }
}
