package com.amazon.device.iap.internal.p005b;

import com.amazon.device.iap.internal.C0350a;
import com.amazon.device.iap.internal.C0356b;
import com.amazon.device.iap.internal.C0394c;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.b.g */
public final class C0388g implements C0356b {

    /* renamed from: a */
    private static final Map<Class, Class> f73a = new HashMap();

    static {
        f73a.put(C0394c.class, C0368c.class);
        f73a.put(C0350a.class, C0384f.class);
    }

    /* renamed from: a */
    public <T> Class<T> mo6204a(Class<T> cls) {
        return (Class) f73a.get(cls);
    }
}
