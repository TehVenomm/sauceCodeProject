package im.getsocial.sdk.socialgraph.p109a.p110a;

import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;

/* renamed from: im.getsocial.sdk.socialgraph.a.a.cjrhisSQCL */
public final class cjrhisSQCL {
    private cjrhisSQCL() {
    }

    /* renamed from: a */
    public static void m2482a(String str) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "User id can not be null or empty");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1515a(str.trim()), "User id can not be null or empty");
        jjbQypPegg.m1512a(str.matches("[0-9]+"), "User id should contain only numbers");
    }
}
