package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.chatlog.x */
class C0857x extends ConnectionObserver {
    /* renamed from: a */
    final /* synthetic */ ConnectionToastFragment f839a;

    C0857x(ConnectionToastFragment connectionToastFragment) {
        this.f839a = connectionToastFragment;
    }

    public void update(Connection connection) {
        this.f839a.mHandler.post(new C0858y(this, connection));
    }
}
