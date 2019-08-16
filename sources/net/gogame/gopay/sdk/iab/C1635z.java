package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.z */
final class C1635z implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1405y f1299a;

    C1635z(C1405y yVar) {
        this.f1299a = yVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        if (!this.f1299a.f1120a.f1026K || this.f1299a.f1120a.f1025J || !this.f1299a.f1120a.f1027L) {
            this.f1299a.f1120a.f1025J = false;
            PurchaseActivity.m816c(this.f1299a.f1120a, i);
            this.f1299a.f1120a.f1058z.setEnabled(true);
            return;
        }
        new Builder(this.f1299a.f1120a).setTitle(this.f1299a.f1120a.f1047o).setMessage(this.f1299a.f1120a.f1046n).setPositiveButton(this.f1299a.f1120a.f1044l, new C1604aa(this, i)).setNegativeButton(this.f1299a.f1120a.f1045m, new C1605ab(this)).setCancelable(false).show();
    }
}
