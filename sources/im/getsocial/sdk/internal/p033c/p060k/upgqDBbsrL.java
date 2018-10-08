package im.getsocial.sdk.internal.p033c.p060k;

import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.k.upgqDBbsrL */
public class upgqDBbsrL {
    /* renamed from: b */
    private static Map<jjbQypPegg, jjbQypPegg> f1467b;
    @XdbacJlTDQ
    /* renamed from: a */
    bpiSwUyLit f1468a;

    /* renamed from: im.getsocial.sdk.internal.c.k.upgqDBbsrL$jjbQypPegg */
    public enum jjbQypPegg {
        PRODUCTION(0),
        TESTING_SSL(1),
        TESTING(2);
        
        private final int _value;

        private jjbQypPegg(int i) {
            this._value = i;
        }

        public static jjbQypPegg findByValue(int i) {
            switch (i) {
                case 0:
                    return PRODUCTION;
                case 1:
                    return TESTING_SSL;
                case 2:
                    return TESTING;
                default:
                    return PRODUCTION;
            }
        }

        public final int getValue() {
            return this._value;
        }
    }

    public upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: b */
    private static Map<jjbQypPegg, jjbQypPegg> m1494b() {
        Map<jjbQypPegg, jjbQypPegg> map;
        synchronized (jjbQypPegg.class) {
            try {
                if (f1467b == null) {
                    Map hashMap = new HashMap();
                    hashMap.put(jjbQypPegg.PRODUCTION, new jjbQypPegg("hades.getsocial.im/sdk", true));
                    hashMap.put(jjbQypPegg.TESTING_SSL, new jjbQypPegg("hades.testing.getsocial.im/sdk", true));
                    hashMap.put(jjbQypPegg.TESTING, new jjbQypPegg("hades.testing.getsocial.im/sdk", false));
                    f1467b = Collections.unmodifiableMap(hashMap);
                }
                map = f1467b;
            } catch (Throwable th) {
                Class cls = jjbQypPegg.class;
            }
        }
        return map;
    }

    /* renamed from: a */
    public final jjbQypPegg m1495a() {
        Map b = upgqDBbsrL.m1494b();
        int i = 0;
        if (this.f1468a.mo4361a("hades_configuration")) {
            i = this.f1468a.mo4364d("hades_configuration");
        }
        return (jjbQypPegg) b.get(jjbQypPegg.findByValue(i));
    }
}
