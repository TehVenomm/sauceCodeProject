package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.u */
class C1223u extends ConnectionObserver {

    /* renamed from: a */
    final /* synthetic */ ConnectionFragment f877a;

    C1223u(ConnectionFragment connectionFragment) {
        this.f877a = connectionFragment;
    }

    public void update(Connection connection) {
        this.f877a.mHandler.post(new C1224v(this, connection));
    }
}
