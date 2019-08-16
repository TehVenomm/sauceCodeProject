package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.HttpRequest.Status;

/* renamed from: com.zopim.android.sdk.api.k */
/* synthetic */ class C1150k {

    /* renamed from: a */
    static final /* synthetic */ int[] f683a = new int[Status.values().length];

    static {
        try {
            f683a[Status.SUCCESS.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f683a[Status.REDIRECT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f683a[Status.CLIENT_ERROR.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f683a[Status.SERVER_ERROR.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
