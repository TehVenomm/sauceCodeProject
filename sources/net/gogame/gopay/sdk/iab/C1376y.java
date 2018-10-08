package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.y */
final class C1376y implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3557a;

    C1376y(PurchaseActivity purchaseActivity) {
        this.f3557a = purchaseActivity;
    }

    public final void onClick(View view) {
        this.f3557a.f3435z.setEnabled(false);
        PurchaseActivity.m3804a(this.f3557a, "Payment Channels", this.f3557a.f3395C, new C1377z(this)).show();
    }
}
