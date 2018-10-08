package net.gogame.gopay.sdk.support;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.support.d */
final class C1391d implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ C1390c f3615a;

    C1391d(C1390c c1390c) {
        this.f3615a = c1390c;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        return this.f3615a.f3594f.onTouchEvent(motionEvent);
    }
}
