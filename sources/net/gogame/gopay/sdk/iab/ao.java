package net.gogame.gopay.sdk.iab;

final class ao implements Runnable {
    /* renamed from: a */
    final /* synthetic */ an f1068a;

    ao(an anVar) {
        this.f1068a = anVar;
    }

    public final void run() {
        if (!this.f1068a.f1067a.f1012H.canRetry() || this.f1068a.f1067a.f1046y == null) {
            this.f1068a.f1067a.f1024c = true;
            this.f1068a.f1067a.f1030i = null;
            this.f1068a.f1067a.onBackPressed();
            return;
        }
        this.f1068a.f1067a.f1046y.loadUrl(this.f1068a.f1067a.f1012H.getFailedUrl());
    }
}
