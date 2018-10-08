package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class ac implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1051a;

    ac(PurchaseActivity purchaseActivity) {
        this.f1051a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (i != 0) {
            if (!this.f1051a.f1015K || this.f1051a.f1025d || this.f1051a.f1014J || !this.f1051a.f1016L) {
                this.f1051a.f1014J = false;
                if (this.f1051a.f1025d) {
                    PurchaseActivity.m805b(this.f1051a, false);
                }
                PurchaseActivity.m808c(this.f1051a, this.f1051a.f1008D.m851b(i).intValue());
                return;
            }
            new Builder(this.f1051a).setTitle(this.f1051a.f1036o).setMessage(this.f1051a.f1035n).setPositiveButton(this.f1051a.f1033l, new ad(this, i)).setNegativeButton(this.f1051a.f1034m, new ae(this)).setCancelable(false).show();
        }
    }
}
