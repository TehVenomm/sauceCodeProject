package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ag */
final class C1368ag implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1064a;

    C1368ag(PurchaseActivity purchaseActivity) {
        this.f1064a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (!this.f1064a.f1026K || this.f1064a.f1025J || !this.f1064a.f1027L) {
            this.f1064a.f1025J = false;
            this.f1064a.f1018C.f1111e = i;
            if (this.f1064a.f1018C.f1112f != null) {
                this.f1064a.f1018C.f1112f.setBackgroundColor(Color.rgb(241, 241, 241));
            }
            this.f1064a.f1018C.f1112f = view;
            view.setBackgroundColor(-1);
            this.f1064a.m803a((C1365a) this.f1064a.f1018C.getItem(i));
            return;
        }
        new Builder(this.f1064a).setTitle(this.f1064a.f1047o).setMessage(this.f1064a.f1046n).setPositiveButton(this.f1064a.f1044l, new C1608ah(this, i, view)).setNegativeButton(this.f1064a.f1045m, new C1609ai(this)).setCancelable(false).show();
    }
}
