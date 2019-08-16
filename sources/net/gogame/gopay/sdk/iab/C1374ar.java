package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.iab.ar */
final class C1374ar implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1070a;

    C1374ar(PurchaseActivity purchaseActivity) {
        this.f1070a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if ((!this.f1070a.f1025J || !this.f1070a.f1036d) && this.f1070a.f1041i != null) {
            this.f1070a.f1027L = true;
            this.f1070a.f1054v.postDelayed(this.f1070a.f1056x, (long) this.f1070a.f1030O);
        }
        return false;
    }
}
