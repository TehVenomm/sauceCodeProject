package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import net.gogame.gopay.sdk.C1361f;

/* renamed from: net.gogame.gopay.sdk.iab.bb */
final class C1380bb implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ C1361f f1072a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1073b;

    C1380bb(PurchaseActivity purchaseActivity, C1361f fVar) {
        this.f1073b = purchaseActivity;
        this.f1072a = fVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m811b(this.f1073b, this.f1072a.f991a, this.f1072a.f993c);
    }
}
