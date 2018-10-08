package im.getsocial.sdk.pushnotifications.p067a;

import android.content.Intent;
import android.os.Bundle;
import im.getsocial.p015a.p016a.p017a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ;

/* renamed from: im.getsocial.sdk.pushnotifications.a.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final String f2509a = Notification.class.getName();
    /* renamed from: b */
    private static final cjrhisSQCL f2510b = upgqDBbsrL.m1274a(jjbQypPegg.class);

    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static XdbacJlTDQ m2463a(Intent intent) {
        if (!intent.hasExtra(f2509a)) {
            return null;
        }
        String stringExtra = intent.getStringExtra(f2509a);
        intent.removeExtra(f2509a);
        return jjbQypPegg.m2465a(stringExtra);
    }

    /* renamed from: a */
    public static XdbacJlTDQ m2464a(bpiSwUyLit bpiswuylit) {
        if (!bpiswuylit.mo4361a(f2509a)) {
            return null;
        }
        String b = bpiswuylit.mo4362b(f2509a);
        bpiswuylit.mo4365e(f2509a);
        return jjbQypPegg.m2465a(b);
    }

    /* renamed from: a */
    private static XdbacJlTDQ m2465a(String str) {
        try {
            return (XdbacJlTDQ) im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg.m2414a(str);
        } catch (pdwpUtZXDT e) {
            return null;
        }
    }

    /* renamed from: a */
    public static void m2466a(Intent intent, XdbacJlTDQ xdbacJlTDQ) {
        intent.putExtra(f2509a, xdbacJlTDQ.m2424e());
    }

    /* renamed from: a */
    public static void m2467a(bpiSwUyLit bpiswuylit, XdbacJlTDQ xdbacJlTDQ) {
        bpiswuylit.mo4360a(f2509a, xdbacJlTDQ.m2424e());
    }

    /* renamed from: b */
    public static im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg m2468b(Intent intent) {
        Bundle extras = intent.getExtras();
        if (extras == null) {
            return null;
        }
        for (String str : extras.keySet()) {
            Object obj = extras.get(str);
            if (obj instanceof String) {
                String str2 = (String) obj;
                try {
                    return im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg.m2415b(str2);
                } catch (pdwpUtZXDT e) {
                    f2510b.mo4387a(str + " does not contain GetSocialNotification, it is " + str2);
                }
            }
        }
        throw new pdwpUtZXDT(2, "Can not find GetSocial data in " + extras);
    }
}
