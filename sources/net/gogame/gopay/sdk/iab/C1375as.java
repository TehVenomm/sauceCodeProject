package net.gogame.gopay.sdk.iab;

import android.widget.Spinner;
import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.as */
final class C1375as implements C1392bq {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1065a;

    C1375as(PurchaseActivity purchaseActivity) {
        this.f1065a = purchaseActivity;
    }

    /* renamed from: a */
    public final void mo21526a(C1362g gVar) {
        boolean z = false;
        this.f1065a.f1011B.mo21499a(this.f1065a.m790a("paymentType"), gVar.f996c);
        this.f1065a.f1016G.setSelection(0);
        this.f1065a.f1019J = true;
        this.f1065a.f1026Q = 0;
        PurchaseActivity.m798a(this.f1065a, this.f1065a.m790a("paymentMethod"), ((C1636k) this.f1065a.f1011B.getItem(0)).f1288a);
        this.f1065a.f1015F.setEnabled(this.f1065a.f1010A.getCount() > 1);
        Spinner p = this.f1065a.f1016G;
        if (this.f1065a.f1011B.getCount() > 1) {
            z = true;
        }
        p.setEnabled(z);
        if (this.f1065a.f1052z != null) {
            this.f1065a.f1052z.setEnabled(true);
        }
    }
}
