package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;

/* renamed from: net.gogame.gopay.sdk.support.o */
final class C1650o implements C1652q {

    /* renamed from: a */
    final /* synthetic */ int f1313a;

    /* renamed from: b */
    final /* synthetic */ C1653r f1314b;

    C1650o(int i, C1653r rVar) {
        this.f1313a = i;
        this.f1314b = rVar;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (bitmap != null) {
            C1415m.m937k();
        }
        if (C1415m.m938l() >= this.f1313a) {
            C1415m.f1176e.clear();
            if (this.f1314b != null) {
                C1653r rVar = this.f1314b;
                C1415m.f1179h;
                rVar.mo22684a();
            }
        }
    }
}
