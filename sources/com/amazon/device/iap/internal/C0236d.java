package com.amazon.device.iap.internal;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.d */
public class C0236d {
    /* renamed from: a */
    private static String f88a = C0236d.class.getSimpleName();
    /* renamed from: b */
    private static String f89b = "sku";
    /* renamed from: c */
    private static C0236d f90c = new C0236d();
    /* renamed from: d */
    private final C0189c f91d = C0237e.m155b();
    /* renamed from: e */
    private Context f92e;
    /* renamed from: f */
    private PurchasingListener f93f;

    private C0236d() {
    }

    /* renamed from: d */
    public static C0236d m142d() {
        return f90c;
    }

    /* renamed from: e */
    private void m143e() {
        if (this.f93f == null) {
            throw new IllegalStateException("You must register a PurchasingListener before invoking this operation");
        }
    }

    /* renamed from: a */
    public PurchasingListener m144a() {
        return this.f93f;
    }

    /* renamed from: a */
    public RequestId m145a(String str) {
        C0243d.m169a((Object) str, f89b);
        m143e();
        RequestId requestId = new RequestId();
        this.f91d.mo1182a(requestId, str);
        return requestId;
    }

    /* renamed from: a */
    public RequestId m146a(Set<String> set) {
        C0243d.m169a((Object) set, "skus");
        C0243d.m171a((Collection) set, "skus");
        for (String trim : set) {
            if (trim.trim().length() == 0) {
                throw new IllegalArgumentException("Empty SKU values are not allowed");
            }
        }
        if (set.size() > 100) {
            throw new IllegalArgumentException(set.size() + " SKUs were provided, but no more than " + 100 + " SKUs are allowed");
        }
        m143e();
        RequestId requestId = new RequestId();
        this.f91d.mo1184a(requestId, new LinkedHashSet(set));
        return requestId;
    }

    /* renamed from: a */
    public RequestId m147a(boolean z) {
        m143e();
        RequestId requestId = new RequestId();
        this.f91d.mo1185a(requestId, z);
        return requestId;
    }

    /* renamed from: a */
    public void m148a(Context context, Intent intent) {
        try {
            this.f91d.mo1180a(context, intent);
        } catch (Exception e) {
            C0244e.m175b(f88a, "Error in onReceive: " + e);
        }
    }

    /* renamed from: a */
    public void m149a(Context context, PurchasingListener purchasingListener) {
        C0244e.m173a(f88a, "PurchasingListener registered: " + purchasingListener);
        C0244e.m173a(f88a, "PurchasingListener Context: " + context);
        if (purchasingListener == null || context == null) {
            throw new IllegalArgumentException("Neither PurchasingListener or its Context can be null");
        }
        this.f92e = context.getApplicationContext();
        this.f93f = purchasingListener;
    }

    /* renamed from: a */
    public void m150a(String str, FulfillmentResult fulfillmentResult) {
        if (C0243d.m172a(str)) {
            throw new IllegalArgumentException("Empty receiptId is not allowed");
        }
        C0243d.m169a((Object) fulfillmentResult, "fulfillmentResult");
        m143e();
        this.f91d.mo1183a(new RequestId(), str, fulfillmentResult);
    }

    /* renamed from: b */
    public Context m151b() {
        return this.f92e;
    }

    /* renamed from: c */
    public RequestId m152c() {
        m143e();
        RequestId requestId = new RequestId();
        this.f91d.mo1181a(requestId);
        return requestId;
    }
}
