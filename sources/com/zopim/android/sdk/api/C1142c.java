package com.zopim.android.sdk.api;

import android.util.Log;
import android.util.Pair;
import com.zopim.android.sdk.attachment.SdkCache;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.model.ChatLog;
import java.io.File;
import java.net.URL;
import java.util.LinkedHashMap;

/* renamed from: com.zopim.android.sdk.api.c */
class C1142c extends ChatLogObserver {

    /* renamed from: a */
    final /* synthetic */ ChatService f672a;

    C1142c(ChatService chatService) {
        this.f672a = chatService;
    }

    public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
        for (ChatLog chatLog : linkedHashMap.values()) {
            switch (C1147h.f680b[chatLog.getType().ordinal()]) {
                case 1:
                    if (chatLog != null && chatLog.getUploadUrl() != null) {
                        if (chatLog.getFile() != null) {
                            C1134a aVar = (C1134a) FileTransfers.INSTANCE.mTransfers.get(chatLog.getFileName());
                            if (aVar != null) {
                                switch (C1147h.f679a[aVar.f659b.ordinal()]) {
                                    case 1:
                                        URL uploadUrl = chatLog.getUploadUrl();
                                        File file = chatLog.getFile();
                                        Logger.m577v(ChatService.LOG_TAG, "Starting file upload task");
                                        C1156p pVar = new C1156p();
                                        pVar.mo20669a((C1161u<Void>) new C1143d<Void>(this, chatLog));
                                        pVar.execute(new Pair[]{new Pair(file, uploadUrl)});
                                        aVar.f659b = C1135b.f662c;
                                        chatLog.setFailed(false);
                                        chatLog.setProgress(1);
                                        break;
                                    default:
                                        Logger.m577v(ChatService.LOG_TAG, "Skipping start of already started upload.");
                                        break;
                                }
                            } else {
                                Logger.m579w(ChatService.LOG_TAG, "Unexpected, upload info should have been added prior to this. Skipping upload");
                                break;
                            }
                        } else {
                            Log.w(ChatService.LOG_TAG, "Upload file is not available. Skipping upload.");
                            break;
                        }
                    } else {
                        Logger.m579w(ChatService.LOG_TAG, "Upload url is not available. Skipping upload.");
                        break;
                    }
                    break;
                case 2:
                    if (chatLog.getAttachment() != null && chatLog.getAttachment().getUrl() != null && chatLog.getAttachment().getName() != null) {
                        C1134a aVar2 = (C1134a) FileTransfers.INSTANCE.mTransfers.get(chatLog.getAttachment().getName());
                        if (aVar2 == null) {
                            File file2 = new File(SdkCache.INSTANCE.getSdkCacheDir(this.f672a.getApplicationContext()).getPath() + File.separator + chatLog.getAttachment().getName());
                            aVar2 = new C1134a();
                            aVar2.f659b = C1135b.f661b;
                            aVar2.f658a = file2;
                            FileTransfers.INSTANCE.mTransfers.put(chatLog.getAttachment().getName(), aVar2);
                            chatLog.setFile(file2);
                        }
                        switch (C1147h.f679a[aVar2.f659b.ordinal()]) {
                            case 1:
                                URL url = chatLog.getAttachment().getUrl();
                                File file3 = chatLog.getFile();
                                Logger.m577v(ChatService.LOG_TAG, "Starting file download task");
                                C1154n nVar = new C1154n();
                                nVar.mo20663a((C1161u<File>) new C1144e<File>(this, chatLog));
                                nVar.execute(new Pair[]{new Pair(url, file3)});
                                aVar2.f659b = C1135b.f662c;
                                chatLog.setFailed(false);
                                break;
                        }
                    } else {
                        Logger.m579w(ChatService.LOG_TAG, "Attachment url is not available. Skipping download.");
                        break;
                    }
                    break;
            }
        }
    }
}
