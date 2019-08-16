package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.q */
final class C1402q implements OnItemSelectedListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1111a;

    C1402q(PurchaseActivity purchaseActivity) {
        this.f1111a = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1111a.f1026Q == i && this.f1111a.f1020K && this.f1111a.f1021L) {
            return;
        }
        if (!this.f1111a.f1020K || this.f1111a.f1019J || !this.f1111a.f1021L) {
            this.f1111a.f1026Q = i;
            this.f1111a.f1018I = false;
            this.f1111a.f1019J = true;
            PurchaseActivity.m798a(this.f1111a, this.f1111a.m790a("paymentMethod"), ((C1636k) this.f1111a.f1011B.getItem(i)).f1288a);
            return;
        }
        new Builder(this.f1111a).setTitle(this.f1111a.f1041o).setMessage(this.f1111a.f1040n).setPositiveButton(this.f1111a.f1038l, new C1631s(this, i)).setNegativeButton(this.f1111a.f1039m, new C1632t(this)).setCancelable(false).show();
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
