package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.z */
final class C1061z implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1060y f1170a;

    C1061z(C1060y c1060y) {
        this.f1170a = c1060y;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        if (this.f1170a.f1169a.f1015K && !this.f1170a.f1169a.f1014J && this.f1170a.f1169a.f1016L) {
            new Builder(this.f1170a.f1169a).setTitle(this.f1170a.f1169a.f1036o).setMessage(this.f1170a.f1169a.f1035n).setPositiveButton(this.f1170a.f1169a.f1033l, new aa(this, i)).setNegativeButton(this.f1170a.f1169a.f1034m, new ab(this)).setCancelable(false).show();
            return;
        }
        this.f1170a.f1169a.f1014J = false;
        PurchaseActivity.m808c(this.f1170a.f1169a, i);
        this.f1170a.f1169a.f1047z.setEnabled(true);
    }
}
