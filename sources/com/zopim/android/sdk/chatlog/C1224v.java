package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.v */
class C1224v implements Runnable {

    /* renamed from: a */
    final /* synthetic */ Connection f878a;

    /* renamed from: b */
    final /* synthetic */ C1223u f879b;

    C1224v(C1223u uVar, Connection connection) {
        this.f879b = uVar;
        this.f878a = connection;
    }

    public void run() {
        this.f879b.f877a.updateConnection(this.f878a);
    }
}
