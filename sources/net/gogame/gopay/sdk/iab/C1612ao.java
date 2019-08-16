package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.ao */
final class C1612ao implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1371an f1238a;

    C1612ao(C1371an anVar) {
        this.f1238a = anVar;
    }

    public final void run() {
        if (!this.f1238a.f1061a.f1017H.canRetry() || this.f1238a.f1061a.f1051y == null) {
            this.f1238a.f1061a.f1029c = true;
            this.f1238a.f1061a.f1035i = null;
            this.f1238a.f1061a.onBackPressed();
            return;
        }
        this.f1238a.f1061a.f1051y.loadUrl(this.f1238a.f1061a.f1017H.getFailedUrl());
    }
}
