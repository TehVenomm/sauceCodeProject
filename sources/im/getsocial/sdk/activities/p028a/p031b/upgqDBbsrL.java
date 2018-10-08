package im.getsocial.sdk.activities.p028a.p031b;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;

/* renamed from: im.getsocial.sdk.activities.a.b.upgqDBbsrL */
public final class upgqDBbsrL implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<jjbQypPegg, pdwpUtZXDT<ActivityPost>> {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg f1167a;

    public upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        jjbQypPegg jjbqyppegg = (jjbQypPegg) obj;
        switch (jjbqyppegg.m954a()) {
            case POST:
                return this.f1167a.mo4429a(jjbQypPegg.m973a(jjbqyppegg.m959c()), jjbqyppegg);
            case COMMENT:
                return this.f1167a.mo4442b(jjbqyppegg.m957b(), jjbqyppegg);
            default:
                throw new RuntimeException("Communication method in not implemented for content type: " + jjbqyppegg.m954a());
        }
    }
}
