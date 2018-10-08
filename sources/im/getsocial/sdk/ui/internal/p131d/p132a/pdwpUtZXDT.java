package im.getsocial.sdk.ui.internal.p131d.p132a;

import im.getsocial.sdk.ui.internal.p131d.p133b.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.ui.internal.d.a.pdwpUtZXDT */
public class pdwpUtZXDT {
    @cjrhisSQCL(a = "scale-mode")
    /* renamed from: a */
    jjbQypPegg f2893a;
    @cjrhisSQCL(a = "width")
    /* renamed from: b */
    fOrCGNYyfk f2894b;
    @cjrhisSQCL(a = "height")
    /* renamed from: c */
    fOrCGNYyfk f2895c;
    @cjrhisSQCL(a = "ppi")
    /* renamed from: d */
    qZypgoeblR f2896d;

    /* renamed from: im.getsocial.sdk.ui.internal.d.a.pdwpUtZXDT$jjbQypPegg */
    public enum jjbQypPegg {
        CONSTANT_PHYSICAL_SIZE,
        SCALE_WITH_SCREEN_SIZE
    }

    /* renamed from: a */
    public final jjbQypPegg m3193a() {
        return this.f2893a;
    }

    /* renamed from: b */
    public final fOrCGNYyfk m3194b() {
        return this.f2894b;
    }

    /* renamed from: c */
    public final fOrCGNYyfk m3195c() {
        return this.f2895c;
    }

    /* renamed from: d */
    public final qZypgoeblR m3196d() {
        return this.f2896d;
    }

    /* renamed from: e */
    public final boolean m3197e() {
        return this.f2895c.m3174a() > this.f2894b.m3174a();
    }
}
