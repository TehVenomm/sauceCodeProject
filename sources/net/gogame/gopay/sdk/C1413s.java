package net.gogame.gopay.sdk;

import android.app.ProgressDialog;

/* renamed from: net.gogame.gopay.sdk.s */
final class C1413s implements Runnable {

    /* renamed from: a */
    final /* synthetic */ StoreActivity f1139a;

    C1413s(StoreActivity storeActivity) {
        this.f1139a = storeActivity;
    }

    public final void run() {
        if (this.f1139a.f989b == null) {
            this.f1139a.f989b = new ProgressDialog(this.f1139a);
            this.f1139a.f989b.setCancelable(false);
            this.f1139a.f989b.setIndeterminate(true);
        }
        this.f1139a.f989b.show();
    }
}
