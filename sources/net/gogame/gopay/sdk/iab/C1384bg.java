package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.bg */
final class C1384bg implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ Dialog f1084a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1085b;

    C1384bg(PurchaseActivity purchaseActivity, Dialog dialog) {
        this.f1085b = purchaseActivity;
        this.f1084a = dialog;
    }

    public final void onClick(View view) {
        this.f1084a.dismiss();
        this.f1085b.f1058z.setEnabled(true);
    }
}
