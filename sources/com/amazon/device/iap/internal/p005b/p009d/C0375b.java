package com.amazon.device.iap.internal.p005b.p009d;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.internal.util.C0406b;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.d.b */
abstract class C0375b extends C0393i {

    /* renamed from: a */
    protected final boolean f57a;

    C0375b(C0378e eVar, String str, boolean z) {
        super(eVar, "purchase_updates", str);
        this.f57a = z;
    }

    /* access modifiers changed from: protected */
    public void preExecution() throws KiwiException {
        super.preExecution();
        mo6232a("cursor", this.f57a ? null : C0406b.m161a((String) mo6233b().mo6221d().mo6226a(AmazonAppstoreBillingService.JSON_KEY_USER_ID)));
    }
}
