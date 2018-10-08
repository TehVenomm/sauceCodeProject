package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class ag implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3444a;

    ag(PurchaseActivity purchaseActivity) {
        this.f3444a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (this.f3444a.f3403K && !this.f3444a.f3402J && this.f3444a.f3404L) {
            new Builder(this.f3444a).setTitle(this.f3444a.f3424o).setMessage(this.f3444a.f3423n).setPositiveButton(this.f3444a.f3421l, new ah(this, i, view)).setNegativeButton(this.f3444a.f3422m, new ai(this)).setCancelable(false).show();
            return;
        }
        this.f3444a.f3402J = false;
        this.f3444a.f3395C.f3532e = i;
        if (this.f3444a.f3395C.f3533f != null) {
            this.f3444a.f3395C.f3533f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f3444a.f3395C.f3533f = view;
        view.setBackgroundColor(-1);
        this.f3444a.m3820a((C1341a) this.f3444a.f3395C.getItem(i));
    }
}
