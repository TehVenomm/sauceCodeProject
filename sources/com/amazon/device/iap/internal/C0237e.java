package com.amazon.device.iap.internal;

import android.util.Log;
import com.amazon.device.iap.internal.p004a.C0192d;
import com.amazon.device.iap.internal.p005b.C0227g;

/* renamed from: com.amazon.device.iap.internal.e */
public final class C0237e {
    /* renamed from: a */
    private static final String f94a = C0237e.class.getName();
    /* renamed from: b */
    private static volatile boolean f95b;
    /* renamed from: c */
    private static volatile boolean f96c;
    /* renamed from: d */
    private static volatile C0189c f97d;
    /* renamed from: e */
    private static volatile C0185a f98e;
    /* renamed from: f */
    private static volatile C0191b f99f;

    /* renamed from: a */
    private static <T> T m153a(Class<T> cls) {
        T t = null;
        try {
            t = C0237e.m157d().mo1186a(cls).newInstance();
        } catch (Throwable e) {
            Log.e(f94a, "error getting instance for " + cls, e);
        }
        return t;
    }

    /* renamed from: a */
    public static boolean m154a() {
        if (f96c) {
            return f95b;
        }
        synchronized (C0237e.class) {
            try {
                if (f96c) {
                    boolean z = f95b;
                    return z;
                }
                C0237e.class.getClassLoader().loadClass("com.amazon.android.Kiwi");
                f95b = false;
                f96c = true;
                return f95b;
            } catch (Throwable th) {
                Class cls = C0237e.class;
            }
        }
    }

    /* renamed from: b */
    public static C0189c m155b() {
        if (f97d == null) {
            synchronized (C0237e.class) {
                try {
                    if (f97d == null) {
                        f97d = (C0189c) C0237e.m153a(C0189c.class);
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = C0237e.class;
                    }
                }
            }
        }
        return f97d;
    }

    /* renamed from: c */
    public static C0185a m156c() {
        if (f98e == null) {
            synchronized (C0237e.class) {
                try {
                    if (f98e == null) {
                        f98e = (C0185a) C0237e.m153a(C0185a.class);
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = C0237e.class;
                    }
                }
            }
        }
        return f98e;
    }

    /* renamed from: d */
    private static C0191b m157d() {
        if (f99f == null) {
            synchronized (C0237e.class) {
                try {
                    if (f99f == null) {
                        if (C0237e.m154a()) {
                            f99f = new C0192d();
                        } else {
                            f99f = new C0227g();
                        }
                    }
                } catch (Throwable th) {
                    Class cls = C0237e.class;
                }
            }
        }
        return f99f;
    }
}
