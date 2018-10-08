package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

final class ar implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3459a;

    ar(PurchaseActivity purchaseActivity) {
        this.f3459a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (!((this.f3459a.f3402J && this.f3459a.f3413d) || this.f3459a.f3418i == null)) {
            this.f3459a.f3404L = true;
            this.f3459a.f3431v.postDelayed(this.f3459a.f3433x, (long) this.f3459a.f3407O);
        }
        return false;
    }
}
