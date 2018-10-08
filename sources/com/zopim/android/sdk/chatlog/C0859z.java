package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.Connection.Status;

/* renamed from: com.zopim.android.sdk.chatlog.z */
/* synthetic */ class C0859z {
    /* renamed from: a */
    static final /* synthetic */ int[] f842a = new int[Status.values().length];

    static {
        try {
            f842a[Status.NO_CONNECTION.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f842a[Status.CONNECTING.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f842a[Status.DISCONNECTED.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f842a[Status.CONNECTED.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
    }
}
