package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1032f;

final class bc implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1032f f1089a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1090b;

    bc(PurchaseActivity purchaseActivity, C1032f c1032f) {
        this.f1090b = purchaseActivity;
        this.f1089a = c1032f;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        PurchaseActivity.m803b(this.f1090b, this.f1089a.f971a, this.f1089a.f973c);
    }
}
