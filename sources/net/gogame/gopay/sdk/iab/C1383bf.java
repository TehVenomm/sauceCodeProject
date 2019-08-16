package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

/* renamed from: net.gogame.gopay.sdk.iab.bf */
final class C1383bf implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1077a;

    C1383bf(PurchaseActivity purchaseActivity) {
        this.f1077a = purchaseActivity;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        this.f1077a.f1052z.setEnabled(true);
    }
}
