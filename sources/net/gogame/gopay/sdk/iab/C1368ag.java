package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.graphics.Color;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ag */
final class C1368ag implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1058a;

    C1368ag(PurchaseActivity purchaseActivity) {
        this.f1058a = purchaseActivity;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        if (!this.f1058a.f1020K || this.f1058a.f1019J || !this.f1058a.f1021L) {
            this.f1058a.f1019J = false;
            this.f1058a.f1012C.f1105e = i;
            if (this.f1058a.f1012C.f1106f != null) {
                this.f1058a.f1012C.f1106f.setBackgroundColor(Color.rgb(241, 241, 241));
            }
            this.f1058a.f1012C.f1106f = view;
            view.setBackgroundColor(-1);
            this.f1058a.m803a((C1365a) this.f1058a.f1012C.getItem(i));
            return;
        }
        new Builder(this.f1058a).setTitle(this.f1058a.f1041o).setMessage(this.f1058a.f1040n).setPositiveButton(this.f1058a.f1038l, new C1608ah(this, i, view)).setNegativeButton(this.f1058a.f1039m, new C1609ai(this)).setCancelable(false).show();
    }
}
