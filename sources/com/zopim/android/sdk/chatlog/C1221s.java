package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.s */
/* synthetic */ class C1221s {

    /* renamed from: a */
    static final /* synthetic */ int[] f874a = new int[Rating.values().length];

    static {
        try {
            f874a[Rating.GOOD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f874a[Rating.BAD.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f874a[Rating.UNKNOWN.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
    }
}
