package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.HttpRequest.Status;

/* renamed from: com.zopim.android.sdk.api.k */
/* synthetic */ class C0807k {
    /* renamed from: a */
    static final /* synthetic */ int[] f640a = new int[Status.values().length];

    static {
        try {
            f640a[Status.SUCCESS.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f640a[Status.REDIRECT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f640a[Status.CLIENT_ERROR.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f640a[Status.SERVER_ERROR.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
