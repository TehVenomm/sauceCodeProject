package com.zopim.android.sdk.chatlog;

import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.chatlog.an */
class C1192an implements Runnable {

    /* renamed from: a */
    final /* synthetic */ LinkedHashMap f830a;

    /* renamed from: b */
    final /* synthetic */ C1191am f831b;

    C1192an(C1191am amVar, LinkedHashMap linkedHashMap) {
        this.f831b = amVar;
        this.f830a = linkedHashMap;
    }

    public void run() {
        this.f831b.f829a.updateChatLogAdapter(this.f830a);
    }
}
