package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.u */
final class C1403u implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1112a;

    C1403u(PurchaseActivity purchaseActivity) {
        this.f1112a = purchaseActivity;
    }

    public final void onClick(View view) {
        if (!this.f1112a.f1030d) {
            if (!this.f1112a.f1020K || this.f1112a.f1019J || !this.f1112a.f1021L) {
                PurchaseActivity.m813b(this.f1112a, true);
            } else {
                new Builder(this.f1112a).setTitle(this.f1112a.f1041o).setMessage(this.f1112a.f1040n).setPositiveButton(this.f1112a.f1038l, new C1633v(this)).setNegativeButton(this.f1112a.f1039m, new C1634w(this)).setCancelable(false).show();
            }
        }
    }
}
