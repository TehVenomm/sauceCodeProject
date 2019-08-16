package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;

/* renamed from: com.zopim.android.sdk.prechat.i */
class C1256i extends ConnectionObserver {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f948a;

    C1256i(ZopimChatFragment zopimChatFragment) {
        this.f948a = zopimChatFragment;
    }

    public void update(Connection connection) {
        if (!this.f948a.mChat.hasEnded()) {
            switch (C1250c.f942a[connection.getStatus().ordinal()]) {
                case 1:
                    if (!this.f948a.mChatInitialized) {
                        this.f948a.onChatInitializationFailed();
                        this.f948a.showNoConnectionError();
                        return;
                    }
                    return;
                case 2:
                    if (!this.f948a.mChatInitialized) {
                        this.f948a.mChatInitialized = true;
                        this.f948a.onChatInitialized();
                        return;
                    }
                    return;
                default:
                    return;
            }
        }
    }
}
