package com.amazon.device.iap.internal.p005b.p006a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0193i;
import com.amazon.device.iap.internal.p005b.C0197e;
import com.amazon.device.iap.internal.p005b.p011f.C0222b;
import com.amazon.device.iap.internal.p005b.p011f.C0223c;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;

/* renamed from: com.amazon.device.iap.internal.b.a.d */
public final class C0198d extends C0197e {
    public C0198d(RequestId requestId) {
        super(requestId);
        C0193i c0195a = new C0195a(this);
        c0195a.m59b(new C0196b(this));
        m68a(c0195a);
    }

    /* renamed from: a */
    public void mo1188a() {
        PurchaseResponse purchaseResponse = (PurchaseResponse) m73d().m115a();
        if (purchaseResponse != null) {
            Receipt receipt = purchaseResponse.getReceipt();
            boolean z = receipt != null;
            C0193i c0223c = new C0223c(this, z);
            if (z && (ProductType.ENTITLED == receipt.getProductType() || ProductType.SUBSCRIPTION == receipt.getProductType())) {
                c0223c.m59b(new C0222b(this, m72c().toString()));
            }
            m70a(purchaseResponse, c0223c);
        }
    }

    /* renamed from: b */
    public void mo1189b() {
        Object obj = (PurchaseResponse) m73d().m115a();
        if (obj == null) {
            obj = new PurchaseResponseBuilder().setRequestId(m72c()).setRequestStatus(RequestStatus.FAILED).build();
        }
        m70a(obj, new C0223c(this, false));
    }
}
