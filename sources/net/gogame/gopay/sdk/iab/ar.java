package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

final class ar implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1071a;

    ar(PurchaseActivity purchaseActivity) {
        this.f1071a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (!((this.f1071a.f1014J && this.f1071a.f1025d) || this.f1071a.f1030i == null)) {
            this.f1071a.f1016L = true;
            this.f1071a.f1043v.postDelayed(this.f1071a.f1045x, (long) this.f1071a.f1019O);
        }
        return false;
    }
}
