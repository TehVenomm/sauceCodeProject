package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.JQrJMKopAa */
public final class JQrJMKopAa {
    /* renamed from: a */
    public String f1591a;
    /* renamed from: b */
    public String f1592b;
    /* renamed from: c */
    public String f1593c;
    /* renamed from: d */
    public Boolean f1594d;
    /* renamed from: e */
    public Map<String, String> f1595e;
    /* renamed from: f */
    public Map<String, String> f1596f;
    /* renamed from: g */
    public Boolean f1597g;
    /* renamed from: h */
    public Map<String, String> f1598h;

    /* renamed from: a */
    public static JQrJMKopAa m1689a(zoToeBNOjF zotoebnojf) {
        JQrJMKopAa jQrJMKopAa = new JQrJMKopAa();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                pdwpUtZXDT d;
                Map hashMap;
                int i;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jQrJMKopAa.f1591a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jQrJMKopAa.f1592b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jQrJMKopAa.f1593c = zotoebnojf.mo4336m();
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jQrJMKopAa.f1594d = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        d = zotoebnojf.mo4327d();
                        hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        jQrJMKopAa.f1595e = hashMap;
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        d = zotoebnojf.mo4327d();
                        hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        jQrJMKopAa.f1596f = hashMap;
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jQrJMKopAa.f1597g = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        d = zotoebnojf.mo4327d();
                        hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        jQrJMKopAa.f1598h = hashMap;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return jQrJMKopAa;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof JQrJMKopAa)) {
            return false;
        }
        JQrJMKopAa jQrJMKopAa = (JQrJMKopAa) obj;
        if ((this.f1591a == jQrJMKopAa.f1591a || (this.f1591a != null && this.f1591a.equals(jQrJMKopAa.f1591a))) && ((this.f1592b == jQrJMKopAa.f1592b || (this.f1592b != null && this.f1592b.equals(jQrJMKopAa.f1592b))) && ((this.f1593c == jQrJMKopAa.f1593c || (this.f1593c != null && this.f1593c.equals(jQrJMKopAa.f1593c))) && ((this.f1594d == jQrJMKopAa.f1594d || (this.f1594d != null && this.f1594d.equals(jQrJMKopAa.f1594d))) && ((this.f1595e == jQrJMKopAa.f1595e || (this.f1595e != null && this.f1595e.equals(jQrJMKopAa.f1595e))) && ((this.f1596f == jQrJMKopAa.f1596f || (this.f1596f != null && this.f1596f.equals(jQrJMKopAa.f1596f))) && (this.f1597g == jQrJMKopAa.f1597g || (this.f1597g != null && this.f1597g.equals(jQrJMKopAa.f1597g))))))))) {
            if (this.f1598h == jQrJMKopAa.f1598h) {
                return true;
            }
            if (this.f1598h != null && this.f1598h.equals(jQrJMKopAa.f1598h)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1591a == null ? 0 : this.f1591a.hashCode();
        int hashCode2 = this.f1592b == null ? 0 : this.f1592b.hashCode();
        int hashCode3 = this.f1593c == null ? 0 : this.f1593c.hashCode();
        int hashCode4 = this.f1594d == null ? 0 : this.f1594d.hashCode();
        int hashCode5 = this.f1595e == null ? 0 : this.f1595e.hashCode();
        int hashCode6 = this.f1596f == null ? 0 : this.f1596f.hashCode();
        int hashCode7 = this.f1597g == null ? 0 : this.f1597g.hashCode();
        if (this.f1598h != null) {
            i = this.f1598h.hashCode();
        }
        return (((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THTokenInfo{referrerUserId=" + this.f1591a + ", token=" + this.f1592b + ", provider=" + this.f1593c + ", firstMatch=" + this.f1594d + ", linkParams=" + this.f1595e + ", internalData=" + this.f1596f + ", guaranteedMatch=" + this.f1597g + ", originalData=" + this.f1598h + "}";
    }
}
