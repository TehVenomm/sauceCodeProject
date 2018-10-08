package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.y */
class C0822y implements Runnable {
    /* renamed from: a */
    final /* synthetic */ String f680a;
    /* renamed from: b */
    final /* synthetic */ C0821x f681b;

    C0822y(C0821x c0821x, String str) {
        this.f681b = c0821x;
        this.f680a = str;
    }

    public void run() {
        if (this.f681b.f678d != null) {
            this.f681b.f678d.loadUrl(this.f680a);
        } else {
            Log.w(C0821x.f675b, "Can't run the web function, web view is null. WebBinder should be initialized.");
        }
    }
}
