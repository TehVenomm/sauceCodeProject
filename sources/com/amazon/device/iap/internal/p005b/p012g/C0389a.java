package com.amazon.device.iap.internal.p005b.p012g;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.C0403a;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.venezia.command.SuccessResult;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.g.a */
public final class C0389a extends C0393i {

    /* renamed from: a */
    protected final Set<String> f74a;

    /* renamed from: b */
    protected final String f75b;

    public C0389a(C0378e eVar, Set<String> set, String str) {
        super(eVar, "purchase_fulfilled", "2.0");
        this.f74a = set;
        this.f75b = str;
        mo6235b(false);
        mo6232a("receiptIds", this.f74a);
        mo6232a("fulfillmentStatus", this.f75b);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws RemoteException, KiwiException {
        return true;
    }

    /* renamed from: a_ */
    public void mo6224a_() {
        Object a = mo6233b().mo6221d().mo6226a("notifyListenerResult");
        if (a != null && Boolean.FALSE.equals(a)) {
            mo6232a("fulfillmentStatus", C0403a.DELIVERY_ATTEMPTED.toString());
        }
        super.mo6224a_();
    }
}
