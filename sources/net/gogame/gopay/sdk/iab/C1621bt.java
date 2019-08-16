package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.bt */
final class C1621bt implements C1652q {

    /* renamed from: a */
    C1623bv f1253a = this.f1254b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1254b;

    /* renamed from: c */
    final /* synthetic */ int f1255c;

    /* renamed from: d */
    final /* synthetic */ C1394bs f1256d;

    C1621bt(C1394bs bsVar, C1623bv bvVar, int i) {
        this.f1256d = bsVar;
        this.f1254b = bvVar;
        this.f1255c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1253a.f1262c == this.f1255c) {
            this.f1256d.mo21549a(this.f1253a, bitmap);
        }
    }
}
