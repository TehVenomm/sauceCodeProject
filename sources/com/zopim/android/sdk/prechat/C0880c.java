package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.model.Connection.Status;

/* renamed from: com.zopim.android.sdk.prechat.c */
/* synthetic */ class C0880c {
    /* renamed from: a */
    static final /* synthetic */ int[] f898a = new int[Status.values().length];

    static {
        try {
            f898a[Status.NO_CONNECTION.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f898a[Status.CONNECTED.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
    }
}
