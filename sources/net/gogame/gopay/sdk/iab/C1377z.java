package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.z */
final class C1377z implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1376y f3558a;

    C1377z(C1376y c1376y) {
        this.f3558a = c1376y;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        if (this.f3558a.f3557a.f3403K && !this.f3558a.f3557a.f3402J && this.f3558a.f3557a.f3404L) {
            new Builder(this.f3558a.f3557a).setTitle(this.f3558a.f3557a.f3424o).setMessage(this.f3558a.f3557a.f3423n).setPositiveButton(this.f3558a.f3557a.f3421l, new aa(this, i)).setNegativeButton(this.f3558a.f3557a.f3422m, new ab(this)).setCancelable(false).show();
            return;
        }
        this.f3558a.f3557a.f3402J = false;
        PurchaseActivity.m3833c(this.f3558a.f3557a, i);
        this.f3558a.f3557a.f3435z.setEnabled(true);
    }
}
