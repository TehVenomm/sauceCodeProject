package com.zopim.android.sdk.data;

/* renamed from: com.zopim.android.sdk.data.l */
class C0873l implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Runnable f873a;
    /* renamed from: b */
    final /* synthetic */ C0872k f874b;

    C0873l(C0872k c0872k, Runnable runnable) {
        this.f874b = c0872k;
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
