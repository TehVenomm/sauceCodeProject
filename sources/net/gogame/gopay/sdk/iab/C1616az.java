package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

/* renamed from: net.gogame.gopay.sdk.iab.az */
final class C1616az implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ C1378ay f1245a;

    C1616az(C1378ay ayVar) {
        this.f1245a = ayVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m811b(this.f1245a.f1071c, this.f1245a.f1069a, this.f1245a.f1070b);
    }
}
