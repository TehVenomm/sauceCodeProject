package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.view.View;
import android.view.View.OnClickListener;

final class bg implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ Dialog f3483a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f3484b;

    bg(PurchaseActivity purchaseActivity, Dialog dialog) {
        this.f3484b = purchaseActivity;
        this.f3483a = dialog;
    }

    public final void onClick(View view) {
        this.f3483a.dismiss();
        this.f3484b.f3435z.setEnabled(true);
    }
}
