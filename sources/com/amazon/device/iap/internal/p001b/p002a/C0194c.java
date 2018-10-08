package com.amazon.device.iap.internal.p001b.p002a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p001b.C0193i;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;

/* renamed from: com.amazon.device.iap.internal.b.a.c */
abstract class C0194c extends C0193i {
    C0194c(C0197e c0197e, String str) {
        super(c0197e, "purchase_response", str);
    }

    /* renamed from: a */
    protected void m62a(String str, String str2, String str3, RequestStatus requestStatus) {
        C0197e b = m58b();
        b.m73d().m117a(new PurchaseResponseBuilder().setRequestId(b.m72c()).setRequestStatus(requestStatus).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str2).build()).setReceipt(null).build());
    }
}
