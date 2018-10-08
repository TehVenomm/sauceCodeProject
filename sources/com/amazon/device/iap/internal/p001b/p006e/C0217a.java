package com.amazon.device.iap.internal.p001b.p006e;

import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserDataResponse;
import com.amazon.device.iap.model.UserDataResponse.RequestStatus;

/* renamed from: com.amazon.device.iap.internal.b.e.a */
public final class C0217a extends C0197e {
    public C0217a(RequestId requestId) {
        super(requestId);
        C0193i c0219c = new C0219c(this);
        c0219c.m59b(new C0220d(this));
        m68a(c0219c);
    }

    /* renamed from: a */
    public void mo1188a() {
        m69a((Object) (UserDataResponse) m73d().m115a());
    }

    /* renamed from: b */
    public void mo1189b() {
        Object obj = (UserDataResponse) m73d().m115a();
        if (obj == null) {
            obj = new UserDataResponseBuilder().setRequestId(m72c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        m69a(obj);
    }
}
