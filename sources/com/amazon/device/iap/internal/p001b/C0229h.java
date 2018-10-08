package com.amazon.device.iap.internal.p001b;

import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.h */
public class C0229h {
    /* renamed from: b */
    static final /* synthetic */ boolean f70b = (!C0229h.class.desiredAssertionStatus());
    /* renamed from: a */
    public final Map<String, Object> f71a = new HashMap();

    /* renamed from: a */
    public Object m115a() {
        return this.f71a.get("RESPONSE");
    }

    /* renamed from: a */
    public Object m116a(String str) {
        return this.f71a.get(str);
    }

    /* renamed from: a */
    public void m117a(Object obj) {
        if (f70b || obj != null) {
            this.f71a.put("RESPONSE", obj);
            return;
        }
        throw new AssertionError();
    }

    /* renamed from: a */
    public void m118a(String str, Object obj) {
        this.f71a.put(str, obj);
    }

    /* renamed from: b */
    public void m119b() {
        this.f71a.remove("RESPONSE");
    }
}
