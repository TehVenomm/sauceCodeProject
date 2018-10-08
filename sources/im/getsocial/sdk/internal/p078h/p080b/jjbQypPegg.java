package im.getsocial.sdk.internal.p078h.p080b;

import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p066m.pdwpUtZXDT;
import org.apache.commons.lang3.StringUtils;

/* renamed from: im.getsocial.sdk.internal.h.b.jjbQypPegg */
public final class jjbQypPegg implements upgqDBbsrL<String, XdbacJlTDQ> {
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: a */
    pdwpUtZXDT f1916a;
    /* renamed from: b */
    private final upgqDBbsrL f1917b = upgqDBbsrL.m1940a(this.f1916a);

    /* renamed from: im.getsocial.sdk.internal.h.b.jjbQypPegg$upgqDBbsrL */
    private static abstract class upgqDBbsrL {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        static upgqDBbsrL m1940a(pdwpUtZXDT pdwputzxdt) {
            switch (pdwputzxdt) {
                case ANDROID:
                    return new jjbQypPegg();
                case IOS:
                    return new pdwpUtZXDT();
                default:
                    return new cjrhisSQCL();
            }
        }

        /* renamed from: f */
        private String m1942f() {
            return mo4554a() + StringUtils.LF + mo4556c() + "\nCheck the documentation for more information.";
        }

        /* renamed from: a */
        protected abstract String mo4554a();

        /* renamed from: b */
        protected abstract String mo4555b();

        /* renamed from: c */
        protected abstract String mo4556c();

        /* renamed from: d */
        final String m1946d() {
            return mo4555b() + StringUtils.LF + mo4556c() + "\nCheck the documentation for more information.";
        }

        /* renamed from: e */
        final String m1947e() {
            return "Your APP_ID is empty. " + m1942f();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.h.b.jjbQypPegg$cjrhisSQCL */
    private static final class cjrhisSQCL extends upgqDBbsrL {
        private cjrhisSQCL() {
            super();
        }

        /* renamed from: a */
        protected final String mo4554a() {
            return "AppId is not valid.";
        }

        /* renamed from: b */
        protected final String mo4555b() {
            return "AppId is null. ";
        }

        /* renamed from: c */
        protected final String mo4556c() {
            return "";
        }
    }

    /* renamed from: im.getsocial.sdk.internal.h.b.jjbQypPegg$jjbQypPegg */
    private static final class jjbQypPegg extends upgqDBbsrL {
        private jjbQypPegg() {
            super();
        }

        /* renamed from: a */
        protected final String mo4554a() {
            return "Check that you have set up <meta-data> tag correctly in your AndroidManifest.xml:";
        }

        /* renamed from: b */
        protected final String mo4555b() {
            return "You need to add <meta-data> tag into your Application tag in AndroidManifest.xml:";
        }

        /* renamed from: c */
        protected final String mo4556c() {
            return "<meta-data\n\t\tandroid:name=\"im.getsocial.sdk.AppId\"\n\t\tandroid:value=\"YOUR_APP_ID\" />";
        }
    }

    /* renamed from: im.getsocial.sdk.internal.h.b.jjbQypPegg$pdwpUtZXDT */
    private static final class pdwpUtZXDT extends upgqDBbsrL {
        private pdwpUtZXDT() {
            super();
        }

        /* renamed from: a */
        protected final String mo4554a() {
            return "Check you have correctly setup AppId key in your application Info.plist file:";
        }

        /* renamed from: b */
        protected final String mo4555b() {
            return "You need to add AppId key to application Info.plist file:";
        }

        /* renamed from: c */
        protected final String mo4556c() {
            return "\t<key>im.getsocial.sdk.AppId</key>\n\t<string>YOUR_APP_ID</string>";
        }
    }

    jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private XdbacJlTDQ m1957a(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), this.f1917b.m1946d());
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1515a(str), this.f1917b.m1947e());
        try {
            return new XdbacJlTDQ(str);
        } catch (Exception e) {
            throw new IllegalArgumentException(e.getMessage() + StringUtils.LF + this.f1917b.m1942f());
        }
    }

    /* renamed from: a */
    public static jjbQypPegg m1958a() {
        return new jjbQypPegg();
    }
}
