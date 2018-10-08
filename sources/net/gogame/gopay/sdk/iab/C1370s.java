package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1379k;

/* renamed from: net.gogame.gopay.sdk.iab.s */
final class C1370s implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3550a;
    /* renamed from: b */
    final /* synthetic */ C1369q f3551b;

    C1370s(C1369q c1369q, int i) {
        this.f3551b = c1369q;
        this.f3550a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3551b.f3549a.f3403K = false;
        this.f3551b.f3549a.f3404L = false;
        this.f3551b.f3549a.f3409Q = this.f3550a;
        this.f3551b.f3549a.f3401I = false;
        this.f3551b.f3549a.f3402J = true;
        PurchaseActivity.m3815a(this.f3551b.f3549a, this.f3551b.f3549a.m3807a("paymentMethod"), ((C1379k) this.f3551b.f3549a.f3394B.getItem(this.f3550a)).f3567a);
    }
}
