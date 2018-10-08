package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.C1349g;
import net.gogame.gopay.sdk.support.C1355r;

final class bj implements C1355r {
    /* renamed from: a */
    final /* synthetic */ C1349g f3489a;
    /* renamed from: b */
    final /* synthetic */ bi f3490b;

    bj(bi biVar, C1349g c1349g) {
        this.f3490b = biVar;
        this.f3489a = c1349g;
    }

    /* renamed from: a */
    public final void mo4873a() {
        boolean z = true;
        int i = this.f3490b.f3488a.getResources().getConfiguration().orientation;
        PurchaseActivity purchaseActivity = this.f3490b.f3488a;
        C1349g c1349g = this.f3489a;
        if (i != 1) {
            z = false;
        }
        PurchaseActivity.m3816a(purchaseActivity, c1349g, z);
    }
}
