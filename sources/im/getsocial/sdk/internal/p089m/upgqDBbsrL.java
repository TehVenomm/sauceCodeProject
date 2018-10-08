package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p090k.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.m.upgqDBbsrL */
final class upgqDBbsrL {
    /* renamed from: a */
    private static final cjrhisSQCL f2229a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);

    private upgqDBbsrL() {
    }

    /* renamed from: a */
    static String m2150a(Context context) {
        try {
            return (String) jjbQypPegg.m2088a("com.google.android.gms.ads.identifier.AdvertisingIdClient").m2089a("getAdvertisingIdInfo", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(context.getApplicationContext(), Context.class)).m2090a("getId", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]).m2091a();
        } catch (Exception e) {
            f2229a.mo4388a("AdvertisingId is not available, error: %s", e.getMessage());
            return "";
        }
    }
}
