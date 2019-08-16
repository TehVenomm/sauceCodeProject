package net.gogame.gopay.sdk.support;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.support.d */
final class C1640d implements OnTouchListener {

    /* renamed from: a */
    final /* synthetic */ C1414c f1293a;

    C1640d(C1414c cVar) {
        this.f1293a = cVar;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        return this.f1293a.f1145f.onTouchEvent(motionEvent);
    }
}
