package net.gogame.gopay.sdk.support;

import android.database.DataSetObserver;

/* renamed from: net.gogame.gopay.sdk.support.e */
final class C1641e extends DataSetObserver {

    /* renamed from: a */
    final /* synthetic */ C1414c f1294a;

    C1641e(C1414c cVar) {
        this.f1294a = cVar;
    }

    public final void onChanged() {
        this.f1294a.f1148i = true;
        this.f1294a.f1160u = false;
        this.f1294a.m908d();
        this.f1294a.invalidate();
        this.f1294a.requestLayout();
    }

    public final void onInvalidated() {
        this.f1294a.f1160u = false;
        this.f1294a.m908d();
        this.f1294a.m901b();
        this.f1294a.invalidate();
        this.f1294a.requestLayout();
    }
}
