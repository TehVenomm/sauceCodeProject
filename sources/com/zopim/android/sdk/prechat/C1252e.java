package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.e */
class C1252e implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f944a;

    C1252e(ZopimChatFragment zopimChatFragment) {
        this.f944a = zopimChatFragment;
    }

    public void run() {
        this.f944a.mChat.endChat();
    }
}
