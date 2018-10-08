package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.FileTransfers.C0792a;
import com.zopim.android.sdk.api.FileTransfers.C0793b;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Error;

/* renamed from: com.zopim.android.sdk.api.d */
class C0800d extends C0799u<Void> {
    /* renamed from: a */
    final /* synthetic */ ChatLog f630a;
    /* renamed from: b */
    final /* synthetic */ C0798c f631b;

    C0800d(C0798c c0798c, ChatLog chatLog) {
        this.f631b = c0798c;
        this.f630a = chatLog;
    }

    /* renamed from: a */
    public void mo4228a(ErrorResponse errorResponse) {
        this.f630a.setError(Error.UPLOAD_FAILED_ERROR);
        this.f630a.setFailed(true);
        ((C0792a) FileTransfers.INSTANCE.mTransfers.get(this.f630a.getFileName())).f615b = C0793b.f620e;
        LivechatChatLogPath.getInstance().broadcast();
    }

    /* renamed from: a */
    public void m580a(Void voidR) {
        Logger.m564v(ChatService.LOG_TAG, "Upload completed");
    }
}
