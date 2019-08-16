package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1408n;

/* renamed from: net.gogame.gopay.sdk.iab.av */
final class C1614av implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1408n f1253a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1254b;

    C1614av(C1376at atVar, C1408n nVar) {
        this.f1254b = atVar;
        this.f1253a = nVar;
    }

    public final void run() {
        PurchaseActivity.m802a(this.f1254b.f1073b, this.f1253a);
    }
}
