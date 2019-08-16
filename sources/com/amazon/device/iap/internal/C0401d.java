package com.amazon.device.iap.internal;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.d */
public class C0401d {

    /* renamed from: a */
    private static String f107a = C0401d.class.getSimpleName();

    /* renamed from: b */
    private static String f108b = "sku";

    /* renamed from: c */
    private static C0401d f109c = new C0401d();

    /* renamed from: d */
    private final C0394c f110d = C0402e.m150b();

    /* renamed from: e */
    private Context f111e;

    /* renamed from: f */
    private PurchasingListener f112f;

    private C0401d() {
    }

    /* renamed from: d */
    public static C0401d m137d() {
        return f109c;
    }

    /* renamed from: e */
    private void m138e() {
        if (this.f112f == null) {
            throw new IllegalStateException("You must register a PurchasingListener before invoking this operation");
        }
    }

    /* renamed from: a */
    public PurchasingListener mo6257a() {
        return this.f112f;
    }

    /* renamed from: a */
    public RequestId mo6258a(String str) {
        C0408d.m164a((Object) str, f108b);
        m138e();
        RequestId requestId = new RequestId();
        this.f110d.mo6198a(requestId, str);
        return requestId;
    }

    /* renamed from: a */
    public RequestId mo6259a(Set<String> set) {
        C0408d.m164a((Object) set, "skus");
        C0408d.m166a((Collection<? extends Object>) set, "skus");
        for (String trim : set) {
            if (trim.trim().length() == 0) {
                throw new IllegalArgumentException("Empty SKU values are not allowed");
            }
        }
        if (set.size() > 100) {
            throw new IllegalArgumentException(set.size() + " SKUs were provided, but no more than " + 100 + " SKUs are allowed");
        }
        m138e();
        RequestId requestId = new RequestId();
        this.f110d.mo6200a(requestId, (Set<String>) new LinkedHashSet<String>(set));
        return requestId;
    }

    /* renamed from: a */
    public RequestId mo6260a(boolean z) {
        m138e();
        RequestId requestId = new RequestId();
        this.f110d.mo6201a(requestId, z);
        return requestId;
    }

    /* renamed from: a */
    public void mo6261a(Context context, Intent intent) {
        try {
            this.f110d.mo6196a(context, intent);
        } catch (Exception e) {
            C0409e.m170b(f107a, "Error in onReceive: " + e);
        }
    }

    /* renamed from: a */
    public void mo6262a(Context context, PurchasingListener purchasingListener) {
        C0409e.m168a(f107a, "PurchasingListener registered: " + purchasingListener);
        C0409e.m168a(f107a, "PurchasingListener Context: " + context);
        if (purchasingListener == null || context == null) {
            throw new IllegalArgumentException("Neither PurchasingListener or its Context can be null");
        }
        this.f111e = context.getApplicationContext();
        this.f112f = purchasingListener;
    }

    /* renamed from: a */
    public void mo6263a(String str, FulfillmentResult fulfillmentResult) {
        if (C0408d.m167a(str)) {
            throw new IllegalArgumentException("Empty receiptId is not allowed");
        }
        C0408d.m164a((Object) fulfillmentResult, "fulfillmentResult");
        m138e();
        this.f110d.mo6199a(new RequestId(), str, fulfillmentResult);
    }

    /* renamed from: b */
    public Context mo6264b() {
        return this.f111e;
    }

    /* renamed from: c */
    public RequestId mo6265c() {
        m138e();
        RequestId requestId = new RequestId();
        this.f110d.mo6197a(requestId);
        return requestId;
    }
}
