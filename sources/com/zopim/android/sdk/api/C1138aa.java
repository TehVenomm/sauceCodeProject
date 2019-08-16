package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.api.aa */
/* synthetic */ class C1138aa {

    /* renamed from: a */
    static final /* synthetic */ int[] f668a = new int[Rating.values().length];

    static {
        try {
            f668a[Rating.GOOD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f668a[Rating.BAD.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f668a[Rating.UNRATED.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
    }
}
