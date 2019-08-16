package com.amazon.device.iap.internal.p005b.p012g;

import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p005b.C0393i;
import com.amazon.device.iap.internal.p005b.p011f.C0386b;
import com.amazon.device.iap.internal.p014c.C0395a;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.HashSet;

/* renamed from: com.amazon.device.iap.internal.b.g.b */
public final class C0390b extends C0378e {

    /* renamed from: a */
    private final String f76a;

    /* renamed from: b */
    private final FulfillmentResult f77b;

    public C0390b(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        super(requestId);
        HashSet hashSet = new HashSet();
        hashSet.add(str);
        this.f76a = str;
        this.f77b = fulfillmentResult;
        mo6217a((C0393i) new C0389a(this, hashSet, fulfillmentResult.toString()));
    }

    /* renamed from: a */
    public void mo6208a() {
    }

    /* renamed from: b */
    public void mo6209b() {
        if (FulfillmentResult.FULFILLED == this.f77b || FulfillmentResult.UNAVAILABLE == this.f77b) {
            String c = C0395a.m115a().mo6247c(this.f76a);
            if (c != null) {
                new C0386b(this, c).mo6224a_();
                C0395a.m115a().mo6244a(this.f76a);
            }
        }
    }
}
