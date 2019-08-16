package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.aa */
final class C1604aa implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1224a;

    /* renamed from: b */
    final /* synthetic */ C1635z f1225b;

    C1604aa(C1635z zVar, int i) {
        this.f1225b = zVar;
        this.f1224a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1225b.f1287a.f1114a.f1020K = false;
        this.f1225b.f1287a.f1114a.f1021L = false;
        PurchaseActivity.m816c(this.f1225b.f1287a.f1114a, this.f1224a);
        this.f1225b.f1287a.f1114a.f1052z.setEnabled(true);
    }
}
