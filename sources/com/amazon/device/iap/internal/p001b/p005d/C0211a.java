package com.amazon.device.iap.internal.p001b.p005d;

import com.amazon.device.iap.internal.model.C0238a;
import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.p001b.p006e.C0219c;
import com.amazon.device.iap.internal.p001b.p006e.C0220d;
import com.amazon.device.iap.internal.p001b.p008g.C0225a;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import java.util.HashSet;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.d.a */
public final class C0211a extends C0197e {
    public C0211a(RequestId requestId, boolean z) {
        super(requestId);
        C0193i c0219c = new C0219c(this);
        c0219c.m55a(new C0213c(this, z));
        C0193i c0220d = new C0220d(this);
        c0220d.m55a(new C0214d(this));
        c0219c.m59b(c0220d);
        m68a(c0219c);
    }

    /* renamed from: a */
    public void mo1188a() {
        C0193i c0193i = null;
        PurchaseUpdatesResponse purchaseUpdatesResponse = (PurchaseUpdatesResponse) m73d().m115a();
        if (purchaseUpdatesResponse.getReceipts() != null && purchaseUpdatesResponse.getReceipts().size() > 0) {
            Set hashSet = new HashSet();
            for (Receipt receipt : purchaseUpdatesResponse.getReceipts()) {
                if (!C0243d.m172a(receipt.getReceiptId())) {
                    hashSet.add(receipt.getReceiptId());
                }
            }
            c0193i = new C0225a(this, hashSet, C0238a.DELIVERED.toString());
        }
        m70a(purchaseUpdatesResponse, c0193i);
    }

    /* renamed from: b */
    public void mo1189b() {
        Object a = m73d().m115a();
        if (a == null || !(a instanceof PurchaseUpdatesResponse)) {
            a = new PurchaseUpdatesResponseBuilder().setRequestId(m72c()).setRequestStatus(RequestStatus.FAILED).build();
        } else {
            PurchaseUpdatesResponse purchaseUpdatesResponse = (PurchaseUpdatesResponse) a;
        }
        m69a(a);
    }
}
