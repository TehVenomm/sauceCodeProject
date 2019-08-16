package com.zopim.android.sdk.data;

/* renamed from: com.zopim.android.sdk.data.l */
class C1243l implements Runnable {

    /* renamed from: a */
    final /* synthetic */ Runnable f917a;

    /* renamed from: b */
    final /* synthetic */ C1242k f918b;

    C1243l(C1242k kVar, Runnable runnable) {
        this.f918b = kVar;
        this.f917a = runnable;
    }

    public void run() {
        try {
            this.f917a.run();
        } finally {
            this.f918b.mo20821a();
        }
    }
}
