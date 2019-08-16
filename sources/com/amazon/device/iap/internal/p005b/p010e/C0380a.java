package com.amazon.device.iap.internal.p005b.p010e;

import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserDataResponse;
import com.amazon.device.iap.model.UserDataResponse.RequestStatus;

/* renamed from: com.amazon.device.iap.internal.b.e.a */
public final class C0380a extends C0378e {
    public C0380a(RequestId requestId) {
        super(requestId);
        C0382c cVar = new C0382c(this);
        cVar.mo6234b((C0393i) new C0383d(this));
        mo6217a((C0393i) cVar);
    }

    /* renamed from: a */
    public void mo6208a() {
        mo6218a((Object) (UserDataResponse) mo6221d().mo6225a());
    }

    /* renamed from: b */
    public void mo6209b() {
        UserDataResponse userDataResponse = (UserDataResponse) mo6221d().mo6225a();
        if (userDataResponse == null) {
            userDataResponse = new UserDataResponseBuilder().setRequestId(mo6220c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        mo6218a((Object) userDataResponse);
    }
}
