package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.bp */
final class C1391bp implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1097a;

    C1391bp(PurchaseActivity purchaseActivity) {
        this.f1097a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1097a.f1024I = false;
        if (!this.f1097a.f1036d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1097a, false);
        PurchaseActivity.m798a(this.f1097a, this.f1097a.m790a("paymentMethod"), ((C1636k) this.f1097a.f1017B.getItem(this.f1097a.f1022G.getSelectedItemPosition())).f1300a);
        return true;
    }
}
