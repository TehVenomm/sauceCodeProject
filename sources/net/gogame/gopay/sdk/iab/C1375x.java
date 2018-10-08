package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.x */
final class C1375x implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3556a;

    C1375x(PurchaseActivity purchaseActivity) {
        this.f3556a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f3556a.f3401I = false;
        if (!this.f3556a.f3413d) {
            return false;
        }
        PurchaseActivity.m3830b(this.f3556a, false);
        return this.f3556a.f3402J;
    }
}
