package com.zopim.android.sdk.prechat;

import com.zopim.android.sdk.model.Connection.Status;

/* renamed from: com.zopim.android.sdk.prechat.c */
/* synthetic */ class C1250c {

    /* renamed from: a */
    static final /* synthetic */ int[] f942a = new int[Status.values().length];

    static {
        try {
            f942a[Status.NO_CONNECTION.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f942a[Status.CONNECTED.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
    }
}
