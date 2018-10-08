package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import net.gogame.gopay.sdk.C1032f;

final class bb implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ C1032f f1087a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1088b;

    bb(PurchaseActivity purchaseActivity, C1032f c1032f) {
        this.f1088b = purchaseActivity;
        this.f1087a = c1032f;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m803b(this.f1088b, this.f1087a.f971a, this.f1087a.f973c);
    }
}
