package im.getsocial.sdk.usermanagement.p138a.p140b;

import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.usermanagement.a.b.qZypgoeblR */
public final class qZypgoeblR implements upgqDBbsrL<pdwpUtZXDT, pdwpUtZXDT> {
    private qZypgoeblR() {
    }

    /* renamed from: a */
    public static qZypgoeblR m3689a() {
        return new qZypgoeblR();
    }

    /* renamed from: a */
    private static void m3690a(String str, int i) {
        if (str != null) {
            jjbQypPegg.m1512a(str.length() <= i, "Avatar URL length should be less or equal " + i + ":" + str);
        }
    }

    /* renamed from: a */
    private static void m3691a(Map<String, String> map, String str) {
        for (Entry entry : map.entrySet()) {
            Object obj = (String) entry.getKey();
            Object obj2 = (String) entry.getValue();
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj), str + " can not have null key: " + obj + "=" + obj2);
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj2), str + " can not have null value: " + obj + "=" + obj2);
            jjbQypPegg.m1512a(obj.length() <= 64, str + " key length should be less or equal 64" + ":" + obj);
            jjbQypPegg.m1512a(obj2.length() <= 1024, str + " value length should be less or equal 1024" + ":" + obj2);
        }
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        pdwpUtZXDT pdwputzxdt = (pdwpUtZXDT) obj;
        qZypgoeblR.m3691a(pdwputzxdt.m3665d(), "Public Property");
        qZypgoeblR.m3691a(pdwputzxdt.m3666e(), "Private Property");
        qZypgoeblR.m3691a(pdwputzxdt.m3667f(), "Internal Public Property");
        qZypgoeblR.m3691a(pdwputzxdt.m3668g(), "internal Private Property");
        qZypgoeblR.m3690a(pdwputzxdt.m3661b(), 2048);
        qZypgoeblR.m3690a(pdwputzxdt.m3658a(), 32);
        return pdwputzxdt;
    }
}
