package com.amazon.device.iap.internal.p001b;

import com.amazon.device.iap.internal.C0185a;
import com.amazon.device.iap.internal.C0189c;
import com.amazon.device.iap.internal.C0191b;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.g */
public final class C0227g implements C0191b {
    /* renamed from: a */
    private static final Map<Class, Class> f69a = new HashMap();

    static {
        f69a.put(C0189c.class, C0210c.class);
        f69a.put(C0185a.class, C0224f.class);
    }

    /* renamed from: a */
    public <T> Class<T> mo1186a(Class<T> cls) {
        return (Class) f69a.get(cls);
    }
}
