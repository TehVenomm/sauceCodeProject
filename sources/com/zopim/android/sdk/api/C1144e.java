package com.zopim.android.sdk.api;

import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Error;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.e */
class C1144e extends C1161u<File> {

    /* renamed from: a */
    final /* synthetic */ ChatLog f675a;

    /* renamed from: b */
    final /* synthetic */ C1142c f676b;

    C1144e(C1142c cVar, ChatLog chatLog) {
        this.f676b = cVar;
        this.f675a = chatLog;
    }

    /* renamed from: a */
    public void mo20643a(ErrorResponse errorResponse) {
        this.f675a.setError(Error.UPLOAD_FAILED_ERROR);
        this.f675a.setFailed(true);
        ((C1134a) FileTransfers.INSTANCE.mTransfers.get(this.f675a.getAttachment().getName())).f659b = C1135b.f664e;
        LivechatChatLogPath.getInstance().broadcast();
    }

    /* renamed from: a */
    public void mo20644a(File file) {
        Logger.m577v(ChatService.LOG_TAG, "Download completed");
        this.f675a.setFailed(false);
        this.f675a.setFile(file);
        ((C1134a) FileTransfers.INSTANCE.mTransfers.get(this.f675a.getAttachment().getName())).f659b = C1135b.f663d;
        LivechatChatLogPath.getInstance().broadcast();
    }
}
