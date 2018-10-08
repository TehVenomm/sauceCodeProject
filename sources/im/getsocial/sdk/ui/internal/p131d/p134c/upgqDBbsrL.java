package im.getsocial.sdk.ui.internal.p131d.p134c;

import im.getsocial.p015a.p016a.pdwpUtZXDT;

/* renamed from: im.getsocial.sdk.ui.internal.d.c.upgqDBbsrL */
public final class upgqDBbsrL {
    private upgqDBbsrL() {
    }

    /* renamed from: a */
    public static void m3223a(pdwpUtZXDT<String, Object> pdwputzxdt, pdwpUtZXDT<String, Object> pdwputzxdt2) {
        if (pdwputzxdt == null || pdwputzxdt2 == null) {
            throw new IllegalArgumentException("Arguments may not be null");
        }
        for (Object obj : (String[]) pdwputzxdt2.keySet().toArray(new String[0])) {
            Object obj2 = pdwputzxdt2.get(obj);
            if (obj2 instanceof im.getsocial.p015a.p016a.upgqDBbsrL) {
                throw new RuntimeException("Can not merge arrays");
            }
            if (pdwputzxdt.containsKey(obj)) {
                Object obj3 = pdwputzxdt.get(obj);
                if ((obj3 instanceof pdwpUtZXDT) && (obj2 instanceof pdwpUtZXDT)) {
                    upgqDBbsrL.m3223a((pdwpUtZXDT) obj3, (pdwpUtZXDT) obj2);
                } else {
                    pdwputzxdt.put(obj, obj2);
                }
            } else {
                pdwputzxdt.put(obj, obj2);
            }
        }
    }
}
