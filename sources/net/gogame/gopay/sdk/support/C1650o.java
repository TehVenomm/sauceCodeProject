package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;

/* renamed from: net.gogame.gopay.sdk.support.o */
final class C1650o implements C1652q {

    /* renamed from: a */
    final /* synthetic */ int f1301a;

    /* renamed from: b */
    final /* synthetic */ C1653r f1302b;

    C1650o(int i, C1653r rVar) {
        this.f1301a = i;
        this.f1302b = rVar;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (bitmap != null) {
            C1415m.m937k();
        }
        if (C1415m.m938l() >= this.f1301a) {
            C1415m.f1170e.clear();
            if (this.f1302b != null) {
                C1653r rVar = this.f1302b;
                C1415m.f1173h;
                rVar.mo22684a();
            }
        }
    }
}
