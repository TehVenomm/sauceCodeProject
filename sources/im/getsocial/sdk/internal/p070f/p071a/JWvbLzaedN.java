package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.JWvbLzaedN */
public final class JWvbLzaedN {
    /* renamed from: a */
    public String f1599a;
    /* renamed from: b */
    public Integer f1600b;
    /* renamed from: c */
    public Integer f1601c;
    /* renamed from: d */
    public Boolean f1602d;
    /* renamed from: e */
    public Integer f1603e;
    /* renamed from: f */
    public Map<String, String> f1604f;
    /* renamed from: g */
    public String f1605g;
    /* renamed from: h */
    public String f1606h;
    /* renamed from: i */
    public ofLJAxfaCe f1607i;

    /* renamed from: a */
    public static JWvbLzaedN m1690a(zoToeBNOjF zotoebnojf) {
        JWvbLzaedN jWvbLzaedN = new JWvbLzaedN();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1599a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1600b = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1601c = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1602d = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1603e = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        pdwpUtZXDT d = zotoebnojf.mo4327d();
                        Map hashMap = new HashMap(d.f1053c);
                        for (int i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        jWvbLzaedN.f1604f = hashMap;
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1605g = zotoebnojf.mo4336m();
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1606h = zotoebnojf.mo4336m();
                        break;
                    case (short) 9:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jWvbLzaedN.f1607i = ofLJAxfaCe.m1879a(zotoebnojf);
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return jWvbLzaedN;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof JWvbLzaedN)) {
            return false;
        }
        JWvbLzaedN jWvbLzaedN = (JWvbLzaedN) obj;
        if ((this.f1599a == jWvbLzaedN.f1599a || (this.f1599a != null && this.f1599a.equals(jWvbLzaedN.f1599a))) && ((this.f1600b == jWvbLzaedN.f1600b || (this.f1600b != null && this.f1600b.equals(jWvbLzaedN.f1600b))) && ((this.f1601c == jWvbLzaedN.f1601c || (this.f1601c != null && this.f1601c.equals(jWvbLzaedN.f1601c))) && ((this.f1602d == jWvbLzaedN.f1602d || (this.f1602d != null && this.f1602d.equals(jWvbLzaedN.f1602d))) && ((this.f1603e == jWvbLzaedN.f1603e || (this.f1603e != null && this.f1603e.equals(jWvbLzaedN.f1603e))) && ((this.f1604f == jWvbLzaedN.f1604f || (this.f1604f != null && this.f1604f.equals(jWvbLzaedN.f1604f))) && ((this.f1605g == jWvbLzaedN.f1605g || (this.f1605g != null && this.f1605g.equals(jWvbLzaedN.f1605g))) && (this.f1606h == jWvbLzaedN.f1606h || (this.f1606h != null && this.f1606h.equals(jWvbLzaedN.f1606h)))))))))) {
            if (this.f1607i == jWvbLzaedN.f1607i) {
                return true;
            }
            if (this.f1607i != null && this.f1607i.equals(jWvbLzaedN.f1607i)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1599a == null ? 0 : this.f1599a.hashCode();
        int hashCode2 = this.f1600b == null ? 0 : this.f1600b.hashCode();
        int hashCode3 = this.f1601c == null ? 0 : this.f1601c.hashCode();
        int hashCode4 = this.f1602d == null ? 0 : this.f1602d.hashCode();
        int hashCode5 = this.f1603e == null ? 0 : this.f1603e.hashCode();
        int hashCode6 = this.f1604f == null ? 0 : this.f1604f.hashCode();
        int hashCode7 = this.f1605g == null ? 0 : this.f1605g.hashCode();
        int hashCode8 = this.f1606h == null ? 0 : this.f1606h.hashCode();
        if (this.f1607i != null) {
            i = this.f1607i.hashCode();
        }
        return (((((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ hashCode8) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THNotification{id=" + this.f1599a + ", createdAt=" + this.f1600b + ", type=" + this.f1601c + ", isRead=" + this.f1602d + ", actionType=" + this.f1603e + ", actionsArgs=" + this.f1604f + ", text=" + this.f1605g + ", title=" + this.f1606h + ", origin=" + this.f1607i + "}";
    }
}
