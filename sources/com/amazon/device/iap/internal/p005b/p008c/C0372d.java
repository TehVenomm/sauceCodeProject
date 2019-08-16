package com.amazon.device.iap.internal.p005b.p008c;

import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c.d */
public final class C0372d extends C0378e {
    public C0372d(RequestId requestId, Set<String> set) {
        super(requestId);
        C0369a aVar = new C0369a(this, set);
        aVar.mo6234b((C0393i) new C0370b(this, set));
        mo6217a((C0393i) aVar);
    }

    /* renamed from: a */
    public void mo6208a() {
        mo6218a((Object) (ProductDataResponse) mo6221d().mo6225a());
    }

    /* renamed from: b */
    public void mo6209b() {
        ProductDataResponse productDataResponse = (ProductDataResponse) mo6221d().mo6225a();
        if (productDataResponse == null) {
            productDataResponse = new ProductDataResponseBuilder().setRequestId(mo6220c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        mo6218a((Object) productDataResponse);
    }
}
