package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1063k;

/* renamed from: net.gogame.gopay.sdk.iab.p */
final class C1052p implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1160a;

    C1052p(PurchaseActivity purchaseActivity) {
        this.f1160a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1160a.f1013I = false;
        if (!this.f1160a.f1025d) {
            return false;
        }
        PurchaseActivity.m805b(this.f1160a, false);
        PurchaseActivity.m790a(this.f1160a, this.f1160a.m782a("paymentMethod"), ((C1063k) this.f1160a.f1006B.getItem(this.f1160a.f1011G.getSelectedItemPosition())).f1179a);
        return true;
    }
}
