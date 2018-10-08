package com.amazon.device.iap.internal.p005b.p008c;

import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0193i;
import com.amazon.device.iap.internal.p005b.C0197e;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c.d */
public final class C0209d extends C0197e {
    public C0209d(RequestId requestId, Set<String> set) {
        super(requestId);
        C0193i c0207a = new C0207a(this, set);
        c0207a.m59b(new C0208b(this, set));
        m68a(c0207a);
    }

    /* renamed from: a */
    public void mo1188a() {
        m69a((Object) (ProductDataResponse) m73d().m115a());
    }

    /* renamed from: b */
    public void mo1189b() {
        Object obj = (ProductDataResponse) m73d().m115a();
        if (obj == null) {
            obj = new ProductDataResponseBuilder().setRequestId(m72c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        m69a(obj);
    }
}
