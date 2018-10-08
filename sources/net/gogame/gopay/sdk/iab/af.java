package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

final class af implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3443a;

    af(PurchaseActivity purchaseActivity) {
        this.f3443a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (!this.f3443a.f3413d) {
            return false;
        }
        PurchaseActivity.m3830b(this.f3443a, false);
        return this.f3443a.f3402J;
    }
}
