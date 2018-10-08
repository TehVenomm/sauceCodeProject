package com.zopim.android.sdk.api;

import android.util.Log;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;
import com.zopim.android.sdk.model.Connection.Status;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.f */
class C0803f extends ConnectionObserver {
    /* renamed from: a */
    final /* synthetic */ ChatService f634a;

    C0803f(ChatService chatService) {
        this.f634a = chatService;
    }

    /* renamed from: a */
    private void m584a() {
        if (!this.f634a.mUnsentMessages.isEmpty()) {
            Log.v(ChatService.LOG_TAG, "Resending cached unsent messages");
            while (true) {
                String str = (String) this.f634a.mUnsentMessages.poll();
                if (str != null) {
                    this.f634a.send(str);
                } else {
                    return;
                }
            }
        }
    }

    /* renamed from: b */
    private void m585b() {
        if (!this.f634a.mUnsentFiles.isEmpty()) {
            Log.v(ChatService.LOG_TAG, "Resending cached unsent files");
            while (true) {
                File file = (File) this.f634a.mUnsentFiles.poll();
                if (file != null) {
                    this.f634a.send(file);
                } else {
                    return;
                }
            }
        }
    }

    public void update(Connection connection) {
        if (connection.getStatus() == Status.CONNECTED) {
            if (!this.f634a.mChatInitialized) {
                this.f634a.onChatInitialized();
            }
            m584a();
            m585b();
        }
    }
}
