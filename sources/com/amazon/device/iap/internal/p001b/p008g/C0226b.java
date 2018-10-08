package com.amazon.device.iap.internal.p001b.p008g;

import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.p001b.p007f.C0222b;
import com.amazon.device.iap.internal.p010c.C0231a;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.HashSet;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.g.b */
public final class C0226b extends C0197e {
    /* renamed from: a */
    private final String f67a;
    /* renamed from: b */
    private final FulfillmentResult f68b;

    public C0226b(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        super(requestId);
        Set hashSet = new HashSet();
        hashSet.add(str);
        this.f67a = str;
        this.f68b = fulfillmentResult;
        m68a(new C0225a(this, hashSet, fulfillmentResult.toString()));
    }

    /* renamed from: a */
    public void mo1188a() {
    }

    /* renamed from: b */
    public void mo1189b() {
        if (FulfillmentResult.FULFILLED == this.f68b || FulfillmentResult.UNAVAILABLE == this.f68b) {
            String c = C0231a.m120a().m130c(this.f67a);
            if (c != null) {
                new C0222b(this, c).a_();
                C0231a.m120a().m127a(this.f67a);
            }
        }
    }
}
