package com.amazon.device.iap.internal.p005b.p013h;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.venezia.command.SuccessResult;

/* renamed from: com.amazon.device.iap.internal.b.h.a */
public class C0392a extends C0393i {
    public C0392a(C0378e eVar, String str, String str2) {
        super(eVar, "submit_metric", "1.0");
        mo6232a("metricName", str);
        mo6232a("metricAttributes", str2);
        mo6235b(false);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws RemoteException, KiwiException {
        return true;
    }
}
