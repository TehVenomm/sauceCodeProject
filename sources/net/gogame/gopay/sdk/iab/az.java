package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;

final class az implements OnCancelListener {
    /* renamed from: a */
    final /* synthetic */ ay f1085a;

    az(ay ayVar) {
        this.f1085a = ayVar;
    }

    public final void onCancel(DialogInterface dialogInterface) {
        PurchaseActivity.m803b(this.f1085a.f1084c, this.f1085a.f1082a, this.f1085a.f1083b);
    }
}
