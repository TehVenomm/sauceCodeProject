package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.q */
class C0814q extends C0800u<Void> {
    /* renamed from: a */
    final /* synthetic */ C0813p f663a;

    C0814q(C0813p c0813p) {
        this.f663a = c0813p;
    }

    /* renamed from: a */
    public void mo4226a(ErrorResponse errorResponse) {
        Log.e(C0813p.f658c, "Error occurred. Reason: " + errorResponse);
        this.f663a.f661d = errorResponse;
    }

    /* renamed from: a */
    public void m621a(Void voidR) {
        this.f663a.f662e = true;
    }
}
