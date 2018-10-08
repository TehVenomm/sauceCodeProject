package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class ag implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1056a;

    ag(PurchaseActivity purchaseActivity) {
        this.f1056a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (this.f1056a.f1015K && !this.f1056a.f1014J && this.f1056a.f1016L) {
            new Builder(this.f1056a).setTitle(this.f1056a.f1036o).setMessage(this.f1056a.f1035n).setPositiveButton(this.f1056a.f1033l, new ah(this, i, view)).setNegativeButton(this.f1056a.f1034m, new ai(this)).setCancelable(false).show();
            return;
        }
        this.f1056a.f1014J = false;
        this.f1056a.f1007C.f1144e = i;
        if (this.f1056a.f1007C.f1145f != null) {
            this.f1056a.f1007C.f1145f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1056a.f1007C.f1145f = view;
        view.setBackgroundColor(-1);
        this.f1056a.m795a((C1025a) this.f1056a.f1007C.getItem(i));
    }
}
