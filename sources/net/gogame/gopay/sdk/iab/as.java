package net.gogame.gopay.sdk.iab;

import android.widget.Spinner;
import net.gogame.gopay.sdk.C1033g;
import net.gogame.gopay.sdk.C1063k;

final class as implements bq {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1072a;

    as(PurchaseActivity purchaseActivity) {
        this.f1072a = purchaseActivity;
    }

    /* renamed from: a */
    public final void mo4423a(C1033g c1033g) {
        boolean z = false;
        this.f1072a.f1006B.m757a(this.f1072a.m782a("paymentType"), c1033g.f976c);
        this.f1072a.f1011G.setSelection(0);
        this.f1072a.f1014J = true;
        this.f1072a.f1021Q = 0;
        PurchaseActivity.m790a(this.f1072a, this.f1072a.m782a("paymentMethod"), ((C1063k) this.f1072a.f1006B.getItem(0)).f1179a);
        this.f1072a.f1010F.setEnabled(this.f1072a.f1005A.getCount() > 1);
        Spinner p = this.f1072a.f1011G;
        if (this.f1072a.f1006B.getCount() > 1) {
            z = true;
        }
        p.setEnabled(z);
        if (this.f1072a.f1047z != null) {
            this.f1072a.f1047z.setEnabled(true);
        }
    }
}
