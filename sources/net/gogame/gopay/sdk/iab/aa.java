package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class aa implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1048a;
    /* renamed from: b */
    final /* synthetic */ C1061z f1049b;

    aa(C1061z c1061z, int i) {
        this.f1049b = c1061z;
        this.f1048a = i;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1049b.f1170a.f1169a.f1015K = false;
        this.f1049b.f1170a.f1169a.f1016L = false;
        PurchaseActivity.m808c(this.f1049b.f1170a.f1169a, this.f1048a);
        this.f1049b.f1170a.f1169a.f1047z.setEnabled(true);
    }
}
