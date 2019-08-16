package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.s */
final class C1631s implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1294a;

    /* renamed from: b */
    final /* synthetic */ C1402q f1295b;

    C1631s(C1402q qVar, int i) {
        this.f1295b = qVar;
        this.f1294a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1295b.f1117a.f1026K = false;
        this.f1295b.f1117a.f1027L = false;
        this.f1295b.f1117a.f1032Q = this.f1294a;
        this.f1295b.f1117a.f1024I = false;
        this.f1295b.f1117a.f1025J = true;
        PurchaseActivity.m798a(this.f1295b.f1117a, this.f1295b.f1117a.m790a("paymentMethod"), ((C1636k) this.f1295b.f1117a.f1017B.getItem(this.f1294a)).f1300a);
    }
}
