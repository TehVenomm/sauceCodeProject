package im.getsocial.sdk.pushnotifications.p067a.p103b;

import im.getsocial.p015a.p016a.p017a.cjrhisSQCL;
import im.getsocial.p015a.p016a.pdwpUtZXDT;

/* renamed from: im.getsocial.sdk.pushnotifications.a.b.jjbQypPegg */
public abstract class jjbQypPegg {
    /* renamed from: a */
    private static pdwpUtZXDT m2412a(pdwpUtZXDT pdwputzxdt) {
        if (pdwputzxdt.containsKey("gs_data")) {
            return (pdwpUtZXDT) pdwputzxdt.get("gs_data");
        }
        for (Object next : pdwputzxdt.values()) {
            if (next instanceof pdwpUtZXDT) {
                pdwpUtZXDT a = jjbQypPegg.m2412a((pdwpUtZXDT) next);
                if (a != null) {
                    return a;
                }
            }
        }
        return null;
    }

    /* renamed from: a */
    private static jjbQypPegg m2413a(pdwpUtZXDT pdwputzxdt, String str, String str2) {
        Object obj = pdwputzxdt.get("s");
        obj = (!(obj instanceof Number) || ((Number) obj).intValue() == 0) ? null : 1;
        return obj != null ? new pdwpUtZXDT() : new XdbacJlTDQ(pdwputzxdt, str, str2);
    }

    /* renamed from: a */
    public static jjbQypPegg m2414a(String str) {
        return jjbQypPegg.m2413a((pdwpUtZXDT) new cjrhisSQCL().m721a(str, null), null, null);
    }

    /* renamed from: b */
    public static jjbQypPegg m2415b(String str) {
        pdwpUtZXDT a = jjbQypPegg.m2412a((pdwpUtZXDT) new cjrhisSQCL().m721a(str, null));
        if (a != null) {
            return jjbQypPegg.m2413a(a, null, null);
        }
        throw new im.getsocial.p015a.p016a.p017a.pdwpUtZXDT(2);
    }

    /* renamed from: a */
    public abstract boolean mo4576a();
}
