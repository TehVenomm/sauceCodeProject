package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.view.View;
import android.widget.FrameLayout;

final class ay implements Runnable {
    /* renamed from: a */
    final /* synthetic */ int f3470a;
    /* renamed from: b */
    final /* synthetic */ String f3471b;
    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f3472c;

    ay(PurchaseActivity purchaseActivity, int i, String str) {
        this.f3472c = purchaseActivity;
        this.f3470a = i;
        this.f3471b = str;
    }

    public final void run() {
        View frameLayout = new FrameLayout(this.f3472c);
        frameLayout.setBackgroundColor(0);
        this.f3472c.setContentView(frameLayout);
        new Builder(this.f3472c).setTitle("Store Error").setMessage("Cannot Connect to Store, Please try again.").setNegativeButton("Dismiss", new ba(this)).setOnCancelListener(new az(this)).show();
    }
}
