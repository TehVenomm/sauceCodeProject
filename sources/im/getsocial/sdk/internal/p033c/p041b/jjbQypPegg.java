package im.getsocial.sdk.internal.p033c.p041b;

import android.app.Application;
import im.getsocial.sdk.internal.p033c.p057g.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.internal.c.b.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1235a = upgqDBbsrL.m1274a(jjbQypPegg.class);

    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static void m1192a(Application application) {
        pdwpUtZXDT.m1272a(new XdbacJlTDQ());
        pdwpUtZXDT pdwputzxdt = new pdwpUtZXDT();
        qdyNCsqjKt.m1216a(pdwputzxdt);
        HptYHntaqF.m1179a(application, pdwputzxdt);
        try {
            im.getsocial.sdk.internal.p091l.jjbQypPegg.m2092a().mo4721a(pdwputzxdt);
        } catch (Exception e) {
            f1235a.mo4387a("UI framework can not be found: " + e.getMessage());
        }
        ztWNWCuZiM.m1220a(new zoToeBNOjF(pdwputzxdt));
    }
}
