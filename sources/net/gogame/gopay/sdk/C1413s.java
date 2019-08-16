package net.gogame.gopay.sdk;

import android.app.ProgressDialog;

/* renamed from: net.gogame.gopay.sdk.s */
final class C1413s implements Runnable {

    /* renamed from: a */
    final /* synthetic */ StoreActivity f1133a;

    C1413s(StoreActivity storeActivity) {
        this.f1133a = storeActivity;
    }

    public final void run() {
        if (this.f1133a.f983b == null) {
            this.f1133a.f983b = new ProgressDialog(this.f1133a);
            this.f1133a.f983b.setCancelable(false);
            this.f1133a.f983b.setIndeterminate(true);
        }
        this.f1133a.f983b.show();
    }
}
