package net.gogame.gopay.sdk.support;

import android.database.DataSetObserver;

/* renamed from: net.gogame.gopay.sdk.support.e */
final class C1641e extends DataSetObserver {

    /* renamed from: a */
    final /* synthetic */ C1414c f1306a;

    C1641e(C1414c cVar) {
        this.f1306a = cVar;
    }

    public final void onChanged() {
        this.f1306a.f1154i = true;
        this.f1306a.f1166u = false;
        this.f1306a.m908d();
        this.f1306a.invalidate();
        this.f1306a.requestLayout();
    }

    public final void onInvalidated() {
        this.f1306a.f1166u = false;
        this.f1306a.m908d();
        this.f1306a.m901b();
        this.f1306a.invalidate();
        this.f1306a.requestLayout();
    }
}
