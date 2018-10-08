package net.gogame.gopay.sdk.iab;

final class ao implements Runnable {
    /* renamed from: a */
    final /* synthetic */ an f3456a;

    ao(an anVar) {
        this.f3456a = anVar;
    }

    public final void run() {
        if (!this.f3456a.f3455a.f3400H.canRetry() || this.f3456a.f3455a.f3434y == null) {
            this.f3456a.f3455a.f3412c = true;
            this.f3456a.f3455a.f3418i = null;
            this.f3456a.f3455a.onBackPressed();
            return;
        }
        this.f3456a.f3455a.f3434y.loadUrl(this.f3456a.f3455a.f3400H.getFailedUrl());
    }
}
