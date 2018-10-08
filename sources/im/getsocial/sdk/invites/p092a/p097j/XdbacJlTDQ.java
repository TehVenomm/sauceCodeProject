package im.getsocial.sdk.invites.p092a.p097j;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.InviteTextPlaceholders;
import java.net.URLEncoder;

/* renamed from: im.getsocial.sdk.invites.a.j.XdbacJlTDQ */
public final class XdbacJlTDQ {
    /* renamed from: a */
    private static final cjrhisSQCL f2427a = upgqDBbsrL.m1274a(XdbacJlTDQ.class);

    private XdbacJlTDQ() {
    }

    /* renamed from: a */
    private static String m2381a(String str) {
        try {
            return URLEncoder.encode(str, "UTF-8").replace("-", "%2D");
        } catch (Throwable e) {
            f2427a.mo4392b(e);
            return "";
        }
    }

    /* renamed from: a */
    public static String m2382a(String str, String str2, String str3, String str4, boolean z) {
        return XdbacJlTDQ.m2383a(XdbacJlTDQ.m2383a(XdbacJlTDQ.m2383a(str, "[APP_PACKAGE_NAME]", str2, z), "[USER_NAME]", str3, z), InviteTextPlaceholders.PLACEHOLDER_APP_INVITE_URL, str4, z).trim();
    }

    /* renamed from: a */
    private static String m2383a(String str, String str2, String str3, boolean z) {
        Object obj = (str3 == null || str3.trim().isEmpty()) ? 1 : null;
        if (obj != null) {
            return str;
        }
        CharSequence a;
        if (z) {
            a = XdbacJlTDQ.m2381a(str3);
        }
        return str.replace(str2, a);
    }
}
