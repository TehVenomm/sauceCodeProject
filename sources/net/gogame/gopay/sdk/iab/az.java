package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

final class az implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ ay f3473a;

    az(ay ayVar) {
        this.f3473a = ayVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m3828b(this.f3473a.f3472c, this.f3473a.f3470a, this.f3473a.f3471b);
    }
}
