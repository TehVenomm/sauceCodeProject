package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.n */
final class C1050n implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1157a;
    /* renamed from: b */
    final /* synthetic */ C1049m f1158b;

    C1050n(C1049m c1049m, int i) {
        this.f1158b = c1049m;
        this.f1157a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1158b.f1156b.f1015K = false;
        this.f1158b.f1156b.f1016L = false;
        PurchaseActivity.m787a(this.f1158b.f1156b, this.f1157a);
    }
}
