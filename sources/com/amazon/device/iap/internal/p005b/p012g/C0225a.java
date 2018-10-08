package com.amazon.device.iap.internal.p005b.p012g;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.C0238a;
import com.amazon.device.iap.internal.p005b.C0193i;
import com.amazon.device.iap.internal.p005b.C0197e;
import com.amazon.venezia.command.SuccessResult;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.g.a */
public final class C0225a extends C0193i {
    /* renamed from: a */
    protected final Set<String> f65a;
    /* renamed from: b */
    protected final String f66b;

    public C0225a(C0197e c0197e, Set<String> set, String str) {
        super(c0197e, "purchase_fulfilled", "2.0");
        this.f65a = set;
        this.f66b = str;
        m60b(false);
        m56a("receiptIds", this.f65a);
        m56a("fulfillmentStatus", this.f66b);
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws RemoteException, KiwiException {
        return true;
    }

    public void a_() {
        Object a = m58b().m73d().m116a("notifyListenerResult");
        if (a != null && Boolean.FALSE.equals(a)) {
            m56a("fulfillmentStatus", C0238a.DELIVERY_ATTEMPTED.toString());
        }
        super.a_();
    }
}
