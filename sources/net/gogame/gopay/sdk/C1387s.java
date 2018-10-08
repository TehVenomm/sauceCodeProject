package net.gogame.gopay.sdk;

import android.app.ProgressDialog;

/* renamed from: net.gogame.gopay.sdk.s */
final class C1387s implements Runnable {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f3580a;

    C1387s(StoreActivity storeActivity) {
        this.f3580a = storeActivity;
    }

    public final void run() {
        if (this.f3580a.f3346b == null) {
            this.f3580a.f3346b = new ProgressDialog(this.f3580a);
            this.f3580a.f3346b.setCancelable(false);
            this.f3580a.f3346b.setIndeterminate(true);
        }
        this.f3580a.f3346b.show();
    }
}
