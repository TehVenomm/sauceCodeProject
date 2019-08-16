package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.x */
final class C1404x implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1119a;

    C1404x(PurchaseActivity purchaseActivity) {
        this.f1119a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1119a.f1024I = false;
        if (!this.f1119a.f1036d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1119a, false);
        return this.f1119a.f1025J;
    }
}
