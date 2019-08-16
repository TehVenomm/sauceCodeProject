package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1408n;

/* renamed from: net.gogame.gopay.sdk.iab.av */
final class C1614av implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1408n f1241a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1242b;

    C1614av(C1376at atVar, C1408n nVar) {
        this.f1242b = atVar;
        this.f1241a = nVar;
    }

    public final void run() {
        PurchaseActivity.m802a(this.f1242b.f1067b, this.f1241a);
    }
}
