package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.n */
final class C1366n implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3545a;
    /* renamed from: b */
    final /* synthetic */ C1365m f3546b;

    C1366n(C1365m c1365m, int i) {
        this.f3546b = c1365m;
        this.f3545a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3546b.f3544b.f3403K = false;
        this.f3546b.f3544b.f3404L = false;
        PurchaseActivity.m3812a(this.f3546b.f3544b, this.f3545a);
    }
}
