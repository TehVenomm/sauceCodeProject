package com.amazon.device.iap.internal.p001b.p005d;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.util.C0241b;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.d.b */
abstract class C0212b extends C0193i {
    /* renamed from: a */
    protected final boolean f50a;

    C0212b(C0197e c0197e, String str, boolean z) {
        super(c0197e, "purchase_updates", str);
        this.f50a = z;
    }

    protected void preExecution() throws KiwiException {
        super.preExecution();
        m56a("cursor", this.f50a ? null : C0241b.m166a((String) m58b().m73d().m116a(AmazonAppstoreBillingService.JSON_KEY_USER_ID)));
    }
}
