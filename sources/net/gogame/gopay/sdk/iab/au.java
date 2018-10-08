package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1066n;

final class au implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1066n f1075a;
    /* renamed from: b */
    final /* synthetic */ at f1076b;

    au(at atVar, C1066n c1066n) {
        this.f1076b = atVar;
        this.f1075a = c1066n;
    }

    public final void run() {
        PurchaseActivity.m794a(this.f1076b.f1074b, this.f1075a);
    }
}
