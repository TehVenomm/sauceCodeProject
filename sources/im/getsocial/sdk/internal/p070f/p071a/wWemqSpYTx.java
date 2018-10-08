package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import im.getsocial.p018b.p021c.ztWNWCuZiM;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.wWemqSpYTx */
public final class wWemqSpYTx {
    /* renamed from: a */
    public Map<String, String> f1841a;
    /* renamed from: b */
    public String f1842b;
    /* renamed from: c */
    public iFpupLCESp f1843c;
    /* renamed from: d */
    public Integer f1844d;
    /* renamed from: e */
    public String f1845e;
    /* renamed from: f */
    public Boolean f1846f;
    /* renamed from: g */
    public Boolean f1847g;
    /* renamed from: h */
    public Boolean f1848h;
    /* renamed from: i */
    public Boolean f1849i;
    /* renamed from: j */
    public KCGqEGAizh f1850j;
    /* renamed from: k */
    public QWVUXapsSm f1851k;
    /* renamed from: l */
    public Boolean f1852l;
    /* renamed from: m */
    public Boolean f1853m;

    /* renamed from: a */
    public static wWemqSpYTx m1885a(zoToeBNOjF zotoebnojf) {
        wWemqSpYTx wwemqspytx = new wWemqSpYTx();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                int i;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        pdwpUtZXDT d = zotoebnojf.mo4327d();
                        Map hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        wwemqspytx.f1841a = hashMap;
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1842b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        i = zotoebnojf.mo4333j();
                        iFpupLCESp findByValue = iFpupLCESp.findByValue(i);
                        if (findByValue != null) {
                            wwemqspytx.f1843c = findByValue;
                            break;
                        }
                        throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.PROTOCOL_ERROR, "Unexpected value for enum-type THDeviceOs: " + i);
                    case (short) 4:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1844d = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1845e = zotoebnojf.mo4336m();
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1846f = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1847g = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1848h = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 9:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1849i = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 10:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1850j = KCGqEGAizh.m1692a(zotoebnojf);
                        break;
                    case (short) 11:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1851k = QWVUXapsSm.m1697a(zotoebnojf);
                        break;
                    case (short) 12:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1852l = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 13:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        wwemqspytx.f1853m = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return wwemqspytx;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof wWemqSpYTx)) {
            return false;
        }
        wWemqSpYTx wwemqspytx = (wWemqSpYTx) obj;
        if ((this.f1841a == wwemqspytx.f1841a || (this.f1841a != null && this.f1841a.equals(wwemqspytx.f1841a))) && ((this.f1842b == wwemqspytx.f1842b || (this.f1842b != null && this.f1842b.equals(wwemqspytx.f1842b))) && ((this.f1843c == wwemqspytx.f1843c || (this.f1843c != null && this.f1843c.equals(wwemqspytx.f1843c))) && ((this.f1844d == wwemqspytx.f1844d || (this.f1844d != null && this.f1844d.equals(wwemqspytx.f1844d))) && ((this.f1845e == wwemqspytx.f1845e || (this.f1845e != null && this.f1845e.equals(wwemqspytx.f1845e))) && ((this.f1846f == wwemqspytx.f1846f || (this.f1846f != null && this.f1846f.equals(wwemqspytx.f1846f))) && ((this.f1847g == wwemqspytx.f1847g || (this.f1847g != null && this.f1847g.equals(wwemqspytx.f1847g))) && ((this.f1848h == wwemqspytx.f1848h || (this.f1848h != null && this.f1848h.equals(wwemqspytx.f1848h))) && ((this.f1849i == wwemqspytx.f1849i || (this.f1849i != null && this.f1849i.equals(wwemqspytx.f1849i))) && ((this.f1850j == wwemqspytx.f1850j || (this.f1850j != null && this.f1850j.equals(wwemqspytx.f1850j))) && ((this.f1851k == wwemqspytx.f1851k || (this.f1851k != null && this.f1851k.equals(wwemqspytx.f1851k))) && (this.f1852l == wwemqspytx.f1852l || (this.f1852l != null && this.f1852l.equals(wwemqspytx.f1852l)))))))))))))) {
            if (this.f1853m == wwemqspytx.f1853m) {
                return true;
            }
            if (this.f1853m != null && this.f1853m.equals(wwemqspytx.f1853m)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1841a == null ? 0 : this.f1841a.hashCode();
        int hashCode2 = this.f1842b == null ? 0 : this.f1842b.hashCode();
        int hashCode3 = this.f1843c == null ? 0 : this.f1843c.hashCode();
        int hashCode4 = this.f1844d == null ? 0 : this.f1844d.hashCode();
        int hashCode5 = this.f1845e == null ? 0 : this.f1845e.hashCode();
        int hashCode6 = this.f1846f == null ? 0 : this.f1846f.hashCode();
        int hashCode7 = this.f1847g == null ? 0 : this.f1847g.hashCode();
        int hashCode8 = this.f1848h == null ? 0 : this.f1848h.hashCode();
        int hashCode9 = this.f1849i == null ? 0 : this.f1849i.hashCode();
        int hashCode10 = this.f1850j == null ? 0 : this.f1850j.hashCode();
        int hashCode11 = this.f1851k == null ? 0 : this.f1851k.hashCode();
        int hashCode12 = this.f1852l == null ? 0 : this.f1852l.hashCode();
        if (this.f1853m != null) {
            i = this.f1853m.hashCode();
        }
        return (((((((((((((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ hashCode8) * -2128831035) ^ hashCode9) * -2128831035) ^ hashCode10) * -2128831035) ^ hashCode11) * -2128831035) ^ hashCode12) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THInviteProvider{name=" + this.f1841a + ", providerId=" + this.f1842b + ", os=" + this.f1843c + ", displayOrder=" + this.f1844d + ", iconUrl=" + this.f1845e + ", enabled=" + this.f1846f + ", supportsInviteText=" + this.f1847g + ", supportsInviteImageUrl=" + this.f1848h + ", supportsInviteSubject=" + this.f1849i + ", properties=" + this.f1850j + ", inviteContent=" + this.f1851k + ", supportsGif=" + this.f1852l + ", supportsVideo=" + this.f1853m + "}";
    }
}
