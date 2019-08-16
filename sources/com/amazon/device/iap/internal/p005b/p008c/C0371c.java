package com.amazon.device.iap.internal.p005b.p008c;

import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c.c */
abstract class C0371c extends C0393i {

    /* renamed from: a */
    protected final Set<String> f53a;

    C0371c(C0378e eVar, String str, Set<String> set) {
        super(eVar, "getItem_data", str);
        this.f53a = set;
        mo6232a("skus", set);
    }
}
