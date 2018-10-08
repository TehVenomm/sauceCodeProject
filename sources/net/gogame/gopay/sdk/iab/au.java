package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1382n;

final class au implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1382n f3463a;
    /* renamed from: b */
    final /* synthetic */ at f3464b;

    au(at atVar, C1382n c1382n) {
        this.f3464b = atVar;
        this.f3463a = c1382n;
    }

    public final void run() {
        PurchaseActivity.m3819a(this.f3464b.f3462b, this.f3463a);
    }
}
