package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import net.gogame.gopay.sdk.support.C1346q;

final class bt implements C1346q {
    /* renamed from: a */
    bv f3507a = this.f3508b;
    /* renamed from: b */
    final /* synthetic */ bv f3508b;
    /* renamed from: c */
    final /* synthetic */ int f3509c;
    /* renamed from: d */
    final /* synthetic */ bs f3510d;

    bt(bs bsVar, bv bvVar, int i) {
        this.f3510d = bsVar;
        this.f3508b = bvVar;
        this.f3509c = i;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (this.f3507a.f3516c == this.f3509c) {
            this.f3510d.m3875a(this.f3507a, bitmap);
        }
    }
}
