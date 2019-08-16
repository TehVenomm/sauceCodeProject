package com.amazon.device.iap.internal.p005b;

import android.content.Context;
import android.os.Handler;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.util.C0406b;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserDataResponse;

/* renamed from: com.amazon.device.iap.internal.b.e */
public class C0378e {
    /* access modifiers changed from: private */

    /* renamed from: a */
    public static final String f61a = C0378e.class.getSimpleName();

    /* renamed from: b */
    private final RequestId f62b;

    /* renamed from: c */
    private final C0391h f63c = new C0391h();

    /* renamed from: d */
    private C0393i f64d = null;

    public C0378e(RequestId requestId) {
        this.f62b = requestId;
    }

    /* renamed from: a */
    public void mo6208a() {
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6217a(C0393i iVar) {
        this.f64d = iVar;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6218a(Object obj) {
        mo6219a(obj, null);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6219a(final Object obj, final C0393i iVar) {
        C0408d.m164a(obj, "response");
        Context b = C0401d.m137d().mo6264b();
        final PurchasingListener a = C0401d.m137d().mo6257a();
        if (b == null || a == null) {
            C0409e.m168a(f61a, "PurchasingListener is not set. Dropping response: " + obj);
            return;
        }
        new Handler(b.getMainLooper()).post(new Runnable() {
            public void run() {
                C0378e.this.mo6221d().mo6228a("notifyListenerResult", Boolean.FALSE);
                try {
                    if (obj instanceof ProductDataResponse) {
                        a.onProductDataResponse((ProductDataResponse) obj);
                    } else if (obj instanceof UserDataResponse) {
                        a.onUserDataResponse((UserDataResponse) obj);
                    } else if (obj instanceof PurchaseUpdatesResponse) {
                        PurchaseUpdatesResponse purchaseUpdatesResponse = (PurchaseUpdatesResponse) obj;
                        a.onPurchaseUpdatesResponse(purchaseUpdatesResponse);
                        Object a = C0378e.this.mo6221d().mo6226a("newCursor");
                        if (a != null && (a instanceof String)) {
                            C0406b.m162a(purchaseUpdatesResponse.getUserData().getUserId(), a.toString());
                        }
                    } else if (obj instanceof PurchaseResponse) {
                        a.onPurchaseResponse((PurchaseResponse) obj);
                    } else {
                        C0409e.m170b(C0378e.f61a, "Unknown response type:" + obj.getClass().getName());
                    }
                    C0378e.this.mo6221d().mo6228a("notifyListenerResult", Boolean.TRUE);
                } catch (Throwable th) {
                    C0409e.m170b(C0378e.f61a, "Error in sendResponse: " + th);
                }
                if (iVar != null) {
                    iVar.mo6230a(true);
                    iVar.mo6224a_();
                }
            }
        });
    }

    /* renamed from: b */
    public void mo6209b() {
    }

    /* renamed from: c */
    public RequestId mo6220c() {
        return this.f62b;
    }

    /* renamed from: d */
    public C0391h mo6221d() {
        return this.f63c;
    }

    /* renamed from: e */
    public void mo6222e() {
        if (this.f64d != null) {
            this.f64d.mo6224a_();
        } else {
            mo6208a();
        }
    }
}
