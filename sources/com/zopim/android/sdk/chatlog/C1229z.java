package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection.Status;

/* renamed from: com.zopim.android.sdk.chatlog.z */
/* synthetic */ class C1229z {

    /* renamed from: a */
    static final /* synthetic */ int[] f886a = new int[Status.values().length];

    static {
        try {
            f886a[Status.NO_CONNECTION.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f886a[Status.CONNECTING.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f886a[Status.DISCONNECTED.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f886a[Status.CONNECTED.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
