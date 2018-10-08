package im.getsocial.sdk.pushnotifications.p067a.p103b;

import com.facebook.share.internal.ShareConstants;
import im.getsocial.p015a.p016a.pdwpUtZXDT;
import im.getsocial.p015a.p016a.upgqDBbsrL;
import im.getsocial.sdk.pushnotifications.Notification;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.pushnotifications.a.b.XdbacJlTDQ */
public final class XdbacJlTDQ extends jjbQypPegg {
    /* renamed from: a */
    private final String f2460a;
    /* renamed from: b */
    private final Notification f2461b;
    /* renamed from: c */
    private final Map<String, String> f2462c;
    /* renamed from: d */
    private final String f2463d;
    /* renamed from: e */
    private boolean f2464e = false;

    XdbacJlTDQ(pdwpUtZXDT pdwputzxdt, String str, String str2) {
        Map pdwputzxdt2 = new pdwpUtZXDT(pdwputzxdt);
        if (str != null) {
            pdwputzxdt2.put("text", str);
        }
        if (str2 != null) {
            pdwputzxdt2.put("title", str2);
        }
        this.f2460a = pdwpUtZXDT.m725a(pdwputzxdt2);
        if (str == null) {
            str = (String) pdwputzxdt.get("text");
        }
        if (str2 == null) {
            str2 = (String) pdwputzxdt.get("title");
        }
        this.f2463d = (String) pdwputzxdt.get("uid");
        Object obj = pdwputzxdt.get("ap");
        this.f2462c = obj instanceof pdwpUtZXDT ? new HashMap((pdwpUtZXDT) obj) : new HashMap();
        upgqDBbsrL upgqdbbsrl = (upgqDBbsrL) pdwputzxdt.get("a");
        boolean z = upgqdbbsrl == null || upgqdbbsrl.isEmpty();
        this.f2461b = new Notification((String) pdwputzxdt.get("id"), false, ((Number) pdwputzxdt.get(ShareConstants.MEDIA_TYPE)).intValue(), ((Number) pdwputzxdt.get("ts")).longValue(), XdbacJlTDQ.m2417d(str2), XdbacJlTDQ.m2417d(str), z ? 0 : ((Number) ((pdwpUtZXDT) upgqdbbsrl.get(0)).get("t")).intValue(), z ? new HashMap() : new HashMap((pdwpUtZXDT) ((pdwpUtZXDT) upgqdbbsrl.get(0)).get("d")));
    }

    /* renamed from: d */
    private static String m2417d(String str) {
        return str == null ? "" : str;
    }

    /* renamed from: a */
    public final void m2418a(boolean z) {
        this.f2464e = true;
    }

    /* renamed from: a */
    public final boolean mo4576a() {
        return false;
    }

    /* renamed from: b */
    public final Map<String, String> m2420b() {
        return this.f2462c;
    }

    /* renamed from: c */
    public final Notification m2421c() {
        return this.f2461b;
    }

    /* renamed from: c */
    public final boolean m2422c(String str) {
        return this.f2463d.equals(str);
    }

    /* renamed from: d */
    public final boolean m2423d() {
        return this.f2464e;
    }

    /* renamed from: e */
    public final String m2424e() {
        return this.f2460a;
    }
}
