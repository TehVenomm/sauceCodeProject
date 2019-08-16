package com.amazon.device.iap.internal.p005b;

import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.h */
public class C0391h {

    /* renamed from: b */
    static final /* synthetic */ boolean f78b = (!C0391h.class.desiredAssertionStatus());

    /* renamed from: a */
    public final Map<String, Object> f79a = new HashMap();

    /* renamed from: a */
    public Object mo6225a() {
        return this.f79a.get("RESPONSE");
    }

    /* renamed from: a */
    public Object mo6226a(String str) {
        return this.f79a.get(str);
    }

    /* renamed from: a */
    public void mo6227a(Object obj) {
        if (f78b || obj != null) {
            this.f79a.put("RESPONSE", obj);
            return;
        }
        throw new AssertionError();
    }

    /* renamed from: a */
    public void mo6228a(String str, Object obj) {
        this.f79a.put(str, obj);
    }

    /* renamed from: b */
    public void mo6229b() {
        this.f79a.remove("RESPONSE");
    }
}
