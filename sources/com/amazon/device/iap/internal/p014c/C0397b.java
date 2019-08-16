package com.amazon.device.iap.internal.p014c;

import com.amazon.device.iap.internal.util.C0408d;
import java.util.Set;
import java.util.concurrent.ConcurrentSkipListSet;

/* renamed from: com.amazon.device.iap.internal.c.b */
public class C0397b {

    /* renamed from: b */
    private static final C0397b f98b = new C0397b();

    /* renamed from: a */
    private final Set<String> f99a = new ConcurrentSkipListSet();

    /* renamed from: a */
    public static C0397b m126a() {
        return f98b;
    }

    /* renamed from: a */
    public boolean mo6249a(String str) {
        if (!C0408d.m167a(str)) {
            return this.f99a.remove(str);
        }
        return false;
    }

    /* renamed from: b */
    public void mo6250b(String str) {
        if (!C0408d.m167a(str)) {
            this.f99a.add(str);
        }
    }
}
