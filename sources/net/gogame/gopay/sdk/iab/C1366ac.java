package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ac */
final class C1366ac implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1056a;

    C1366ac(PurchaseActivity purchaseActivity) {
        this.f1056a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (i != 0) {
            if (!this.f1056a.f1020K || this.f1056a.f1030d || this.f1056a.f1019J || !this.f1056a.f1021L) {
                this.f1056a.f1019J = false;
                if (this.f1056a.f1030d) {
                    PurchaseActivity.m813b(this.f1056a, false);
                }
                PurchaseActivity.m816c(this.f1056a, this.f1056a.f1013D.getItem(i).intValue());
                return;
            }
            new Builder(this.f1056a).setTitle(this.f1056a.f1041o).setMessage(this.f1056a.f1040n).setPositiveButton(this.f1056a.f1038l, new C1606ad(this, i)).setNegativeButton(this.f1056a.f1039m, new C1607ae(this)).setCancelable(false).show();
        }
    }
}
