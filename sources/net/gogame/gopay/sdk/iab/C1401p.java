package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.p */
final class C1401p implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1116a;

    C1401p(PurchaseActivity purchaseActivity) {
        this.f1116a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1116a.f1024I = false;
        if (!this.f1116a.f1036d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1116a, false);
        PurchaseActivity.m798a(this.f1116a, this.f1116a.m790a("paymentMethod"), ((C1636k) this.f1116a.f1017B.getItem(this.f1116a.f1022G.getSelectedItemPosition())).f1300a);
        return true;
    }
}
