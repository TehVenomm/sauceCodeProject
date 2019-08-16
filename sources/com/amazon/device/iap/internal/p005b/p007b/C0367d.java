package com.amazon.device.iap.internal.p005b.p007b;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.RequestId;

/* renamed from: com.amazon.device.iap.internal.b.b.d */
public final class C0367d extends C0378e {
    public C0367d(RequestId requestId, String str) {
        super(requestId);
        C0366c cVar = new C0366c(this, str);
        cVar.mo6234b((C0393i) new C0365b(this, str));
        mo6217a((C0393i) cVar);
    }

    /* renamed from: a */
    public void mo6208a() {
    }

    /* renamed from: b */
    public void mo6209b() {
        PurchaseResponse purchaseResponse = (PurchaseResponse) mo6221d().mo6225a();
        if (purchaseResponse == null) {
            purchaseResponse = new PurchaseResponseBuilder().setRequestId(mo6220c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        mo6218a((Object) purchaseResponse);
    }
}
