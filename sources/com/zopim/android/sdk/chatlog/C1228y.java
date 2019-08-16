package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.y */
class C1228y implements Runnable {

    /* renamed from: a */
    final /* synthetic */ Connection f884a;

    /* renamed from: b */
    final /* synthetic */ C1227x f885b;

    C1228y(C1227x xVar, Connection connection) {
        this.f885b = xVar;
        this.f884a = connection;
    }

    public void run() {
        this.f885b.f883a.updateToastView(this.f884a);
    }
}
