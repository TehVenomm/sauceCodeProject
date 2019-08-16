package com.amazon.device.iap.internal.p005b;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.internal.C0394c;
import com.amazon.device.iap.internal.p005b.p006a.C0361d;
import com.amazon.device.iap.internal.p005b.p007b.C0367d;
import com.amazon.device.iap.internal.p005b.p008c.C0372d;
import com.amazon.device.iap.internal.p005b.p009d.C0374a;
import com.amazon.device.iap.internal.p005b.p010e.C0380a;
import com.amazon.device.iap.internal.p005b.p012g.C0390b;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import com.facebook.internal.ServerProtocol;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c */
public final class C0368c implements C0394c {

    /* renamed from: a */
    private static final String f50a = C0368c.class.getSimpleName();

    /* renamed from: a */
    public void mo6196a(Context context, Intent intent) {
        C0409e.m168a(f50a, "handleResponse");
        String stringExtra = intent.getStringExtra(ServerProtocol.DIALOG_PARAM_RESPONSE_TYPE);
        if (stringExtra == null) {
            C0409e.m168a(f50a, "Invalid response type: null");
            return;
        }
        C0409e.m168a(f50a, "Found response type: " + stringExtra);
        if ("purchase_response".equals(stringExtra)) {
            new C0361d(RequestId.fromString(intent.getStringExtra("requestId"))).mo6222e();
        }
    }

    /* renamed from: a */
    public void mo6197a(RequestId requestId) {
        C0409e.m168a(f50a, "sendGetUserData");
        new C0380a(requestId).mo6222e();
    }

    /* renamed from: a */
    public void mo6198a(RequestId requestId, String str) {
        C0409e.m168a(f50a, "sendPurchaseRequest");
        new C0367d(requestId, str).mo6222e();
    }

    /* renamed from: a */
    public void mo6199a(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        C0409e.m168a(f50a, "sendNotifyFulfillment");
        new C0390b(requestId, str, fulfillmentResult).mo6222e();
    }

    /* renamed from: a */
    public void mo6200a(RequestId requestId, Set<String> set) {
        C0409e.m168a(f50a, "sendGetProductDataRequest");
        new C0372d(requestId, set).mo6222e();
    }

    /* renamed from: a */
    public void mo6201a(RequestId requestId, boolean z) {
        C0409e.m168a(f50a, "sendGetPurchaseUpdates");
        new C0374a(requestId, z).mo6222e();
    }
}
