package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.FileTransfers.C0793a;
import com.zopim.android.sdk.api.FileTransfers.C0794b;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Error;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.e */
class C0802e extends C0800u<File> {
    /* renamed from: a */
    final /* synthetic */ ChatLog f632a;
    /* renamed from: b */
    final /* synthetic */ C0799c f633b;

    C0802e(C0799c c0799c, ChatLog chatLog) {
        this.f633b = c0799c;
        this.f632a = chatLog;
    }

    /* renamed from: a */
    public void mo4226a(ErrorResponse errorResponse) {
        this.f632a.setError(Error.UPLOAD_FAILED_ERROR);
        this.f632a.setFailed(true);
        ((C0793a) FileTransfers.INSTANCE.mTransfers.get(this.f632a.getAttachment().getName())).f615b = C0794b.f620e;
        LivechatChatLogPath.getInstance().broadcast();
    }

    /* renamed from: a */
    public void m582a(File file) {
        Logger.m564v(ChatService.LOG_TAG, "Download completed");
        this.f632a.setFailed(false);
        this.f632a.setFile(file);
        ((C0793a) FileTransfers.INSTANCE.mTransfers.get(this.f632a.getAttachment().getName())).f615b = C0794b.f619d;
        LivechatChatLogPath.getInstance().broadcast();
    }
}
