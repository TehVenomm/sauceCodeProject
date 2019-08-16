package com.zopim.android.sdk.api;

import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Error;

/* renamed from: com.zopim.android.sdk.api.d */
class C1143d extends C1161u<Void> {

    /* renamed from: a */
    final /* synthetic */ ChatLog f673a;

    /* renamed from: b */
    final /* synthetic */ C1142c f674b;

    C1143d(C1142c cVar, ChatLog chatLog) {
        this.f674b = cVar;
        this.f673a = chatLog;
    }

    /* renamed from: a */
    public void mo20643a(ErrorResponse errorResponse) {
        this.f673a.setError(Error.UPLOAD_FAILED_ERROR);
        this.f673a.setFailed(true);
        ((C1134a) FileTransfers.INSTANCE.mTransfers.get(this.f673a.getFileName())).f659b = C1135b.f664e;
        LivechatChatLogPath.getInstance().broadcast();
    }

    /* renamed from: a */
    public void mo20644a(Void voidR) {
        Logger.m577v(ChatService.LOG_TAG, "Upload completed");
    }
}
