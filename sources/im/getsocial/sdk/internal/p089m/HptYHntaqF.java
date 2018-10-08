package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p090k.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.m.HptYHntaqF */
public final class HptYHntaqF {
    /* renamed from: b */
    private static final cjrhisSQCL f2209b = upgqDBbsrL.m1274a(HptYHntaqF.class);
    @XdbacJlTDQ
    /* renamed from: a */
    Context f2210a;

    private HptYHntaqF() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public static boolean m2107a() {
        HptYHntaqF hptYHntaqF = new HptYHntaqF();
        try {
            return ((Boolean) jjbQypPegg.m2088a("com.google.android.instantapps.InstantApps").m2089a("isInstantApp", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(hptYHntaqF.f2210a, Context.class)).m2091a()).booleanValue();
        } catch (Exception e) {
            f2209b.mo4388a("InstantApps is not available, error: %s", e.getMessage());
            return false;
        }
    }
}
