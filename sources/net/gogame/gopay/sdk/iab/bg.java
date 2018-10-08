package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.view.View;
import android.view.View.OnClickListener;

final class bg implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ Dialog f1095a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1096b;

    bg(PurchaseActivity purchaseActivity, Dialog dialog) {
        this.f1096b = purchaseActivity;
        this.f1095a = dialog;
    }

    public final void onClick(View view) {
        this.f1095a.dismiss();
        this.f1096b.f1047z.setEnabled(true);
    }
}
