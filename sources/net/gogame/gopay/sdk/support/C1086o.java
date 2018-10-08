package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;

/* renamed from: net.gogame.gopay.sdk.support.o */
final class C1086o implements C1030q {
    /* renamed from: a */
    final /* synthetic */ int f1243a;
    /* renamed from: b */
    final /* synthetic */ C1039r f1244b;

    C1086o(int i, C1039r c1039r) {
        this.f1243a = i;
        this.f1244b = c1039r;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (bitmap != null) {
            C1084m.m940k();
        }
        if (C1084m.m941l() >= this.f1243a) {
            C1084m.f1239e.clear();
            if (this.f1244b != null) {
                C1039r c1039r = this.f1244b;
                C1084m.f1242h;
                c1039r.mo4425a();
            }
        }
    }
}
