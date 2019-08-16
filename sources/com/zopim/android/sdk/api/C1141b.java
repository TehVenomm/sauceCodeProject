package com.zopim.android.sdk.api;

/* renamed from: com.zopim.android.sdk.api.b */
class C1141b implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ChatService f671a;

    C1141b(ChatService chatService) {
        this.f671a = chatService;
    }

    public void run() {
        if (this.f671a.canCommunicate()) {
            ChatService.mChat.mo20635a();
        }
    }
}
