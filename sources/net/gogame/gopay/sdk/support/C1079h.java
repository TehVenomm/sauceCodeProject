package net.gogame.gopay.sdk.support;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import android.widget.Scroller;

@TargetApi(11)
/* renamed from: net.gogame.gopay.sdk.support.h */
final class C1079h {
    static {
        if (VERSION.SDK_INT < 11) {
            throw new RuntimeException("Should not get to HoneycombPlus class unless sdk is >= 11!");
        }
    }

    /* renamed from: a */
    public static void m919a(Scroller scroller) {
        if (scroller != null) {
            scroller.setFriction(0.009f);
        }
    }
}
