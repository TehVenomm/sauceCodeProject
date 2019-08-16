package net.gogame.gopay.sdk.iab;

import android.app.AlertDialog.Builder;
import android.widget.FrameLayout;

/* renamed from: net.gogame.gopay.sdk.iab.ay */
final class C1378ay implements Runnable {

    /* renamed from: a */
    final /* synthetic */ int f1069a;

    /* renamed from: b */
    final /* synthetic */ String f1070b;

    /* renamed from: c */
    final /* synthetic */ PurchaseActivity f1071c;

    C1378ay(PurchaseActivity purchaseActivity, int i, String str) {
        this.f1071c = purchaseActivity;
        this.f1069a = i;
        this.f1070b = str;
    }

    public final void run() {
        FrameLayout frameLayout = new FrameLayout(this.f1071c);
        frameLayout.setBackgroundColor(0);
        this.f1071c.setContentView(frameLayout);
        new Builder(this.f1071c).setTitle("Store Error").setMessage("Cannot Connect to Store, Please try again.").setNegativeButton("Dismiss", new C1617ba(this)).setOnCancelListener(new C1616az(this)).show();
    }
}
