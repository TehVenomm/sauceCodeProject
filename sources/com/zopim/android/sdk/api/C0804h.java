package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.FileTransfers.C0793b;
import com.zopim.android.sdk.model.ChatLog.Type;

/* renamed from: com.zopim.android.sdk.api.h */
/* synthetic */ class C0804h {
    /* renamed from: a */
    static final /* synthetic */ int[] f636a = new int[C0793b.values().length];
    /* renamed from: b */
    static final /* synthetic */ int[] f637b = new int[Type.values().length];

    static {
        try {
            f637b[Type.ATTACHMENT_UPLOAD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f637b[Type.CHAT_MSG_AGENT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f636a[C0793b.f617b.ordinal()] = 1;
        } catch (NoSuchFieldError e3) {
        }
    }
}
