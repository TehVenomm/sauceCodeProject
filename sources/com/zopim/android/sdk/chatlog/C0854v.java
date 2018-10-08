package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.v */
class C0854v implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Connection f834a;
    /* renamed from: b */
    final /* synthetic */ C0853u f835b;

    C0854v(C0853u c0853u, Connection connection) {
        this.f835b = c0853u;
        this.f834a = connection;
    }

    public void run() {
        this.f835b.f833a.updateConnection(this.f834a);
    }
}
