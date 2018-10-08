package com.amazon.device.iap.internal.p014c;

import com.amazon.device.iap.internal.util.C0243d;
import java.util.Set;
import java.util.concurrent.ConcurrentSkipListSet;

/* renamed from: com.amazon.device.iap.internal.c.b */
public class C0232b {
    /* renamed from: b */
    private static final C0232b f79b = new C0232b();
    /* renamed from: a */
    private final Set<String> f80a = new ConcurrentSkipListSet();

    /* renamed from: a */
    public static C0232b m131a() {
        return f79b;
    }

    /* renamed from: a */
    public boolean m132a(String str) {
        return !C0243d.m172a(str) ? this.f80a.remove(str) : false;
    }

    /* renamed from: b */
    public void m133b(String str) {
        if (!C0243d.m172a(str)) {
            this.f80a.add(str);
        }
    }
}
