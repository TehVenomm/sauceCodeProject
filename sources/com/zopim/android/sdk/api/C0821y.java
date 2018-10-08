package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.y */
class C0821y implements Runnable {
    /* renamed from: a */
    final /* synthetic */ String f680a;
    /* renamed from: b */
    final /* synthetic */ C0820x f681b;

    C0821y(C0820x c0820x, String str) {
        this.f681b = c0820x;
        this.f680a = str;
    }

    public void run() {
        if (this.f681b.f678d != null) {
            this.f681b.f678d.loadUrl(this.f680a);
        } else {
            Log.w(C0820x.f675b, "Can't run the web function, web view is null. WebBinder should be initialized.");
        }
    }
}
