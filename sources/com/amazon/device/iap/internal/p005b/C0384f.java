package com.amazon.device.iap.internal.p005b;

import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.device.iap.internal.C0350a;

/* renamed from: com.amazon.device.iap.internal.b.f */
public class C0384f implements C0350a {

    /* renamed from: a */
    private static KiwiLogger f72a = new KiwiLogger("In App Purchasing SDK - Production Mode");

    /* renamed from: c */
    private static String m81c(String str, String str2) {
        return str + ": " + str2;
    }

    /* renamed from: a */
    public void mo6192a(String str, String str2) {
        f72a.trace(m81c(str, str2));
    }

    /* renamed from: a */
    public boolean mo6193a() {
        return KiwiLogger.TRACE_ON;
    }

    /* renamed from: b */
    public void mo6194b(String str, String str2) {
        f72a.error(m81c(str, str2));
    }

    /* renamed from: b */
    public boolean mo6195b() {
        return KiwiLogger.ERROR_ON;
    }
}
