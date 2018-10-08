package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.iab.m */
final class C1049m implements OnItemSelectedListener {
    /* renamed from: a */
    boolean f1155a = true;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1156b;

    C1049m(PurchaseActivity purchaseActivity) {
        this.f1156b = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1155a) {
            this.f1155a = false;
        } else if (this.f1156b.f1020P != i || !this.f1156b.f1015K || !this.f1156b.f1016L) {
            if (this.f1156b.f1015K && !this.f1156b.f1014J && this.f1156b.f1016L) {
                new Builder(this.f1156b).setTitle(this.f1156b.f1036o).setMessage(this.f1156b.f1035n).setPositiveButton(this.f1156b.f1033l, new C1050n(this, i)).setNegativeButton(this.f1156b.f1034m, new C1051o(this)).setCancelable(false).show();
            } else {
                PurchaseActivity.m787a(this.f1156b, i);
            }
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
