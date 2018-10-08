package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

final class bf implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1094a;

    bf(PurchaseActivity purchaseActivity) {
        this.f1094a = purchaseActivity;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        this.f1094a.f1047z.setEnabled(true);
    }
}
