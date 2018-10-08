package im.getsocial.sdk.activities.p028a.p031b;

import im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p086j.p088b.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.activities.a.b.pdwpUtZXDT */
public class pdwpUtZXDT implements upgqDBbsrL<cjrhisSQCL, jjbQypPegg> {
    /* renamed from: a */
    private final jjbQypPegg f1164a;
    /* renamed from: b */
    private im.getsocial.sdk.internal.p086j.p088b.upgqDBbsrL f1165b;

    public pdwpUtZXDT(jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg2) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) jjbqyppegg), "Can not create SetMediaContentUrlFunc with null content");
        this.f1164a = jjbqyppegg;
        if (jjbqyppegg2 != null) {
            this.f1165b = jjbqyppegg2.m2010b();
        }
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        cjrhisSQCL cjrhissqcl = (cjrhisSQCL) obj;
        if (!(cjrhissqcl == null || this.f1165b == null)) {
            if (this.f1165b == im.getsocial.sdk.internal.p086j.p088b.upgqDBbsrL.IMAGE) {
                this.f1164a.m960d(cjrhissqcl.m2004a());
            } else if (this.f1165b == im.getsocial.sdk.internal.p086j.p088b.upgqDBbsrL.VIDEO) {
                this.f1164a.m960d(cjrhissqcl.m2004a());
                this.f1164a.m962e(cjrhissqcl.m2006c());
            }
        }
        return this.f1164a;
    }
}
