package net.gogame.gopay.sdk.support;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;

/* renamed from: net.gogame.gopay.sdk.support.d */
final class C1075d implements OnTouchListener {
    /* renamed from: a */
    final /* synthetic */ C1074c f1227a;

    C1075d(C1074c c1074c) {
        this.f1227a = c1074c;
    }

    public final boolean onTouch(View view, MotionEvent motionEvent) {
        return this.f1227a.f1206f.onTouchEvent(motionEvent);
    }
}
