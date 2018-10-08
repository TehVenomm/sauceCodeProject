package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class ad implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3440a;
    /* renamed from: b */
    final /* synthetic */ ac f3441b;

    ad(ac acVar, int i) {
        this.f3441b = acVar;
        this.f3440a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3441b.f3439a.f3403K = false;
        this.f3441b.f3439a.f3404L = false;
        PurchaseActivity.m3833c(this.f3441b.f3439a, this.f3441b.f3439a.f3396D.m3876b(this.f3440a).intValue());
    }
}
