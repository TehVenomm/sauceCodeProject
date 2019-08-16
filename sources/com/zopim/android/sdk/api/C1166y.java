package com.zopim.android.sdk.api;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.api.y */
class C1166y implements Runnable {

    /* renamed from: a */
    final /* synthetic */ String f724a;

    /* renamed from: b */
    final /* synthetic */ C1164x f725b;

    C1166y(C1164x xVar, String str) {
        this.f725b = xVar;
        this.f724a = str;
    }

    public void run() {
        if (this.f725b.f720d != null) {
            this.f725b.f720d.loadUrl(this.f724a);
        } else {
            Log.w(C1164x.f717b, "Can't run the web function, web view is null. WebBinder should be initialized.");
        }
    }
}
