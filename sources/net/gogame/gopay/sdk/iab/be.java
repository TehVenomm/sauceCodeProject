package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.support.C1038u;
import net.gogame.gopay.sdk.support.C1084m;

final class be implements C1038u {
    /* renamed from: a */
    final /* synthetic */ float f1092a;
    /* renamed from: b */
    final /* synthetic */ bd f1093b;

    be(bd bdVar, float f) {
        this.f1093b = bdVar;
        this.f1092a = f;
    }

    /* renamed from: a */
    public final void mo4424a() {
        this.f1093b.f1091a.f1041t.edit().putFloat("_version_", this.f1092a).apply();
        C1084m.m929b();
        this.f1093b.f1091a.m796a(new bi(this.f1093b.f1091a), false);
    }
}
