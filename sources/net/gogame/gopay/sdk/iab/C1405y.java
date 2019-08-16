package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.y */
final class C1405y implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1120a;

    C1405y(PurchaseActivity purchaseActivity) {
        this.f1120a = purchaseActivity;
    }

    public final void onClick(View view) {
        this.f1120a.f1058z.setEnabled(false);
        PurchaseActivity.m787a(this.f1120a, "Payment Channels", this.f1120a.f1018C, new C1635z(this)).show();
    }
}
