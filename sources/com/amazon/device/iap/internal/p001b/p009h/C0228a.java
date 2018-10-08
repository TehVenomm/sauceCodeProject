package com.amazon.device.iap.internal.p001b.p009h;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.venezia.command.SuccessResult;

/* renamed from: com.amazon.device.iap.internal.b.h.a */
public class C0228a extends C0193i {
    public C0228a(C0197e c0197e, String str, String str2) {
        super(c0197e, "submit_metric", "1.0");
        m56a("metricName", str);
        m56a("metricAttributes", str2);
        m60b(false);
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws RemoteException, KiwiException {
        return true;
    }
}
