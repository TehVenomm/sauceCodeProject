package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.u */
class C0854u extends ConnectionObserver {
    /* renamed from: a */
    final /* synthetic */ ConnectionFragment f833a;

    C0854u(ConnectionFragment connectionFragment) {
        this.f833a = connectionFragment;
    }

    public void update(Connection connection) {
        this.f833a.mHandler.post(new C0855v(this, connection));
    }
}
