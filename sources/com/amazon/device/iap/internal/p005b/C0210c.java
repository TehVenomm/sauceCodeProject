package com.amazon.device.iap.internal.p005b;

import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.internal.C0189c;
import com.amazon.device.iap.internal.p005b.p006a.C0198d;
import com.amazon.device.iap.internal.p005b.p007b.C0204d;
import com.amazon.device.iap.internal.p005b.p008c.C0209d;
import com.amazon.device.iap.internal.p005b.p009d.C0211a;
import com.amazon.device.iap.internal.p005b.p010e.C0217a;
import com.amazon.device.iap.internal.p005b.p012g.C0226b;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import com.facebook.internal.ServerProtocol;
import java.util.Set;

/* renamed from: com.amazon.device.iap.internal.b.c */
public final class C0210c implements C0189c {
    /* renamed from: a */
    private static final String f49a = C0210c.class.getSimpleName();

    /* renamed from: a */
    public void mo1180a(Context context, Intent intent) {
        C0244e.m173a(f49a, "handleResponse");
        String stringExtra = intent.getStringExtra(ServerProtocol.DIALOG_PARAM_RESPONSE_TYPE);
        if (stringExtra == null) {
            C0244e.m173a(f49a, "Invalid response type: null");
            return;
        }
        C0244e.m173a(f49a, "Found response type: " + stringExtra);
        if ("purchase_response".equals(stringExtra)) {
            new C0198d(RequestId.fromString(intent.getStringExtra("requestId"))).m74e();
        }
    }

    /* renamed from: a */
    public void mo1181a(RequestId requestId) {
        C0244e.m173a(f49a, "sendGetUserData");
        new C0217a(requestId).m74e();
    }

    /* renamed from: a */
    public void mo1182a(RequestId requestId, String str) {
        C0244e.m173a(f49a, "sendPurchaseRequest");
        new C0204d(requestId, str).m74e();
    }

    /* renamed from: a */
    public void mo1183a(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        C0244e.m173a(f49a, "sendNotifyFulfillment");
        new C0226b(requestId, str, fulfillmentResult).m74e();
    }

    /* renamed from: a */
    public void mo1184a(RequestId requestId, Set<String> set) {
        C0244e.m173a(f49a, "sendGetProductDataRequest");
        new C0209d(requestId, set).m74e();
    }

    /* renamed from: a */
    public void mo1185a(RequestId requestId, boolean z) {
        C0244e.m173a(f49a, "sendGetPurchaseUpdates");
        new C0211a(requestId, z).m74e();
    }
}
