package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;

/* renamed from: net.gogame.gopay.sdk.support.o */
final class C1402o implements C1346q {
    /* renamed from: a */
    final /* synthetic */ int f3631a;
    /* renamed from: b */
    final /* synthetic */ C1355r f3632b;

    C1402o(int i, C1355r c1355r) {
        this.f3631a = i;
        this.f3632b = c1355r;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (bitmap != null) {
            C1400m.m3965k();
        }
        if (C1400m.m3966l() >= this.f3631a) {
            C1400m.f3627e.clear();
            if (this.f3632b != null) {
                C1355r c1355r = this.f3632b;
                C1400m.f3630h;
                c1355r.mo4873a();
            }
        }
    }
}
