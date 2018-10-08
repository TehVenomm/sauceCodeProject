package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.y */
class C0858y implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Connection f840a;
    /* renamed from: b */
    final /* synthetic */ C0857x f841b;

    C0858y(C0857x c0857x, Connection connection) {
        this.f841b = c0857x;
        this.f840a = connection;
    }

    public void run() {
        this.f841b.f839a.updateToastView(this.f840a);
    }
}
