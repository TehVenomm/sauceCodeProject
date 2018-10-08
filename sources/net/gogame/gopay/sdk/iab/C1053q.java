package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import net.gogame.gopay.sdk.C1063k;

/* renamed from: net.gogame.gopay.sdk.iab.q */
final class C1053q implements OnItemSelectedListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1161a;

    C1053q(PurchaseActivity purchaseActivity) {
        this.f1161a = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1161a.f1021Q != i || !this.f1161a.f1015K || !this.f1161a.f1016L) {
            if (this.f1161a.f1015K && !this.f1161a.f1014J && this.f1161a.f1016L) {
                new Builder(this.f1161a).setTitle(this.f1161a.f1036o).setMessage(this.f1161a.f1035n).setPositiveButton(this.f1161a.f1033l, new C1054s(this, i)).setNegativeButton(this.f1161a.f1034m, new C1055t(this)).setCancelable(false).show();
                return;
            }
            this.f1161a.f1021Q = i;
            this.f1161a.f1013I = false;
            this.f1161a.f1014J = true;
            PurchaseActivity.m790a(this.f1161a, this.f1161a.m782a("paymentMethod"), ((C1063k) this.f1161a.f1006B.getItem(i)).f1179a);
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
