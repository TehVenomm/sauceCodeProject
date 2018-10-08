package com.unity3d.player;

import android.os.Build.VERSION;

/* renamed from: com.unity3d.player.q */
public final class C0773q {
    /* renamed from: a */
    static final boolean f535a = (VERSION.SDK_INT >= 11);
    /* renamed from: b */
    static final boolean f536b = (VERSION.SDK_INT >= 12);
    /* renamed from: c */
    static final boolean f537c = (VERSION.SDK_INT >= 14);
    /* renamed from: d */
    static final boolean f538d = (VERSION.SDK_INT >= 16);
    /* renamed from: e */
    static final boolean f539e = (VERSION.SDK_INT >= 17);
    /* renamed from: f */
    static final boolean f540f = (VERSION.SDK_INT >= 19);
    /* renamed from: g */
    static final boolean f541g = (VERSION.SDK_INT >= 21);
    /* renamed from: h */
    static final boolean f542h;
    /* renamed from: i */
    static final C0754f f543i = (f535a ? new C0755d() : null);
    /* renamed from: j */
    static final C0750e f544j = (f536b ? new C0751c() : null);
    /* renamed from: k */
    static final C0757h f545k = (f538d ? new C0766l() : null);
    /* renamed from: l */
    static final C0756g f546l = (f539e ? new C0764k() : null);
    /* renamed from: m */
    static final C0758i f547m;

    static {
        C0758i c0758i = null;
        boolean z = true;
        if (VERSION.SDK_INT < 23) {
            z = false;
        }
        f542h = z;
        if (f542h) {
            c0758i = new C0769n();
        }
        f547m = c0758i;
    }
}
