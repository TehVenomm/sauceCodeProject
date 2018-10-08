package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.KluUZYuxme */
public final class KluUZYuxme {
    /* renamed from: a */
    public String f1626a;
    /* renamed from: b */
    public String f1627b;
    /* renamed from: c */
    public Map<String, String> f1628c;

    /* renamed from: a */
    public static KluUZYuxme m1695a(zoToeBNOjF zotoebnojf) {
        KluUZYuxme kluUZYuxme = new KluUZYuxme();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kluUZYuxme.f1626a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kluUZYuxme.f1627b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        pdwpUtZXDT d = zotoebnojf.mo4327d();
                        Map hashMap = new HashMap(d.f1053c);
                        for (int i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        kluUZYuxme.f1628c = hashMap;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return kluUZYuxme;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof KluUZYuxme)) {
            return false;
        }
        KluUZYuxme kluUZYuxme = (KluUZYuxme) obj;
        if ((this.f1626a == kluUZYuxme.f1626a || (this.f1626a != null && this.f1626a.equals(kluUZYuxme.f1626a))) && (this.f1627b == kluUZYuxme.f1627b || (this.f1627b != null && this.f1627b.equals(kluUZYuxme.f1627b)))) {
            if (this.f1628c == kluUZYuxme.f1628c) {
                return true;
            }
            if (this.f1628c != null && this.f1628c.equals(kluUZYuxme.f1628c)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1626a == null ? 0 : this.f1626a.hashCode();
        int hashCode2 = this.f1627b == null ? 0 : this.f1627b.hashCode();
        if (this.f1628c != null) {
            i = this.f1628c.hashCode();
        }
        return (((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THApplicationInfo{name=" + this.f1626a + ", iconUrl=" + this.f1627b + ", properties=" + this.f1628c + "}";
    }
}
