package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

/* renamed from: net.gogame.gopay.sdk.iab.az */
final class C1616az implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ C1378ay f1257a;

    C1616az(C1378ay ayVar) {
        this.f1257a = ayVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m811b(this.f1257a.f1077c, this.f1257a.f1075a, this.f1257a.f1076b);
    }
}
