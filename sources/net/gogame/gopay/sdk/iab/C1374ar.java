package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.ar */
final class C1374ar implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1064a;

    C1374ar(PurchaseActivity purchaseActivity) {
        this.f1064a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if ((!this.f1064a.f1019J || !this.f1064a.f1030d) && this.f1064a.f1035i != null) {
            this.f1064a.f1021L = true;
            this.f1064a.f1048v.postDelayed(this.f1064a.f1050x, (long) this.f1064a.f1024O);
        }
        return false;
    }
}
