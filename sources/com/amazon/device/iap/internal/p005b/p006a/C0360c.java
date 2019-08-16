package com.amazon.device.iap.internal.p005b.p006a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;

/* renamed from: com.amazon.device.iap.internal.b.a.c */
abstract class C0360c extends C0393i {
    C0360c(C0378e eVar, String str) {
        super(eVar, "purchase_response", str);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6207a(String str, String str2, String str3, RequestStatus requestStatus) {
        C0378e b = mo6233b();
        b.mo6221d().mo6227a((Object) new PurchaseResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(requestStatus).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str2).build()).setReceipt(null).build());
    }
}
