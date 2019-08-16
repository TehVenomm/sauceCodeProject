package com.amazon.device.iap.internal.p005b.p011f;

import com.amazon.device.iap.internal.p005b.C0378e;

/* renamed from: com.amazon.device.iap.internal.b.f.c */
public final class C0387c extends C0385a {
    public C0387c(C0378e eVar, boolean z) {
        super(eVar, "2.0");
        mo6232a("receiptDelivered", Boolean.valueOf(z));
    }

    /* renamed from: a_ */
    public void mo6224a_() {
        Object a = mo6233b().mo6221d().mo6226a("notifyListenerResult");
        if (a == null || !Boolean.TRUE.equals(a)) {
            mo6232a("notifyListenerSucceeded", Boolean.valueOf(false));
        } else {
            mo6232a("notifyListenerSucceeded", Boolean.valueOf(true));
        }
        super.mo6224a_();
    }
}
