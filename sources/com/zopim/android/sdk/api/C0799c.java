package com.zopim.android.sdk.api;

import android.util.Log;
import android.util.Pair;
import com.zopim.android.sdk.api.FileTransfers.C0793a;
import com.zopim.android.sdk.api.FileTransfers.C0794b;
import com.zopim.android.sdk.attachment.SdkCache;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import java.io.File;
import java.net.URL;
import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.api.c */
class C0799c extends ChatLogObserver {
    /* renamed from: a */
    final /* synthetic */ ChatService f628a;

    C0799c(ChatService chatService) {
        this.f628a = chatService;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        for (ChatLog chatLog : linkedHashMap.values()) {
            C0793a c0793a;
            URL uploadUrl;
            File file;
            switch (C0805h.f637b[chatLog.getType().ordinal()]) {
                case 1:
                    if (chatLog != null && chatLog.getUploadUrl() != null) {
                        if (chatLog.getFile() != null) {
                            c0793a = (C0793a) FileTransfers.INSTANCE.mTransfers.get(chatLog.getFileName());
                            if (c0793a != null) {
                                switch (C0805h.f636a[c0793a.f615b.ordinal()]) {
                                    case 1:
                                        uploadUrl = chatLog.getUploadUrl();
                                        file = chatLog.getFile();
                                        Logger.m564v(ChatService.LOG_TAG, "Starting file upload task");
                                        C0813p c0813p = new C0813p();
                                        c0813p.m616a(new C0801d(this, chatLog));
                                        c0813p.execute(new Pair[]{new Pair(file, uploadUrl)});
                                        c0793a.f615b = C0794b.f618c;
                                        chatLog.setFailed(false);
                                        chatLog.setProgress(1);
                                        break;
                                    default:
                                        Logger.m564v(ChatService.LOG_TAG, "Skipping start of already started upload.");
                                        break;
                                }
                            }
                            Logger.m566w(ChatService.LOG_TAG, "Unexpected, upload info should have been added prior to this. Skipping upload");
                            break;
                        }
                        Log.w(ChatService.LOG_TAG, "Upload file is not available. Skipping upload.");
                        break;
                    }
                    Logger.m566w(ChatService.LOG_TAG, "Upload url is not available. Skipping upload.");
                    break;
                    break;
                case 2:
                    if (chatLog.getAttachment() != null && chatLog.getAttachment().getUrl() != null && chatLog.getAttachment().getName() != null) {
                        c0793a = (C0793a) FileTransfers.INSTANCE.mTransfers.get(chatLog.getAttachment().getName());
                        if (c0793a == null) {
                            File file2 = new File(SdkCache.INSTANCE.getSdkCacheDir(this.f628a.getApplicationContext()).getPath() + File.separator + chatLog.getAttachment().getName());
                            c0793a = new C0793a();
                            c0793a.f615b = C0794b.f617b;
                            c0793a.f614a = file2;
                            FileTransfers.INSTANCE.mTransfers.put(chatLog.getAttachment().getName(), c0793a);
                            chatLog.setFile(file2);
                        }
                        switch (C0805h.f636a[c0793a.f615b.ordinal()]) {
                            case 1:
                                uploadUrl = chatLog.getAttachment().getUrl();
                                file = chatLog.getFile();
                                Logger.m564v(ChatService.LOG_TAG, "Starting file download task");
                                C0811n c0811n = new C0811n();
                                c0811n.m606a(new C0802e(this, chatLog));
                                c0811n.execute(new Pair[]{new Pair(uploadUrl, file)});
                                c0793a.f615b = C0794b.f618c;
                                chatLog.setFailed(false);
                                break;
                            default:
                                break;
                        }
                    }
                    Logger.m566w(ChatService.LOG_TAG, "Attachment url is not available. Skipping download.");
                    break;
                default:
                    break;
            }
        }
    }
}
