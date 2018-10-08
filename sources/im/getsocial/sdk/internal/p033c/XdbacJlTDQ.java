package im.getsocial.sdk.internal.p033c;

import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.XdbacJlTDQ */
public class XdbacJlTDQ {
    /* renamed from: a */
    private final String f1227a;

    public XdbacJlTDQ(String str) {
        Object obj = 1;
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "GetSocial appId may not be null.");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1515a(str), "GetSocial appId may not be empty.");
        if ((str.length() == 40 ? 1 : null) != null) {
            this.f1227a = XdbacJlTDQ.m1131a(str);
            return;
        }
        if (str.length() > 20) {
            obj = null;
        }
        if (obj != null) {
            this.f1227a = str;
            return;
        }
        throw new IllegalArgumentException("GetSocial appId must be less than or equal to 20 symbols or exactly 40 symbols.");
    }

    /* renamed from: a */
    private static String m1131a(String str) {
        String substring = str.substring(20);
        while (substring.charAt(0) == '0') {
            substring = substring.substring(1);
        }
        return substring;
    }

    /* renamed from: a */
    public final String m1132a() {
        return this.f1227a;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        return this.f1227a.equals(((XdbacJlTDQ) obj).f1227a);
    }

    public int hashCode() {
        return this.f1227a.hashCode();
    }
}
