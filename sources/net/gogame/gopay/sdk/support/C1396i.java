package net.gogame.gopay.sdk.support;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import android.widget.Scroller;

@TargetApi(14)
/* renamed from: net.gogame.gopay.sdk.support.i */
final class C1396i {
    static {
        if (VERSION.SDK_INT < 14) {
            throw new RuntimeException("Should not get to IceCreamSandwichPlus class unless sdk is >= 14!");
        }
    }

    /* renamed from: a */
    public static float m3945a(Scroller scroller) {
        return scroller.getCurrVelocity();
    }
}
