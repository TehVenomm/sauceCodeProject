package im.getsocial.sdk.internal.p033c.p066m;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;

/* renamed from: im.getsocial.sdk.internal.c.m.jjbQypPegg */
public final class jjbQypPegg {

    /* renamed from: im.getsocial.sdk.internal.c.m.jjbQypPegg$cjrhisSQCL */
    public static class cjrhisSQCL {
        private cjrhisSQCL() {
        }

        /* renamed from: a */
        public static void m1511a(boolean z, String str) {
            if (!z) {
                throw new IllegalStateException(str);
            }
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.m.jjbQypPegg$jjbQypPegg */
    public static class jjbQypPegg {
        private jjbQypPegg() {
        }

        /* renamed from: a */
        public static void m1512a(boolean z, String str) {
            if (!z) {
                throw new IllegalArgumentException(str);
            }
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.m.jjbQypPegg$upgqDBbsrL */
    public static class upgqDBbsrL {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        public static void m1513a(boolean z, String str) {
            if (!z) {
                throw new GetSocialException(ErrorCode.NO_INTERNET, str);
            }
        }
    }

    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static boolean m1514a(Object obj) {
        return obj != null;
    }

    /* renamed from: a */
    public static boolean m1515a(String str) {
        jjbQypPegg.m1512a(jjbQypPegg.m1514a((Object) str), "Don't forget to check if argument is not null before empty check");
        return !str.isEmpty();
    }

    /* renamed from: b */
    public static boolean m1516b(String str) {
        return !jjbQypPegg.m1517c(str);
    }

    /* renamed from: c */
    public static boolean m1517c(String str) {
        return str == null || str.isEmpty();
    }
}
