package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.bh */
final class C1385bh implements OnItemClickListener {

    /* renamed from: a */
    final /* synthetic */ OnClickListener f1080a;

    /* renamed from: b */
    final /* synthetic */ Dialog f1081b;

    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f1082c;

    C1385bh(PurchaseActivity purchaseActivity, OnClickListener onClickListener, Dialog dialog) {
        this.f1082c = purchaseActivity;
        this.f1080a = onClickListener;
        this.f1081b = dialog;
    }

    public final void onItemClick(AdapterView adapterView, View view, int i, long j) {
        this.f1080a.onClick(null, i);
        this.f1082c.f1052z.setEnabled(true);
        this.f1081b.dismiss();
    }
}
