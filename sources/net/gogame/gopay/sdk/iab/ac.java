package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class ac implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3439a;

    ac(PurchaseActivity purchaseActivity) {
        this.f3439a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (i != 0) {
            if (!this.f3439a.f3403K || this.f3439a.f3413d || this.f3439a.f3402J || !this.f3439a.f3404L) {
                this.f3439a.f3402J = false;
                if (this.f3439a.f3413d) {
                    PurchaseActivity.m3830b(this.f3439a, false);
                }
                PurchaseActivity.m3833c(this.f3439a, this.f3439a.f3396D.m3876b(i).intValue());
                return;
            }
            new Builder(this.f3439a).setTitle(this.f3439a.f3424o).setMessage(this.f3439a.f3423n).setPositiveButton(this.f3439a.f3421l, new ad(this, i)).setNegativeButton(this.f3439a.f3422m, new ae(this)).setCancelable(false).show();
        }
    }
}
