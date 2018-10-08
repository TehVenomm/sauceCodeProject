package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.iab.m */
final class C1365m implements OnItemSelectedListener {
    /* renamed from: a */
    boolean f3543a = true;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f3544b;

    C1365m(PurchaseActivity purchaseActivity) {
        this.f3544b = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f3543a) {
            this.f3543a = false;
        } else if (this.f3544b.f3408P != i || !this.f3544b.f3403K || !this.f3544b.f3404L) {
            if (this.f3544b.f3403K && !this.f3544b.f3402J && this.f3544b.f3404L) {
                new Builder(this.f3544b).setTitle(this.f3544b.f3424o).setMessage(this.f3544b.f3423n).setPositiveButton(this.f3544b.f3421l, new C1366n(this, i)).setNegativeButton(this.f3544b.f3422m, new C1367o(this)).setCancelable(false).show();
            } else {
                PurchaseActivity.m3812a(this.f3544b, i);
            }
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
