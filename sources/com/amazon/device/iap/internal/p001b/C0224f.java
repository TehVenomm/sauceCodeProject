package com.amazon.device.iap.internal.p001b;

import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.device.iap.internal.C0185a;

/* renamed from: com.amazon.device.iap.internal.b.f */
public class C0224f implements C0185a {
    /* renamed from: a */
    private static KiwiLogger f64a = new KiwiLogger("In App Purchasing SDK - Production Mode");

    /* renamed from: c */
    private static String m105c(String str, String str2) {
        return str + ": " + str2;
    }

    /* renamed from: a */
    public void mo1176a(String str, String str2) {
        f64a.trace(C0224f.m105c(str, str2));
    }

    /* renamed from: a */
    public boolean mo1177a() {
        return KiwiLogger.TRACE_ON;
    }

    /* renamed from: b */
    public void mo1178b(String str, String str2) {
        f64a.error(C0224f.m105c(str, str2));
    }

    /* renamed from: b */
    public boolean mo1179b() {
        return KiwiLogger.ERROR_ON;
    }
}
