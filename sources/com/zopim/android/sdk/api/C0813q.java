package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.q */
class C0813q extends C0799u<Void> {
    /* renamed from: a */
    final /* synthetic */ C0812p f663a;

    C0813q(C0812p c0812p) {
        this.f663a = c0812p;
    }

    /* renamed from: a */
    public void mo4228a(ErrorResponse errorResponse) {
        Log.e(C0812p.f658c, "Error occurred. Reason: " + errorResponse);
        this.f663a.f661d = errorResponse;
    }

    /* renamed from: a */
    public void m621a(Void voidR) {
        this.f663a.f662e = true;
    }
}
