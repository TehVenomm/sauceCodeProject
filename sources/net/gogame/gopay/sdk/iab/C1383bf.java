package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

/* renamed from: net.gogame.gopay.sdk.iab.bf */
final class C1383bf implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1083a;

    C1383bf(PurchaseActivity purchaseActivity) {
        this.f1083a = purchaseActivity;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        this.f1083a.f1058z.setEnabled(true);
    }
}
