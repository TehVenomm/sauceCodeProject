package net.gogame.gopay.sdk;

import android.app.ProgressDialog;

/* renamed from: net.gogame.gopay.sdk.s */
final class C1071s implements Runnable {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f1192a;

    C1071s(StoreActivity storeActivity) {
        this.f1192a = storeActivity;
    }

    public final void run() {
        if (this.f1192a.f958b == null) {
            this.f1192a.f958b = new ProgressDialog(this.f1192a);
            this.f1192a.f958b.setCancelable(false);
            this.f1192a.f958b.setIndeterminate(true);
        }
        this.f1192a.f958b.show();
    }
}
