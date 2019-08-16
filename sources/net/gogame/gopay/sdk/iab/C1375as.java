package net.gogame.gopay.sdk.iab;

import android.widget.Spinner;
import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.as */
final class C1375as implements C1392bq {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1071a;

    C1375as(PurchaseActivity purchaseActivity) {
        this.f1071a = purchaseActivity;
    }

    /* renamed from: a */
    public final void mo21526a(C1362g gVar) {
        boolean z = false;
        this.f1071a.f1017B.mo21499a(this.f1071a.m790a("paymentType"), gVar.f1002c);
        this.f1071a.f1022G.setSelection(0);
        this.f1071a.f1025J = true;
        this.f1071a.f1032Q = 0;
        PurchaseActivity.m798a(this.f1071a, this.f1071a.m790a("paymentMethod"), ((C1636k) this.f1071a.f1017B.getItem(0)).f1300a);
        this.f1071a.f1021F.setEnabled(this.f1071a.f1016A.getCount() > 1);
        Spinner p = this.f1071a.f1022G;
        if (this.f1071a.f1017B.getCount() > 1) {
            z = true;
        }
        p.setEnabled(z);
        if (this.f1071a.f1058z != null) {
            this.f1071a.f1058z.setEnabled(true);
        }
    }
}
