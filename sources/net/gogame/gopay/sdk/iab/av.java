package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1382n;

final class av implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C1382n f3465a;
    /* renamed from: b */
    final /* synthetic */ at f3466b;

    av(at atVar, C1382n c1382n) {
        this.f3466b = atVar;
        this.f3465a = c1382n;
    }

    public final void run() {
        PurchaseActivity.m3819a(this.f3466b.f3462b, this.f3465a);
    }
}
