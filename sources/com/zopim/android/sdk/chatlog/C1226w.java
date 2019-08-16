package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection.Status;

/* renamed from: com.zopim.android.sdk.chatlog.w */
/* synthetic */ class C1226w {

    /* renamed from: a */
    static final /* synthetic */ int[] f882a = new int[Status.values().length];

    static {
        try {
            f882a[Status.NO_CONNECTION.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f882a[Status.CONNECTING.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f882a[Status.DISCONNECTED.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f882a[Status.CONNECTED.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
