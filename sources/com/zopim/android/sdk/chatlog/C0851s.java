package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.s */
/* synthetic */ class C0851s {
    /* renamed from: a */
    static final /* synthetic */ int[] f830a = new int[Rating.values().length];

    static {
        try {
            f830a[Rating.GOOD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f830a[Rating.BAD.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f830a[Rating.UNKNOWN.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
    }
}
