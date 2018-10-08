package com.amazon.device.iap.internal.p005b.p007b;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.p005b.C0197e;
import com.amazon.device.iap.internal.p014c.C0232b;

/* renamed from: com.amazon.device.iap.internal.b.b.b */
public final class C0202b extends C0201a {
    public C0202b(C0197e c0197e, String str) {
        super(c0197e, "1.0", str);
    }

    protected void preExecution() throws KiwiException {
        super.preExecution();
        C0232b.m131a().m133b(m61c());
    }
}
