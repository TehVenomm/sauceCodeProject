package com.amazon.device.iap.internal.p001b;

import android.content.Context;
import android.os.Handler;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.util.C0241b;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserDataResponse;

/* renamed from: com.amazon.device.iap.internal.b.e */
public class C0197e {
    /* renamed from: a */
    private static final String f31a = C0197e.class.getSimpleName();
    /* renamed from: b */
    private final RequestId f32b;
    /* renamed from: c */
    private final C0229h f33c = new C0229h();
    /* renamed from: d */
    private C0193i f34d = null;

    public C0197e(RequestId requestId) {
        this.f32b = requestId;
    }

    /* renamed from: a */
    public void mo1188a() {
    }

    /* renamed from: a */
    protected void m68a(C0193i c0193i) {
        this.f34d = c0193i;
    }

    /* renamed from: a */
    protected void m69a(Object obj) {
        m70a(obj, null);
    }

    /* renamed from: a */
    protected void m70a(final Object obj, final C0193i c0193i) {
        C0243d.m169a(obj, "response");
        Context b = C0236d.m142d().m151b();
        final PurchasingListener a = C0236d.m142d().m144a();
        if (b == null || a == null) {
            C0244e.m173a(f31a, "PurchasingListener is not set. Dropping response: " + obj);
            return;
        }
        new Handler(b.getMainLooper()).post(new Runnable(this) {
            /* renamed from: d */
            final /* synthetic */ C0197e f60d;

            public void run() {
                this.f60d.m73d().m118a("notifyListenerResult", Boolean.FALSE);
                try {
                    if (obj instanceof ProductDataResponse) {
                        a.onProductDataResponse((ProductDataResponse) obj);
                    } else if (obj instanceof UserDataResponse) {
                        a.onUserDataResponse((UserDataResponse) obj);
                    } else if (obj instanceof PurchaseUpdatesResponse) {
                        PurchaseUpdatesResponse purchaseUpdatesResponse = (PurchaseUpdatesResponse) obj;
                        a.onPurchaseUpdatesResponse(purchaseUpdatesResponse);
                        Object a = this.f60d.m73d().m116a("newCursor");
                        if (a != null && (a instanceof String)) {
                            C0241b.m167a(purchaseUpdatesResponse.getUserData().getUserId(), a.toString());
                        }
                    } else if (obj instanceof PurchaseResponse) {
                        a.onPurchaseResponse((PurchaseResponse) obj);
                    } else {
                        C0244e.m175b(C0197e.f31a, "Unknown response type:" + obj.getClass().getName());
                    }
                    this.f60d.m73d().m118a("notifyListenerResult", Boolean.TRUE);
                } catch (Throwable th) {
                    C0244e.m175b(C0197e.f31a, "Error in sendResponse: " + th);
                }
                if (c0193i != null) {
                    c0193i.m54a(true);
                    c0193i.a_();
                }
            }
        });
    }

    /* renamed from: b */
    public void mo1189b() {
    }

    /* renamed from: c */
    public RequestId m72c() {
        return this.f32b;
    }

    /* renamed from: d */
    public C0229h m73d() {
        return this.f33c;
    }

    /* renamed from: e */
    public void m74e() {
        if (this.f34d != null) {
            this.f34d.a_();
        } else {
            mo1188a();
        }
    }
}
