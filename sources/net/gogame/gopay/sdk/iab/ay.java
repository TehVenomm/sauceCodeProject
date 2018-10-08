package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.FrameLayout;

final class ay implements Runnable {
    /* renamed from: a */
    final /* synthetic */ int f1082a;
    /* renamed from: b */
    final /* synthetic */ String f1083b;
    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f1084c;

    ay(PurchaseActivity purchaseActivity, int i, String str) {
        this.f1084c = purchaseActivity;
        this.f1082a = i;
        this.f1083b = str;
    }

    public final void run() {
        View frameLayout = new FrameLayout(this.f1084c);
        frameLayout.setBackgroundColor(0);
        this.f1084c.setContentView(frameLayout);
        new Builder(this.f1084c).setTitle("Store Error").setMessage("Cannot Connect to Store, Please try again.").setNegativeButton("Dismiss", new ba(this)).setOnCancelListener(new az(this)).show();
    }
}
