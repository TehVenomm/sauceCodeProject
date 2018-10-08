package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.v */
final class C1057v implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1056u f1166a;

    C1057v(C1056u c1056u) {
        this.f1166a = c1056u;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1166a.f1165a.f1015K = false;
        this.f1166a.f1165a.f1016L = false;
        PurchaseActivity.m805b(this.f1166a.f1165a, true);
    }
}
