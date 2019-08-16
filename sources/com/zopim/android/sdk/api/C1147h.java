package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.ChatLog.Type;

/* renamed from: com.zopim.android.sdk.api.h */
/* synthetic */ class C1147h {

    /* renamed from: a */
    static final /* synthetic */ int[] f679a = new int[C1135b.values().length];

    /* renamed from: b */
    static final /* synthetic */ int[] f680b = new int[Type.values().length];

    static {
        try {
            f680b[Type.ATTACHMENT_UPLOAD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f680b[Type.CHAT_MSG_AGENT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f679a[C1135b.f661b.ordinal()] = 1;
        } catch (NoSuchFieldError e3) {
        }
    }
}
