package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

final class bf implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3482a;

    bf(PurchaseActivity purchaseActivity) {
        this.f3482a = purchaseActivity;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        this.f3482a.f3435z.setEnabled(true);
    }
}
