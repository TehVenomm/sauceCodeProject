package net.gogame.gopay.sdk.support;

import android.database.DataSetObserver;

/* renamed from: net.gogame.gopay.sdk.support.e */
final class C1076e extends DataSetObserver {
    /* renamed from: a */
    final /* synthetic */ C1074c f1228a;

    C1076e(C1074c c1074c) {
        this.f1228a = c1074c;
    }

    public final void onChanged() {
        this.f1228a.f1209i = true;
        this.f1228a.f1221u = false;
        this.f1228a.m909d();
        this.f1228a.invalidate();
        this.f1228a.requestLayout();
    }

    public final void onInvalidated() {
        this.f1228a.f1221u = false;
        this.f1228a.m909d();
        this.f1228a.m902b();
        this.f1228a.invalidate();
        this.f1228a.requestLayout();
    }
}
