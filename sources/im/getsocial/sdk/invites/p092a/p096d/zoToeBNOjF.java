package im.getsocial.sdk.invites.p092a.p096d;

import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;
import im.getsocial.sdk.invites.LocalizableText;
import im.getsocial.sdk.invites.cjrhisSQCL;
import im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT;
import im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.d.zoToeBNOjF */
public final class zoToeBNOjF implements upgqDBbsrL<pdwpUtZXDT, pdwpUtZXDT> {
    /* renamed from: a */
    private final pdwpUtZXDT f2365a;
    /* renamed from: b */
    private final pdwpUtZXDT f2366b;

    public zoToeBNOjF(pdwpUtZXDT pdwputzxdt, pdwpUtZXDT pdwputzxdt2) {
        this.f2365a = pdwputzxdt;
        this.f2366b = pdwputzxdt2;
    }

    /* renamed from: a */
    private static pdwpUtZXDT m2319a(pdwpUtZXDT pdwputzxdt, pdwpUtZXDT pdwputzxdt2, boolean z) {
        Object obj = null;
        jjbQypPegg d = pdwpUtZXDT.m2276a().m2266a(pdwputzxdt.m2279d()).m2270b(pdwputzxdt.m2280e()).m2273c(pdwputzxdt.m2281f()).m2275e(pdwputzxdt.m2284i()).m2274d(pdwputzxdt.m2283h());
        if (pdwputzxdt2 != null) {
            Map hashMap;
            if (!pdwputzxdt.m2278c()) {
                if (z) {
                    Object obj2 = pdwputzxdt2.m2281f() != null ? 1 : null;
                    Object obj3 = pdwputzxdt2.m2284i() != null ? 1 : null;
                    if (pdwputzxdt2.m2283h() != null) {
                        obj = 1;
                    }
                    if (!(obj2 == null && obj3 == null && r2 == null)) {
                        d.m2273c(pdwputzxdt2.m2281f());
                        d.m2275e(pdwputzxdt2.m2284i());
                        d.m2274d(pdwputzxdt2.m2283h());
                    }
                } else {
                    if (pdwputzxdt2.m2281f() != null) {
                        d.m2273c(pdwputzxdt2.m2281f());
                    }
                    if (pdwputzxdt2.m2284i() != null) {
                        d.m2275e(pdwputzxdt2.m2284i());
                    }
                    if (pdwputzxdt2.m2283h() != null) {
                        d.m2274d(pdwputzxdt2.m2283h());
                    }
                }
            }
            if (pdwputzxdt2.m2279d() != null) {
                hashMap = new HashMap(cjrhisSQCL.m2405a(pdwputzxdt2.m2279d()));
                if (pdwputzxdt != null) {
                    zoToeBNOjF.m2320a(hashMap, cjrhisSQCL.m2405a(pdwputzxdt.m2279d()));
                }
                d.m2266a(new LocalizableText(hashMap));
            }
            if (pdwputzxdt2.m2280e() != null) {
                hashMap = new HashMap(cjrhisSQCL.m2405a(pdwputzxdt2.m2280e()));
                if (pdwputzxdt != null) {
                    zoToeBNOjF.m2320a(hashMap, cjrhisSQCL.m2405a(pdwputzxdt.m2280e()));
                }
                d.m2270b(new LocalizableText(hashMap));
            }
        }
        return d.m2269a();
    }

    /* renamed from: a */
    private static void m2320a(Map<String, String> map, Map<String, String> map2) {
        for (String a : map2.keySet()) {
            zoToeBNOjF.m2321a((Map) map, (Map) map2, a);
        }
        zoToeBNOjF.m2321a((Map) map, (Map) map2, "en");
    }

    /* renamed from: a */
    private static void m2321a(Map<String, String> map, Map<String, String> map2, String str) {
        if (ztWNWCuZiM.m1521a((String) map.get(str))) {
            String str2 = (String) map2.get(str);
            if (!ztWNWCuZiM.m1521a(str2)) {
                map.put(str, str2);
            }
        }
    }

    /* renamed from: a */
    public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
        return zoToeBNOjF.m2319a(zoToeBNOjF.m2319a(this.f2365a, this.f2366b, false), (pdwpUtZXDT) obj, true);
    }
}
