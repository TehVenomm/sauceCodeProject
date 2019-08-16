package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.bt */
final class C1621bt implements C1652q {

    /* renamed from: a */
    C1623bv f1265a = this.f1266b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1266b;

    /* renamed from: c */
    final /* synthetic */ int f1267c;

    /* renamed from: d */
    final /* synthetic */ C1394bs f1268d;

    C1621bt(C1394bs bsVar, C1623bv bvVar, int i) {
        this.f1268d = bsVar;
        this.f1266b = bvVar;
        this.f1267c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1265a.f1274c == this.f1267c) {
            this.f1268d.mo21549a(this.f1265a, bitmap);
        }
    }
}
