package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.n */
final class C1629n implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1291a;

    /* renamed from: b */
    final /* synthetic */ C1400m f1292b;

    C1629n(C1400m mVar, int i) {
        this.f1292b = mVar;
        this.f1291a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1292b.f1115b.f1026K = false;
        this.f1292b.f1115b.f1027L = false;
        PurchaseActivity.m795a(this.f1292b.f1115b, this.f1291a);
    }
}
