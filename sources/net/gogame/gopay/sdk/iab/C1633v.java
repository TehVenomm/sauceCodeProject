package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.v */
final class C1633v implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1403u f1297a;

    C1633v(C1403u uVar) {
        this.f1297a = uVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1297a.f1118a.f1026K = false;
        this.f1297a.f1118a.f1027L = false;
        PurchaseActivity.m813b(this.f1297a.f1118a, true);
    }
}
