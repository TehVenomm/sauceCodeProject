package im.getsocial.sdk.internal.p033c.p054e;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.usermanagement.p138a.p140b.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.c.e.jjbQypPegg */
public class jjbQypPegg implements upgqDBbsrL {
    /* renamed from: b */
    private static final cjrhisSQCL f1274b = upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg f1275a;
    /* renamed from: c */
    private final int f1276c = 3;
    /* renamed from: d */
    private int f1277d;

    public jjbQypPegg(int i) {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL m1248a() {
        im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL upgqdbbsrl = null;
        try {
            upgqdbbsrl = this.f1275a.m1378b();
        } catch (RuntimeException e) {
            f1274b.mo4387a("sdk not initialized");
        }
        return upgqdbbsrl;
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        Throwable th = (Throwable) obj;
        if (m1248a() == null) {
            return pdwpUtZXDT.m1660a(th);
        }
        Throwable a = im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th);
        if (a.getErrorCode() != ErrorCode.SDK_NOT_INITIALIZED || this.f1277d >= this.f1276c) {
            return pdwpUtZXDT.m1660a(a);
        }
        f1274b.mo4387a("session expired, re-authenticate");
        this.f1277d++;
        return pdwpUtZXDT.m1659a(this.f1275a.m1378b().m1322b()).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.pdwpUtZXDT()).m1665a(new im.getsocial.sdk.usermanagement.p138a.p140b.upgqDBbsrL()).m1669b(new zoToeBNOjF());
    }
}
