package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.u */
final class C1056u implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1165a;

    C1056u(PurchaseActivity purchaseActivity) {
        this.f1165a = purchaseActivity;
    }

    public final void onClick(View view) {
        if (!this.f1165a.f1025d) {
            if (this.f1165a.f1015K && !this.f1165a.f1014J && this.f1165a.f1016L) {
                new Builder(this.f1165a).setTitle(this.f1165a.f1036o).setMessage(this.f1165a.f1035n).setPositiveButton(this.f1165a.f1033l, new C1057v(this)).setNegativeButton(this.f1165a.f1034m, new C1058w(this)).setCancelable(false).show();
            } else {
                PurchaseActivity.m805b(this.f1165a, true);
            }
        }
    }
}
