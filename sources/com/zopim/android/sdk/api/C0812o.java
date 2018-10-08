package com.zopim.android.sdk.api;

import android.util.Log;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.o */
class C0812o extends C0800u<File> {
    /* renamed from: a */
    final /* synthetic */ C0811n f657a;

    C0812o(C0811n c0811n) {
        this.f657a = c0811n;
    }

    /* renamed from: a */
    public void mo4226a(ErrorResponse errorResponse) {
        Log.e(C0811n.f653b, "Error occurred. Reason: " + errorResponse.mo4229a());
        this.f657a.f656d = errorResponse;
    }

    /* renamed from: a */
    public void m609a(File file) {
        this.f657a.f655c = file;
    }
}
