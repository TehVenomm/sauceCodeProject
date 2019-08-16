package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.v */
final class C1633v implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1403u f1285a;

    C1633v(C1403u uVar) {
        this.f1285a = uVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1285a.f1112a.f1020K = false;
        this.f1285a.f1112a.f1021L = false;
        PurchaseActivity.m813b(this.f1285a.f1112a, true);
    }
}
