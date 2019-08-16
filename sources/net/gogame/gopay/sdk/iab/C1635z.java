package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.z */
final class C1635z implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1405y f1287a;

    C1635z(C1405y yVar) {
        this.f1287a = yVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        if (!this.f1287a.f1114a.f1020K || this.f1287a.f1114a.f1019J || !this.f1287a.f1114a.f1021L) {
            this.f1287a.f1114a.f1019J = false;
            PurchaseActivity.m816c(this.f1287a.f1114a, i);
            this.f1287a.f1114a.f1052z.setEnabled(true);
            return;
        }
        new Builder(this.f1287a.f1114a).setTitle(this.f1287a.f1114a.f1041o).setMessage(this.f1287a.f1114a.f1040n).setPositiveButton(this.f1287a.f1114a.f1038l, new C1604aa(this, i)).setNegativeButton(this.f1287a.f1114a.f1039m, new C1605ab(this)).setCancelable(false).show();
    }
}
