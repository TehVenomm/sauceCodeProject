package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import net.gogame.gopay.sdk.support.C1030q;

final class bt implements C1030q {
    /* renamed from: a */
    bv f1119a = this.f1120b;
    /* renamed from: b */
    final /* synthetic */ bv f1120b;
    /* renamed from: c */
    final /* synthetic */ int f1121c;
    /* renamed from: d */
    final /* synthetic */ bs f1122d;

    bt(bs bsVar, bv bvVar, int i) {
        this.f1122d = bsVar;
        this.f1120b = bvVar;
        this.f1121c = i;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (this.f1119a.f1128c == this.f1121c) {
            this.f1122d.m850a(this.f1119a, bitmap);
        }
    }
}
