package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

final class ba implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ay f3474a;

    ba(ay ayVar) {
        this.f3474a = ayVar;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        PurchaseActivity.m3828b(this.f3474a.f3472c, this.f3474a.f3470a, this.f3474a.f3471b);
    }
}
