package com.zopim.android.sdk.api;

import android.util.Log;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.o */
class C1155o extends C1161u<File> {

    /* renamed from: a */
    final /* synthetic */ C1154n f700a;

    C1155o(C1154n nVar) {
        this.f700a = nVar;
    }

    /* renamed from: a */
    public void mo20643a(ErrorResponse errorResponse) {
        Log.e(C1154n.f696b, "Error occurred. Reason: " + errorResponse.mo20621a());
        this.f700a.f699d = errorResponse;
    }

    /* renamed from: a */
    public void mo20644a(File file) {
        this.f700a.f698c = file;
    }
}
