package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1379k;

final class bp implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3500a;

    bp(PurchaseActivity purchaseActivity) {
        this.f3500a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f3500a.f3401I = false;
        if (!this.f3500a.f3413d) {
            return false;
        }
        PurchaseActivity.m3830b(this.f3500a, false);
        PurchaseActivity.m3815a(this.f3500a, this.f3500a.m3807a("paymentMethod"), ((C1379k) this.f3500a.f3394B.getItem(this.f3500a.f3399G.getSelectedItemPosition())).f3567a);
        return true;
    }
}
