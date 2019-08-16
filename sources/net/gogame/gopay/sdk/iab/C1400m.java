package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.iab.m */
final class C1400m implements OnItemSelectedListener {

    /* renamed from: a */
    boolean f1114a = true;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1115b;

    C1400m(PurchaseActivity purchaseActivity) {
        this.f1115b = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f1114a) {
            this.f1114a = false;
        } else if (this.f1115b.f1031P == i && this.f1115b.f1026K && this.f1115b.f1027L) {
        } else {
            if (!this.f1115b.f1026K || this.f1115b.f1025J || !this.f1115b.f1027L) {
                PurchaseActivity.m795a(this.f1115b, i);
            } else {
                new Builder(this.f1115b).setTitle(this.f1115b.f1047o).setMessage(this.f1115b.f1046n).setPositiveButton(this.f1115b.f1044l, new C1629n(this, i)).setNegativeButton(this.f1115b.f1045m, new C1630o(this)).setCancelable(false).show();
            }
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
