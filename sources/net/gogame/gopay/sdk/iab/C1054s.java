package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1063k;

/* renamed from: net.gogame.gopay.sdk.iab.s */
final class C1054s implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1162a;
    /* renamed from: b */
    final /* synthetic */ C1053q f1163b;

    C1054s(C1053q c1053q, int i) {
        this.f1163b = c1053q;
        this.f1162a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1163b.f1161a.f1015K = false;
        this.f1163b.f1161a.f1016L = false;
        this.f1163b.f1161a.f1021Q = this.f1162a;
        this.f1163b.f1161a.f1013I = false;
        this.f1163b.f1161a.f1014J = true;
        PurchaseActivity.m790a(this.f1163b.f1161a, this.f1163b.f1161a.m782a("paymentMethod"), ((C1063k) this.f1163b.f1161a.f1006B.getItem(this.f1162a)).f1179a);
    }
}
