package com.amazon.device.iap.internal.p005b.p006a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.internal.p005b.p011f.C0386b;
import com.amazon.device.iap.internal.p005b.p011f.C0387c;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;

/* renamed from: com.amazon.device.iap.internal.b.a.d */
public final class C0361d extends C0378e {
    public C0361d(RequestId requestId) {
        super(requestId);
        C0358a aVar = new C0358a(this);
        aVar.mo6234b((C0393i) new C0359b(this));
        mo6217a((C0393i) aVar);
    }

    /* renamed from: a */
    public void mo6208a() {
        PurchaseResponse purchaseResponse = (PurchaseResponse) mo6221d().mo6225a();
        if (purchaseResponse != null) {
            Receipt receipt = purchaseResponse.getReceipt();
            boolean z = receipt != null;
            C0387c cVar = new C0387c(this, z);
            if (z && (ProductType.ENTITLED == receipt.getProductType() || ProductType.SUBSCRIPTION == receipt.getProductType())) {
                cVar.mo6234b((C0393i) new C0386b(this, mo6220c().toString()));
            }
            mo6219a(purchaseResponse, cVar);
        }
    }

    /* renamed from: b */
    public void mo6209b() {
        PurchaseResponse purchaseResponse = (PurchaseResponse) mo6221d().mo6225a();
        if (purchaseResponse == null) {
            purchaseResponse = new PurchaseResponseBuilder().setRequestId(mo6220c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        mo6219a(purchaseResponse, new C0387c(this, false));
    }
}
