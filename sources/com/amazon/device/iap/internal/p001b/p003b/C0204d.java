package com.amazon.device.iap.internal.p001b.p003b;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.RequestId;

/* renamed from: com.amazon.device.iap.internal.b.b.d */
public final class C0204d extends C0197e {
    public C0204d(RequestId requestId, String str) {
        super(requestId);
        C0193i c0203c = new C0203c(this, str);
        c0203c.m59b(new C0202b(this, str));
        m68a(c0203c);
    }

    /* renamed from: a */
    public void mo1188a() {
    }

    /* renamed from: b */
    public void mo1189b() {
        Object obj = (PurchaseResponse) m73d().m115a();
        if (obj == null) {
            obj = new PurchaseResponseBuilder().setRequestId(m72c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        m69a(obj);
    }
}
