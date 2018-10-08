package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class ba implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ay f1086a;

    ba(ay ayVar) {
        this.f1086a = ayVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        PurchaseActivity.m803b(this.f1086a.f1084c, this.f1086a.f1082a, this.f1086a.f1083b);
    }
}
