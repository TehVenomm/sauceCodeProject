package net.gogame.gopay.sdk.iab;

import android.widget.Spinner;
import net.gogame.gopay.sdk.C1349g;
import net.gogame.gopay.sdk.C1379k;

final class as implements bq {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3460a;

    as(PurchaseActivity purchaseActivity) {
        this.f3460a = purchaseActivity;
    }

    /* renamed from: a */
    public final void mo4871a(C1349g c1349g) {
        boolean z = false;
        this.f3460a.f3394B.m3782a(this.f3460a.m3807a("paymentType"), c1349g.f3364c);
        this.f3460a.f3399G.setSelection(0);
        this.f3460a.f3402J = true;
        this.f3460a.f3409Q = 0;
        PurchaseActivity.m3815a(this.f3460a, this.f3460a.m3807a("paymentMethod"), ((C1379k) this.f3460a.f3394B.getItem(0)).f3567a);
        this.f3460a.f3398F.setEnabled(this.f3460a.f3393A.getCount() > 1);
        Spinner p = this.f3460a.f3399G;
        if (this.f3460a.f3394B.getCount() > 1) {
            z = true;
        }
        p.setEnabled(z);
        if (this.f3460a.f3435z != null) {
            this.f3460a.f3435z.setEnabled(true);
        }
    }
}
