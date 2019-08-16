package com.amazon.device.iap.internal.p005b.p009d;

import com.amazon.device.iap.internal.model.C0403a;
import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.internal.p005b.p010e.C0382c;
import com.amazon.device.iap.internal.p005b.p010e.C0383d;
import com.amazon.device.iap.internal.p005b.p012g.C0389a;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import java.util.HashSet;

/* renamed from: com.amazon.device.iap.internal.b.d.a */
public final class C0374a extends C0378e {
    public C0374a(RequestId requestId, boolean z) {
        super(requestId);
        C0382c cVar = new C0382c(this);
        cVar.mo6231a((C0393i) new C0376c(this, z));
        C0383d dVar = new C0383d(this);
        dVar.mo6231a((C0393i) new C0377d(this));
        cVar.mo6234b((C0393i) dVar);
        mo6217a((C0393i) cVar);
    }

    /* renamed from: a */
    public void mo6208a() {
        C0389a aVar = null;
        PurchaseUpdatesResponse purchaseUpdatesResponse = (PurchaseUpdatesResponse) mo6221d().mo6225a();
        if (purchaseUpdatesResponse.getReceipts() != null && purchaseUpdatesResponse.getReceipts().size() > 0) {
            HashSet hashSet = new HashSet();
            for (Receipt receipt : purchaseUpdatesResponse.getReceipts()) {
                if (!C0408d.m167a(receipt.getReceiptId())) {
                    hashSet.add(receipt.getReceiptId());
                }
            }
            aVar = new C0389a(this, hashSet, C0403a.DELIVERED.toString());
        }
        mo6219a(purchaseUpdatesResponse, aVar);
    }

    /* renamed from: b */
    public void mo6209b() {
        Object a = mo6221d().mo6225a();
        mo6218a((Object) (a == null || !(a instanceof PurchaseUpdatesResponse)) ? new PurchaseUpdatesResponseBuilder().setRequestId(mo6220c()).setRequestStatus(RequestStatus.FAILED).build() : (PurchaseUpdatesResponse) a);
    }
}
