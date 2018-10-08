package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemLongClickListener;

final class aj implements OnItemLongClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1061a;

    aj(PurchaseActivity purchaseActivity) {
        this.f1061a = purchaseActivity;
    }

    public final boolean onItemLongClick(AdapterView adapterView, View view, int i, long j) {
        if (this.f1061a.f1015K && !this.f1061a.f1014J && this.f1061a.f1016L) {
            new Builder(this.f1061a).setTitle(this.f1061a.f1036o).setMessage(this.f1061a.f1035n).setPositiveButton(this.f1061a.f1033l, new ak(this, i, view)).setNegativeButton(this.f1061a.f1034m, new al(this)).setCancelable(false).show();
        } else {
            this.f1061a.f1014J = false;
            this.f1061a.f1007C.f1144e = i;
            if (this.f1061a.f1007C.f1145f != null) {
                this.f1061a.f1007C.f1145f.setBackgroundColor(Color.rgb(241, 241, 241));
            }
            this.f1061a.f1007C.f1145f = view;
            view.setBackgroundColor(-1);
            this.f1061a.m795a((C1025a) this.f1061a.f1007C.getItem(i));
        }
        return true;
    }
}
