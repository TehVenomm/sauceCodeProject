package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.x */
class C1227x extends ConnectionObserver {

    /* renamed from: a */
    final /* synthetic */ ConnectionToastFragment f883a;

    C1227x(ConnectionToastFragment connectionToastFragment) {
        this.f883a = connectionToastFragment;
    }

    public void update(Connection connection) {
        this.f883a.mHandler.post(new C1228y(this, connection));
    }
}
