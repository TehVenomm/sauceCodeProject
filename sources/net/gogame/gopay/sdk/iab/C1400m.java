package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.iab.m */
final class C1400m implements OnItemSelectedListener {

    /* renamed from: a */
    boolean f1108a = true;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1109b;

    C1400m(PurchaseActivity purchaseActivity) {
        this.f1109b = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1108a) {
            this.f1108a = false;
        } else if (this.f1109b.f1025P == i && this.f1109b.f1020K && this.f1109b.f1021L) {
        } else {
            if (!this.f1109b.f1020K || this.f1109b.f1019J || !this.f1109b.f1021L) {
                PurchaseActivity.m795a(this.f1109b, i);
            } else {
                new Builder(this.f1109b).setTitle(this.f1109b.f1041o).setMessage(this.f1109b.f1040n).setPositiveButton(this.f1109b.f1038l, new C1629n(this, i)).setNegativeButton(this.f1109b.f1039m, new C1630o(this)).setCancelable(false).show();
            }
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
