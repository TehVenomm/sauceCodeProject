package com.zopim.android.sdk.chatlog;

import java.util.LinkedHashMap;

class an implements Runnable {
    /* renamed from: a */
    final /* synthetic */ LinkedHashMap f786a;
    /* renamed from: b */
    final /* synthetic */ am f787b;

    an(am amVar, LinkedHashMap linkedHashMap) {
        this.f787b = amVar;
        this.f786a = linkedHashMap;
    }

    public void run() {
        this.f787b.f785a.updateChatLogAdapter(this.f786a);
    }
}
