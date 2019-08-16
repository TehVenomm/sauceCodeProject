package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.support.C1653r;

/* renamed from: net.gogame.gopay.sdk.iab.bj */
final class C1619bj implements C1653r {

    /* renamed from: a */
    final /* synthetic */ C1362g f1249a;

    /* renamed from: b */
    final /* synthetic */ C1386bi f1250b;

    C1619bj(C1386bi biVar, C1362g gVar) {
        this.f1250b = biVar;
        this.f1249a = gVar;
    }

    /* renamed from: a */
    public final void mo22684a() {
        boolean z = true;
        int i = this.f1250b.f1083a.getResources().getConfiguration().orientation;
        PurchaseActivity purchaseActivity = this.f1250b.f1083a;
        C1362g gVar = this.f1249a;
        if (i != 1) {
            z = false;
        }
        PurchaseActivity.m799a(purchaseActivity, gVar, z);
    }
}
