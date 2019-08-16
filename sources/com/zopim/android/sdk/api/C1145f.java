package com.zopim.android.sdk.api;

import android.util.Log;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Connection;
import com.zopim.android.sdk.model.Connection.Status;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.f */
class C1145f extends ConnectionObserver {

    /* renamed from: a */
    final /* synthetic */ ChatService f677a;

    C1145f(ChatService chatService) {
        this.f677a = chatService;
    }

    /* renamed from: a */
    private void m593a() {
        if (!this.f677a.mUnsentMessages.isEmpty()) {
            Log.v(ChatService.LOG_TAG, "Resending cached unsent messages");
            while (true) {
                String str = (String) this.f677a.mUnsentMessages.poll();
                if (str != null) {
                    this.f677a.send(str);
                } else {
                    return;
                }
            }
        }
    }

    /* renamed from: b */
    private void m594b() {
        if (!this.f677a.mUnsentFiles.isEmpty()) {
            Log.v(ChatService.LOG_TAG, "Resending cached unsent files");
            while (true) {
                File file = (File) this.f677a.mUnsentFiles.poll();
                if (file != null) {
                    this.f677a.send(file);
                } else {
                    return;
                }
            }
        }
    }

    public void update(Connection connection) {
        if (connection.getStatus() == Status.CONNECTED) {
            if (!this.f677a.mChatInitialized) {
                this.f677a.onChatInitialized();
            }
            m593a();
            m594b();
        }
    }
}
