package net.gogame.gopay.sdk.support;

import android.database.DataSetObserver;

/* renamed from: net.gogame.gopay.sdk.support.e */
final class C1392e extends DataSetObserver {
    /* renamed from: a */
    final /* synthetic */ C1390c f3616a;

    C1392e(C1390c c1390c) {
        this.f3616a = c1390c;
    }

    public final void onChanged() {
        this.f3616a.f3597i = true;
        this.f3616a.f3609u = false;
        this.f3616a.m3934d();
        this.f3616a.invalidate();
        this.f3616a.requestLayout();
    }

    public final void onInvalidated() {
        this.f3616a.f3609u = false;
        this.f3616a.m3934d();
        this.f3616a.m3927b();
        this.f3616a.invalidate();
        this.f3616a.requestLayout();
    }
}
