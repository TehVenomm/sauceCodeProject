package net.gogame.gopay.sdk.iab;

import net.gogame.gopay.sdk.support.C1415m;
import net.gogame.gopay.sdk.support.C1655u;

/* renamed from: net.gogame.gopay.sdk.iab.be */
final class C1618be implements C1655u {

    /* renamed from: a */
    final /* synthetic */ float f1259a;

    /* renamed from: b */
    final /* synthetic */ C1382bd f1260b;

    C1618be(C1382bd bdVar, float f) {
        this.f1260b = bdVar;
        this.f1259a = f;
    }

    /* renamed from: a */
    public final void mo22683a() {
        this.f1260b.f1082a.f1052t.edit().putFloat("_version_", this.f1259a).apply();
        C1415m.m926b();
        this.f1260b.f1082a.m804a((C1392bq) new C1386bi(this.f1260b.f1082a), false);
    }
}
