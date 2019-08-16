package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.support.C1653r;

/* renamed from: net.gogame.gopay.sdk.iab.bj */
final class C1619bj implements C1653r {

    /* renamed from: a */
    final /* synthetic */ C1362g f1261a;

    /* renamed from: b */
    final /* synthetic */ C1386bi f1262b;

    C1619bj(C1386bi biVar, C1362g gVar) {
        this.f1262b = biVar;
        this.f1261a = gVar;
    }

    /* renamed from: a */
    public final void mo22684a() {
        boolean z = true;
        int i = this.f1262b.f1089a.getResources().getConfiguration().orientation;
        PurchaseActivity purchaseActivity = this.f1262b.f1089a;
        C1362g gVar = this.f1261a;
        if (i != 1) {
            z = false;
        }
        PurchaseActivity.m799a(purchaseActivity, gVar, z);
    }
}
