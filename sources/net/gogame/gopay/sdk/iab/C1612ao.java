package net.gogame.gopay.sdk.iab;

/* renamed from: net.gogame.gopay.sdk.iab.ao */
final class C1612ao implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1371an f1250a;

    C1612ao(C1371an anVar) {
        this.f1250a = anVar;
    }

    public final void run() {
        if (!this.f1250a.f1067a.f1023H.canRetry() || this.f1250a.f1067a.f1057y == null) {
            this.f1250a.f1067a.f1035c = true;
            this.f1250a.f1067a.f1041i = null;
            this.f1250a.f1067a.onBackPressed();
            return;
        }
        this.f1250a.f1067a.f1057y.loadUrl(this.f1250a.f1067a.f1023H.getFailedUrl());
    }
}
