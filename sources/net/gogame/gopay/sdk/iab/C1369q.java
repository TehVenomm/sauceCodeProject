package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import net.gogame.gopay.sdk.C1379k;

/* renamed from: net.gogame.gopay.sdk.iab.q */
final class C1369q implements OnItemSelectedListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3549a;

    C1369q(PurchaseActivity purchaseActivity) {
        this.f3549a = purchaseActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        if (this.f3549a.f3409Q != i || !this.f3549a.f3403K || !this.f3549a.f3404L) {
            if (this.f3549a.f3403K && !this.f3549a.f3402J && this.f3549a.f3404L) {
                new Builder(this.f3549a).setTitle(this.f3549a.f3424o).setMessage(this.f3549a.f3423n).setPositiveButton(this.f3549a.f3421l, new C1370s(this, i)).setNegativeButton(this.f3549a.f3422m, new C1371t(this)).setCancelable(false).show();
                return;
            }
            this.f3549a.f3409Q = i;
            this.f3549a.f3401I = false;
            this.f3549a.f3402J = true;
            PurchaseActivity.m3815a(this.f3549a, this.f3549a.m3807a("paymentMethod"), ((C1379k) this.f3549a.f3394B.getItem(i)).f3567a);
        }
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
