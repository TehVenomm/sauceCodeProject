package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1379k;

/* renamed from: net.gogame.gopay.sdk.iab.p */
final class C1368p implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f3548a;

    C1368p(PurchaseActivity purchaseActivity) {
        this.f3548a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f3548a.f3401I = false;
        if (!this.f3548a.f3413d) {
            return false;
        }
        PurchaseActivity.m3830b(this.f3548a, false);
        PurchaseActivity.m3815a(this.f3548a, this.f3548a.m3807a("paymentMethod"), ((C1379k) this.f3548a.f3394B.getItem(this.f3548a.f3399G.getSelectedItemPosition())).f3567a);
        return true;
    }
}
