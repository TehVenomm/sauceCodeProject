package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.v */
final class C1373v implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1372u f3554a;

    C1373v(C1372u c1372u) {
        this.f3554a = c1372u;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3554a.f3553a.f3403K = false;
        this.f3554a.f3553a.f3404L = false;
        PurchaseActivity.m3830b(this.f3554a.f3553a, true);
    }
}
