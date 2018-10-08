package com.zopim.android.sdk.api;

/* renamed from: com.zopim.android.sdk.api.b */
class C0798b implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ChatService f627a;

    C0798b(ChatService chatService) {
        this.f627a = chatService;
    }

    public void run() {
        if (this.f627a.canCommunicate()) {
            ChatService.mChat.mo4231a();
        }
    }
}
