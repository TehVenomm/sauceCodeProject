package com.zopim.android.sdk.data;

/* renamed from: com.zopim.android.sdk.data.l */
class C0874l implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Runnable f873a;
    /* renamed from: b */
    final /* synthetic */ C0873k f874b;

    C0874l(C0873k c0873k, Runnable runnable) {
        this.f874b = c0873k;
        this.f873a = runnable;
    }

    public void run() {
        try {
            this.f873a.run();
        } finally {
            this.f874b.m704a();
        }
    }
}
