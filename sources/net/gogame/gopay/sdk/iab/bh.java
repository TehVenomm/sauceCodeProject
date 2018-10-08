package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

final class bh implements OnItemClickListener {
    /* renamed from: a */
    final /* synthetic */ OnClickListener f3485a;
    /* renamed from: b */
    final /* synthetic */ Dialog f3486b;
    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f3487c;

    bh(PurchaseActivity purchaseActivity, OnClickListener onClickListener, Dialog dialog) {
        this.f3487c = purchaseActivity;
        this.f3485a = onClickListener;
        this.f3486b = dialog;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        this.f3485a.onClick(null, i);
        this.f3487c.f3435z.setEnabled(true);
        this.f3486b.dismiss();
    }
}
