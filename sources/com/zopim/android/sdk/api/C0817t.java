package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.HttpRequest.Status;

/* renamed from: com.zopim.android.sdk.api.t */
/* synthetic */ class C0817t {
    /* renamed from: a */
    static final /* synthetic */ int[] f671a = new int[Status.values().length];

    static {
        try {
            f671a[Status.SUCCESS.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f671a[Status.REDIRECT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f671a[Status.CLIENT_ERROR.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f671a[Status.SERVER_ERROR.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
