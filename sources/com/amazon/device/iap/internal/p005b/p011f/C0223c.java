package com.amazon.device.iap.internal.p005b.p011f;

import com.amazon.device.iap.internal.p005b.C0197e;

/* renamed from: com.amazon.device.iap.internal.b.f.c */
public final class C0223c extends C0221a {
    public C0223c(C0197e c0197e, boolean z) {
        super(c0197e, "2.0");
        m56a("receiptDelivered", Boolean.valueOf(z));
    }

    public void a_() {
        Object a = m58b().m73d().m116a("notifyListenerResult");
        if (a == null || !Boolean.TRUE.equals(a)) {
            m56a("notifyListenerSucceeded", Boolean.valueOf(false));
        } else {
            m56a("notifyListenerSucceeded", Boolean.valueOf(true));
        }
        super.a_();
    }
}
