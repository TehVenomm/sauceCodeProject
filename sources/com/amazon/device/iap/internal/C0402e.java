package com.amazon.device.iap.internal;

import android.util.Log;
import com.amazon.device.iap.internal.p004a.C0355d;
import com.amazon.device.iap.internal.p005b.C0388g;

/* renamed from: com.amazon.device.iap.internal.e */
public final class C0402e {

    /* renamed from: a */
    private static final String f113a = C0402e.class.getName();

    /* renamed from: b */
    private static volatile boolean f114b;

    /* renamed from: c */
    private static volatile boolean f115c;

    /* renamed from: d */
    private static volatile C0394c f116d;

    /* renamed from: e */
    private static volatile C0350a f117e;

    /* renamed from: f */
    private static volatile C0356b f118f;

    /* renamed from: a */
    private static <T> T m148a(Class<T> cls) {
        boolean z = false;
        try {
            return m152d().mo6204a(cls).newInstance();
        } catch (Exception e) {
            Log.e(f113a, "error getting instance for " + cls, e);
            return z;
        }
    }

    /* renamed from: a */
    public static boolean m149a() {
        if (f115c) {
            return f114b;
        }
        synchronized (C0402e.class) {
            try {
                if (f115c) {
                    boolean z = f114b;
                    return z;
                }
                C0402e.class.getClassLoader().loadClass("com.amazon.android.Kiwi");
                f114b = false;
                f115c = true;
                return f114b;
            } catch (Throwable th) {
                Class<C0402e> cls = C0402e.class;
                throw th;
            }
        }
    }

    /* renamed from: b */
    public static C0394c m150b() {
        if (f116d == null) {
            synchronized (C0402e.class) {
                try {
                    if (f116d == null) {
                        f116d = (C0394c) m148a(C0394c.class);
                    }
                } finally {
                    while (true) {
                        Class<C0402e> cls = C0402e.class;
                    }
                }
            }
        }
        return f116d;
    }

    /* renamed from: c */
    public static C0350a m151c() {
        if (f117e == null) {
            synchronized (C0402e.class) {
                try {
                    if (f117e == null) {
                        f117e = (C0350a) m148a(C0350a.class);
                    }
                } finally {
                    while (true) {
                        Class<C0402e> cls = C0402e.class;
                    }
                }
            }
        }
        return f117e;
    }

    /* renamed from: d */
    private static C0356b m152d() {
        if (f118f == null) {
            synchronized (C0402e.class) {
                try {
                    if (f118f == null) {
                        if (m149a()) {
                            f118f = new C0355d();
                        } else {
                            f118f = new C0388g();
                        }
                    }
                } finally {
                    Class<C0402e> cls = C0402e.class;
                }
            }
        }
        return f118f;
    }
}
