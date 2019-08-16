package com.unity3d.player;

import android.os.Build.VERSION;

/* renamed from: com.unity3d.player.h */
public final class C1107h {

    /* renamed from: a */
    static final boolean f580a = (VERSION.SDK_INT >= 19);

    /* renamed from: b */
    static final boolean f581b = (VERSION.SDK_INT >= 21);

    /* renamed from: c */
    static final boolean f582c;

    /* renamed from: d */
    static final C1102c f583d;

    static {
        boolean z = true;
        if (VERSION.SDK_INT < 23) {
            z = false;
        }
        f582c = z;
        f583d = z ? new C1105f() : null;
    }
}
