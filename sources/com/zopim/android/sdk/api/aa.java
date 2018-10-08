package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.ChatLog.Rating;

/* synthetic */ class aa {
    /* renamed from: a */
    static final /* synthetic */ int[] f624a = new int[Rating.values().length];

    static {
        try {
            f624a[Rating.GOOD.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f624a[Rating.BAD.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f624a[Rating.UNRATED.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
    }
}
