package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.u */
final class C1403u implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1118a;

    C1403u(PurchaseActivity purchaseActivity) {
        this.f1118a = purchaseActivity;
    }

    public final void onClick(View view) {
        if (!this.f1118a.f1036d) {
            if (!this.f1118a.f1026K || this.f1118a.f1025J || !this.f1118a.f1027L) {
                PurchaseActivity.m813b(this.f1118a, true);
            } else {
                new Builder(this.f1118a).setTitle(this.f1118a.f1047o).setMessage(this.f1118a.f1046n).setPositiveButton(this.f1118a.f1044l, new C1633v(this)).setNegativeButton(this.f1118a.f1045m, new C1634w(this)).setCancelable(false).show();
            }
        }
    }
}
