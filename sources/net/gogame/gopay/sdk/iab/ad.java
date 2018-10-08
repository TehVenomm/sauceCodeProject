package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class ad implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1052a;
    /* renamed from: b */
    final /* synthetic */ ac f1053b;

    ad(ac acVar, int i) {
        this.f1053b = acVar;
        this.f1052a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1053b.f1051a.f1015K = false;
        this.f1053b.f1051a.f1016L = false;
        PurchaseActivity.m808c(this.f1053b.f1051a, this.f1053b.f1051a.f1008D.m851b(this.f1052a).intValue());
    }
}
