package com.amazon.device.iap.internal.p005b.p008c;

import com.amazon.device.iap.internal.p005b.C0193i;
import com.amazon.device.iap.internal.p005b.C0197e;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c.c */
abstract class C0206c extends C0193i {
    /* renamed from: a */
    protected final Set<String> f46a;

    C0206c(C0197e c0197e, String str, Set<String> set) {
        super(c0197e, "getItem_data", str);
        this.f46a = set;
        m56a("skus", set);
    }
}
