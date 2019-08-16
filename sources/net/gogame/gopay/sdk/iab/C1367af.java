package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.af */
final class C1367af implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1063a;

    C1367af(PurchaseActivity purchaseActivity) {
        this.f1063a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (!this.f1063a.f1036d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1063a, false);
        return this.f1063a.f1025J;
    }
}
