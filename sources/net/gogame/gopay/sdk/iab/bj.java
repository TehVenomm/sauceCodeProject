package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1033g;
import net.gogame.gopay.sdk.support.C1039r;

final class bj implements C1039r {
    /* renamed from: a */
    final /* synthetic */ C1033g f1101a;
    /* renamed from: b */
    final /* synthetic */ bi f1102b;

    bj(bi biVar, C1033g c1033g) {
        this.f1102b = biVar;
        this.f1101a = c1033g;
    }

    /* renamed from: a */
    public final void mo4425a() {
        boolean z = true;
        int i = this.f1102b.f1100a.getResources().getConfiguration().orientation;
        PurchaseActivity purchaseActivity = this.f1102b.f1100a;
        C1033g c1033g = this.f1101a;
        if (i != 1) {
            z = false;
        }
        PurchaseActivity.m791a(purchaseActivity, c1033g, z);
    }
}
