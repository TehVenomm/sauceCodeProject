package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1066n;

final class av implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1066n f1077a;
    /* renamed from: b */
    final /* synthetic */ at f1078b;

    av(at atVar, C1066n c1066n) {
        this.f1078b = atVar;
        this.f1077a = c1066n;
    }

    public final void run() {
        PurchaseActivity.m794a(this.f1078b.f1074b, this.f1077a);
    }
}
