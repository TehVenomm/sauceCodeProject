package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1063k;

final class bp implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1112a;

    bp(PurchaseActivity purchaseActivity) {
        this.f1112a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1112a.f1013I = false;
        if (!this.f1112a.f1025d) {
            return false;
        }
        PurchaseActivity.m805b(this.f1112a, false);
        PurchaseActivity.m790a(this.f1112a, this.f1112a.m782a("paymentMethod"), ((C1063k) this.f1112a.f1006B.getItem(this.f1112a.f1011G.getSelectedItemPosition())).f1179a);
        return true;
    }
}
