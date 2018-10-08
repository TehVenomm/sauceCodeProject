package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.prechat.i */
class C0887i extends ConnectionObserver {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f904a;

    C0887i(ZopimChatFragment zopimChatFragment) {
        this.f904a = zopimChatFragment;
    }

    public void update(Connection connection) {
        if (!this.f904a.mChat.hasEnded()) {
            switch (C0881c.f898a[connection.getStatus().ordinal()]) {
                case 1:
                    if (!this.f904a.mChatInitialized) {
                        this.f904a.onChatInitializationFailed();
                        this.f904a.showNoConnectionError();
                        return;
                    }
                    return;
                case 2:
                    if (!this.f904a.mChatInitialized) {
                        this.f904a.mChatInitialized = true;
                        this.f904a.onChatInitialized();
                        return;
                    }
                    return;
                default:
                    return;
            }
        }
    }
}
