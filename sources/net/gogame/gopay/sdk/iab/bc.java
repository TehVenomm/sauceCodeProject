package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import net.gogame.gopay.sdk.C1348f;

final class bc implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C1348f f3477a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f3478b;

    bc(PurchaseActivity purchaseActivity, C1348f c1348f) {
        this.f3478b = purchaseActivity;
        this.f3477a = c1348f;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        PurchaseActivity.m3828b(this.f3478b, this.f3477a.f3359a, this.f3477a.f3361c);
    }
}
