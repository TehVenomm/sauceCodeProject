package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.q */
class C1157q extends C1161u<Void> {

    /* renamed from: a */
    final /* synthetic */ C1156p f706a;

    C1157q(C1156p pVar) {
        this.f706a = pVar;
    }

    /* renamed from: a */
    public void mo20643a(ErrorResponse errorResponse) {
        Log.e(C1156p.f701c, "Error occurred. Reason: " + errorResponse);
        this.f706a.f704d = errorResponse;
    }

    /* renamed from: a */
    public void mo20644a(Void voidR) {
        this.f706a.f705e = true;
    }
}
