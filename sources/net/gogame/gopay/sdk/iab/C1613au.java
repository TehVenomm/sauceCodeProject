package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1408n;

/* renamed from: net.gogame.gopay.sdk.iab.au */
final class C1613au implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1408n f1239a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1240b;

    C1613au(C1376at atVar, C1408n nVar) {
        this.f1240b = atVar;
        this.f1239a = nVar;
    }

    public final void run() {
        PurchaseActivity.m802a(this.f1240b.f1067b, this.f1239a);
    }
}
