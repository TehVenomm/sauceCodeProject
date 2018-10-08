package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.u */
final class C1372u implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3553a;

    C1372u(PurchaseActivity purchaseActivity) {
        this.f3553a = purchaseActivity;
    }

    public final void onClick(View view) {
        if (!this.f3553a.f3413d) {
            if (this.f3553a.f3403K && !this.f3553a.f3402J && this.f3553a.f3404L) {
                new Builder(this.f3553a).setTitle(this.f3553a.f3424o).setMessage(this.f3553a.f3423n).setPositiveButton(this.f3553a.f3421l, new C1373v(this)).setNegativeButton(this.f3553a.f3422m, new C1374w(this)).setCancelable(false).show();
            } else {
                PurchaseActivity.m3830b(this.f3553a, true);
            }
        }
    }
}
