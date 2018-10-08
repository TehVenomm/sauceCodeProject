package im.getsocial.sdk.activities.p028a.p031b;

import im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;

/* renamed from: im.getsocial.sdk.activities.a.b.XdbacJlTDQ */
public final class XdbacJlTDQ implements upgqDBbsrL<jjbQypPegg, jjbQypPegg> {
    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        boolean z = false;
        jjbQypPegg jjbqyppegg = (jjbQypPegg) obj;
        boolean z2 = jjbqyppegg.m967h() != null && jjbqyppegg.m967h().m2011c();
        boolean z3 = (ztWNWCuZiM.m1521a(jjbqyppegg.m969j()) || ztWNWCuZiM.m1521a(jjbqyppegg.m968i())) ? false : true;
        boolean z4 = !ztWNWCuZiM.m1521a(jjbqyppegg.m961d());
        if (z3 || z2 || z4) {
            z = true;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Can not post without any data.");
        return jjbqyppegg;
    }
}
