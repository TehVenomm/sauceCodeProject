package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

final class af implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1055a;

    af(PurchaseActivity purchaseActivity) {
        this.f1055a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (!this.f1055a.f1025d) {
            return false;
        }
        PurchaseActivity.m805b(this.f1055a, false);
        return this.f1055a.f1014J;
    }
}
