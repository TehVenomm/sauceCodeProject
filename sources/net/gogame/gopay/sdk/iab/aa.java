package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class aa implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3436a;
    /* renamed from: b */
    final /* synthetic */ C1377z f3437b;

    aa(C1377z c1377z, int i) {
        this.f3437b = c1377z;
        this.f3436a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3437b.f3558a.f3557a.f3403K = false;
        this.f3437b.f3558a.f3557a.f3404L = false;
        PurchaseActivity.m3833c(this.f3437b.f3558a.f3557a, this.f3436a);
        this.f3437b.f3558a.f3557a.f3435z.setEnabled(true);
    }
}
