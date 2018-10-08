package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.support.C1354u;
import net.gogame.gopay.sdk.support.C1400m;

final class be implements C1354u {
    /* renamed from: a */
    final /* synthetic */ float f3480a;
    /* renamed from: b */
    final /* synthetic */ bd f3481b;

    be(bd bdVar, float f) {
        this.f3481b = bdVar;
        this.f3480a = f;
    }

    /* renamed from: a */
    public final void mo4872a() {
        this.f3481b.f3479a.f3429t.edit().putFloat("_version_", this.f3480a).apply();
        C1400m.m3954b();
        this.f3481b.f3479a.m3821a(new bi(this.f3481b.f3479a), false);
    }
}
