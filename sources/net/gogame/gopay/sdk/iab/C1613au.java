package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1408n;

/* renamed from: net.gogame.gopay.sdk.iab.au */
final class C1613au implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1408n f1251a;

    /* renamed from: b */
    final /* synthetic */ C1376at f1252b;

    C1613au(C1376at atVar, C1408n nVar) {
        this.f1252b = atVar;
        this.f1251a = nVar;
    }

    public final void run() {
        PurchaseActivity.m802a(this.f1252b.f1073b, this.f1251a);
    }
}
