package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import net.gogame.gopay.sdk.C1348f;

final class bb implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ C1348f f3475a;
    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f3476b;

    bb(PurchaseActivity purchaseActivity, C1348f c1348f) {
        this.f3476b = purchaseActivity;
        this.f3475a = c1348f;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m3828b(this.f3476b, this.f3475a.f3359a, this.f3475a.f3361c);
    }
}
