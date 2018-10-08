package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.y */
class C0859y implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Connection f840a;
    /* renamed from: b */
    final /* synthetic */ C0858x f841b;

    C0859y(C0858x c0858x, Connection connection) {
        this.f841b = c0858x;
        this.f840a = connection;
    }

    public void run() {
        this.f841b.f839a.updateToastView(this.f840a);
    }
}
