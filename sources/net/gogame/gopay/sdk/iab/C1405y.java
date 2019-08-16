package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.y */
final class C1405y implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1114a;

    C1405y(PurchaseActivity purchaseActivity) {
        this.f1114a = purchaseActivity;
    }

    public final void onClick(View view) {
        this.f1114a.f1052z.setEnabled(false);
        PurchaseActivity.m787a(this.f1114a, "Payment Channels", this.f1114a.f1012C, new C1635z(this)).show();
    }
}
