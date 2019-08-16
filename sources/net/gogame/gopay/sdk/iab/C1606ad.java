package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ad */
final class C1606ad implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1239a;

    /* renamed from: b */
    final /* synthetic */ C1366ac f1240b;

    C1606ad(C1366ac acVar, int i) {
        this.f1240b = acVar;
        this.f1239a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1240b.f1062a.f1026K = false;
        this.f1240b.f1062a.f1027L = false;
        PurchaseActivity.m816c(this.f1240b.f1062a, this.f1240b.f1062a.f1019D.getItem(this.f1239a).intValue());
    }
}
