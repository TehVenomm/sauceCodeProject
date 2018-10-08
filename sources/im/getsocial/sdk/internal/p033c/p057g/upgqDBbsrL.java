package im.getsocial.sdk.internal.p033c.p057g;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.g.upgqDBbsrL */
public final class upgqDBbsrL implements cjrhisSQCL {
    /* renamed from: a */
    private final String f1288a;
    /* renamed from: b */
    private final zoToeBNOjF f1289b;
    /* renamed from: c */
    private final jjbQypPegg f1290c;

    private upgqDBbsrL(String str, jjbQypPegg jjbqyppegg, zoToeBNOjF zotoebnojf) {
        this.f1288a = "GetSocial_" + str;
        this.f1290c = jjbqyppegg;
        this.f1289b = zotoebnojf;
    }

    /* renamed from: a */
    public static cjrhisSQCL m1274a(Class cls) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) cls), "Can not create log with null or empty tag");
        String simpleName = cls.getSimpleName();
        boolean z = im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) simpleName) && im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1515a(simpleName);
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Can not create log with null or empty tag");
        return new upgqDBbsrL(simpleName, pdwpUtZXDT.m1269a(), pdwpUtZXDT.m1273b());
    }

    /* renamed from: a */
    private void m1275a(jjbQypPegg jjbqyppegg, String str) {
        if (m1278a(jjbqyppegg)) {
            if (str == null) {
                str = "null";
            }
            this.f1289b.mo4386a(jjbqyppegg, this.f1288a, str, new Object[0]);
        }
    }

    /* renamed from: a */
    private void m1276a(jjbQypPegg jjbqyppegg, String str, Object... objArr) {
        if (m1278a(jjbqyppegg)) {
            if (str == null) {
                str = "null";
            }
            this.f1289b.mo4386a(jjbqyppegg, this.f1288a, str, objArr);
        }
    }

    /* renamed from: a */
    private void m1277a(jjbQypPegg jjbqyppegg, Throwable th) {
        m1275a(jjbqyppegg, th == null ? null : pdwpUtZXDT.m1270a(th));
    }

    /* renamed from: a */
    private boolean m1278a(jjbQypPegg jjbqyppegg) {
        return jjbqyppegg.value() <= this.f1290c.value();
    }

    /* renamed from: a */
    public final void mo4387a(String str) {
        m1275a(jjbQypPegg.DEBUG, str);
    }

    /* renamed from: a */
    public final void mo4388a(String str, Object... objArr) {
        m1276a(jjbQypPegg.DEBUG, str, objArr);
    }

    /* renamed from: a */
    public final void mo4389a(Throwable th) {
        m1277a(jjbQypPegg.DEBUG, th);
    }

    /* renamed from: b */
    public final void mo4390b(String str) {
        m1275a(jjbQypPegg.INFO, str);
    }

    /* renamed from: b */
    public final void mo4391b(String str, Object... objArr) {
        m1276a(jjbQypPegg.WARN, str, objArr);
    }

    /* renamed from: b */
    public final void mo4392b(Throwable th) {
        m1277a(jjbQypPegg.WARN, th);
    }

    /* renamed from: c */
    public final void mo4393c(String str) {
        m1275a(jjbQypPegg.WARN, str);
    }

    /* renamed from: c */
    public final void mo4394c(String str, Object... objArr) {
        m1276a(jjbQypPegg.ERROR, str, objArr);
    }

    /* renamed from: c */
    public final void mo4395c(Throwable th) {
        m1277a(jjbQypPegg.ERROR, th);
    }

    /* renamed from: d */
    public final void mo4396d(String str) {
        m1275a(jjbQypPegg.ERROR, str);
    }
}
