package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemLongClickListener;

final class aj implements OnItemLongClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3449a;

    aj(PurchaseActivity purchaseActivity) {
        this.f3449a = purchaseActivity;
    }

    public final boolean onItemLongClick(AdapterView adapterView, View view, int i, long j) {
        if (this.f3449a.f3403K && !this.f3449a.f3402J && this.f3449a.f3404L) {
            new Builder(this.f3449a).setTitle(this.f3449a.f3424o).setMessage(this.f3449a.f3423n).setPositiveButton(this.f3449a.f3421l, new ak(this, i, view)).setNegativeButton(this.f3449a.f3422m, new al(this)).setCancelable(false).show();
        } else {
            this.f3449a.f3402J = false;
            this.f3449a.f3395C.f3532e = i;
            if (this.f3449a.f3395C.f3533f != null) {
                this.f3449a.f3395C.f3533f.setBackgroundColor(Color.rgb(241, 241, 241));
            }
            this.f3449a.f3395C.f3533f = view;
            view.setBackgroundColor(-1);
            this.f3449a.m3820a((C1341a) this.f3449a.f3395C.getItem(i));
        }
        return true;
    }
}
