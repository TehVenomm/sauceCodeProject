package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.aa */
final class C1604aa implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1236a;

    /* renamed from: b */
    final /* synthetic */ C1635z f1237b;

    C1604aa(C1635z zVar, int i) {
        this.f1237b = zVar;
        this.f1236a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1237b.f1299a.f1120a.f1026K = false;
        this.f1237b.f1299a.f1120a.f1027L = false;
        PurchaseActivity.m816c(this.f1237b.f1299a.f1120a, this.f1236a);
        this.f1237b.f1299a.f1120a.f1058z.setEnabled(true);
    }
}
