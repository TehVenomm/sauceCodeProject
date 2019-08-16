package com.amazon.device.iap.internal.p004a;

import com.amazon.device.iap.internal.C0350a;
import com.amazon.device.iap.internal.C0356b;
import com.amazon.device.iap.internal.C0394c;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.a.d */
public final class C0355d implements C0356b {

    /* renamed from: a */
    private static final Map<Class, Class> f36a = new HashMap();

    static {
        f36a.put(C0394c.class, C0353c.class);
        f36a.put(C0350a.class, C0351a.class);
    }

    /* renamed from: a */
    public <T> Class<T> mo6204a(Class<T> cls) {
        return (Class) f36a.get(cls);
    }
}
