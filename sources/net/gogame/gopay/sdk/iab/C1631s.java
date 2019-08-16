package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.s */
final class C1631s implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1282a;

    /* renamed from: b */
    final /* synthetic */ C1402q f1283b;

    C1631s(C1402q qVar, int i) {
        this.f1283b = qVar;
        this.f1282a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1283b.f1111a.f1020K = false;
        this.f1283b.f1111a.f1021L = false;
        this.f1283b.f1111a.f1026Q = this.f1282a;
        this.f1283b.f1111a.f1018I = false;
        this.f1283b.f1111a.f1019J = true;
        PurchaseActivity.m798a(this.f1283b.f1111a, this.f1283b.f1111a.m790a("paymentMethod"), ((C1636k) this.f1283b.f1111a.f1011B.getItem(this.f1282a)).f1288a);
    }
}
