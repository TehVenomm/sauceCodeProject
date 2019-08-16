package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import net.gogame.gopay.sdk.C1361f;

/* renamed from: net.gogame.gopay.sdk.iab.bb */
final class C1380bb implements OnCancelListener {

    /* renamed from: a */
    final /* synthetic */ C1361f f1078a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1079b;

    C1380bb(PurchaseActivity purchaseActivity, C1361f fVar) {
        this.f1079b = purchaseActivity;
        this.f1078a = fVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m811b(this.f1079b, this.f1078a.f997a, this.f1078a.f999c);
    }
}
