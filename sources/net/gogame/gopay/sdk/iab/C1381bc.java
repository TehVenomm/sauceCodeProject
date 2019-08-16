package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1361f;

/* renamed from: net.gogame.gopay.sdk.iab.bc */
final class C1381bc implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1361f f1074a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1075b;

    C1381bc(PurchaseActivity purchaseActivity, C1361f fVar) {
        this.f1075b = purchaseActivity;
        this.f1074a = fVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        PurchaseActivity.m811b(this.f1075b, this.f1074a.f991a, this.f1074a.f993c);
    }
}
