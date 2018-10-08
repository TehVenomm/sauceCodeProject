package com.zopim.android.sdk.api;

import android.util.Log;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.o */
class C0811o extends C0799u<File> {
    /* renamed from: a */
    final /* synthetic */ C0810n f657a;

    C0811o(C0810n c0810n) {
        this.f657a = c0810n;
    }

    /* renamed from: a */
    public void mo4228a(ErrorResponse errorResponse) {
        Log.e(C0810n.f653b, "Error occurred. Reason: " + errorResponse.mo4231a());
        this.f657a.f656d = errorResponse;
    }

    /* renamed from: a */
    public void m609a(File file) {
        this.f657a.f655c = file;
    }
}
