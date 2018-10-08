package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class bh implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ OnClickListener f1097a;
    /* renamed from: b */
    final /* synthetic */ Dialog f1098b;
    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f1099c;

    bh(PurchaseActivity purchaseActivity, OnClickListener onClickListener, Dialog dialog) {
        this.f1099c = purchaseActivity;
        this.f1097a = onClickListener;
        this.f1098b = dialog;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        this.f1097a.onClick(null, i);
        this.f1099c.f1047z.setEnabled(true);
        this.f1098b.dismiss();
    }
}
