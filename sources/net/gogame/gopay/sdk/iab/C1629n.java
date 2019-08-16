package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.n */
final class C1629n implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1279a;

    /* renamed from: b */
    final /* synthetic */ C1400m f1280b;

    C1629n(C1400m mVar, int i) {
        this.f1280b = mVar;
        this.f1279a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1280b.f1109b.f1020K = false;
        this.f1280b.f1109b.f1021L = false;
        PurchaseActivity.m795a(this.f1280b.f1109b, this.f1279a);
    }
}
