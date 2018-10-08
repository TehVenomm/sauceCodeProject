package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.x */
final class C1059x implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1168a;

    C1059x(PurchaseActivity purchaseActivity) {
        this.f1168a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1168a.f1013I = false;
        if (!this.f1168a.f1025d) {
            return false;
        }
        PurchaseActivity.m805b(this.f1168a, false);
        return this.f1168a.f1014J;
    }
}
