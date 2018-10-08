package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.y */
final class C1060y implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1169a;

    C1060y(PurchaseActivity purchaseActivity) {
        this.f1169a = purchaseActivity;
    }

    public final void onClick(View view) {
        this.f1169a.f1047z.setEnabled(false);
        PurchaseActivity.m779a(this.f1169a, "Payment Channels", this.f1169a.f1007C, new C1061z(this)).show();
    }
}
