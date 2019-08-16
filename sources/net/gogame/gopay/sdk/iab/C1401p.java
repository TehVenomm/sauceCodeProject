package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.p */
final class C1401p implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1110a;

    C1401p(PurchaseActivity purchaseActivity) {
        this.f1110a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1110a.f1018I = false;
        if (!this.f1110a.f1030d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1110a, false);
        PurchaseActivity.m798a(this.f1110a, this.f1110a.m790a("paymentMethod"), ((C1636k) this.f1110a.f1011B.getItem(this.f1110a.f1016G.getSelectedItemPosition())).f1288a);
        return true;
    }
}
