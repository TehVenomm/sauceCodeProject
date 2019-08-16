package net.gogame.gopay.sdk.iab;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import net.gogame.gopay.sdk.C1636k;

/* renamed from: net.gogame.gopay.sdk.iab.bp */
final class C1391bp implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1091a;

    C1391bp(PurchaseActivity purchaseActivity) {
        this.f1091a = purchaseActivity;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.f1091a.f1018I = false;
        if (!this.f1091a.f1030d) {
            return false;
        }
        PurchaseActivity.m813b(this.f1091a, false);
        PurchaseActivity.m798a(this.f1091a, this.f1091a.m790a("paymentMethod"), ((C1636k) this.f1091a.f1011B.getItem(this.f1091a.f1016G.getSelectedItemPosition())).f1288a);
        return true;
    }
}
