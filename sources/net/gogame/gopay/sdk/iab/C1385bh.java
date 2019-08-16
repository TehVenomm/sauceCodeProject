package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.bh */
final class C1385bh implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ OnClickListener f1086a;

    /* renamed from: b */
    final /* synthetic */ Dialog f1087b;

    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f1088c;

    C1385bh(PurchaseActivity purchaseActivity, OnClickListener onClickListener, Dialog dialog) {
        this.f1088c = purchaseActivity;
        this.f1086a = onClickListener;
        this.f1087b = dialog;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        this.f1086a.onClick(null, i);
        this.f1088c.f1058z.setEnabled(true);
        this.f1087b.dismiss();
    }
}
