package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.FileTransfers.C0792a;
import com.zopim.android.sdk.api.FileTransfers.C0793b;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Error;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.e */
class C0801e extends C0799u<File> {
    /* renamed from: a */
    final /* synthetic */ ChatLog f632a;
    /* renamed from: b */
    final /* synthetic */ C0798c f633b;

    C0801e(C0798c c0798c, ChatLog chatLog) {
        this.f633b = c0798c;
        this.f632a = chatLog;
    }

    /* renamed from: a */
    public void mo4228a(ErrorResponse errorResponse) {
        this.f632a.setError(Error.UPLOAD_FAILED_ERROR);
        this.f632a.setFailed(true);
        ((C0792a) FileTransfers.INSTANCE.mTransfers.get(this.f632a.getAttachment().getName())).f615b = C0793b.f620e;
        LivechatChatLogPath.getInstance().broadcast();
    }

    /* renamed from: a */
    public void m582a(File file) {
        Logger.m564v(ChatService.LOG_TAG, "Download completed");
        this.f632a.setFailed(false);
        this.f632a.setFile(file);
        ((C0792a) FileTransfers.INSTANCE.mTransfers.get(this.f632a.getAttachment().getName())).f615b = C0793b.f619d;
        LivechatChatLogPath.getInstance().broadcast();
    }
}
