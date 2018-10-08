package im.getsocial.sdk.internal.p082i.p083a;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p082i.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.i.a.upgqDBbsrL */
public final class upgqDBbsrL implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<String, String> {
    /* renamed from: a */
    private static final cjrhisSQCL f1976a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);

    /* renamed from: a */
    private static String m1988a(String str) {
        try {
            return jjbQypPegg.m1994a(str);
        } catch (IllegalArgumentException e) {
            f1976a.mo4387a("Returning default language en");
            return "en";
        }
    }
}
