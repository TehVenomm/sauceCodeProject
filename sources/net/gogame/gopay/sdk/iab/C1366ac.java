package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ac */
final class C1366ac implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1062a;

    C1366ac(PurchaseActivity purchaseActivity) {
        this.f1062a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (i != 0) {
            if (!this.f1062a.f1026K || this.f1062a.f1036d || this.f1062a.f1025J || !this.f1062a.f1027L) {
                this.f1062a.f1025J = false;
                if (this.f1062a.f1036d) {
                    PurchaseActivity.m813b(this.f1062a, false);
                }
                PurchaseActivity.m816c(this.f1062a, this.f1062a.f1019D.getItem(i).intValue());
                return;
            }
            new Builder(this.f1062a).setTitle(this.f1062a.f1047o).setMessage(this.f1062a.f1046n).setPositiveButton(this.f1062a.f1044l, new C1606ad(this, i)).setNegativeButton(this.f1062a.f1045m, new C1607ae(this)).setCancelable(false).show();
        }
    }
}
