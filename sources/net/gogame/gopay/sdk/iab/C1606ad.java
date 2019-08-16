package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.ad */
final class C1606ad implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1227a;

    /* renamed from: b */
    final /* synthetic */ C1366ac f1228b;

    C1606ad(C1366ac acVar, int i) {
        this.f1228b = acVar;
        this.f1227a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1228b.f1056a.f1020K = false;
        this.f1228b.f1056a.f1021L = false;
        PurchaseActivity.m816c(this.f1228b.f1056a, this.f1228b.f1056a.f1013D.getItem(this.f1227a).intValue());
    }
}
