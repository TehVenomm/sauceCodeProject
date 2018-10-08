package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import im.getsocial.p018b.p021c.ztWNWCuZiM;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.KCGqEGAizh */
public final class KCGqEGAizh {
    /* renamed from: a */
    public String f1610a;
    /* renamed from: b */
    public List<ruWsnwUPKh> f1611b;
    /* renamed from: c */
    public String f1612c;
    /* renamed from: d */
    public String f1613d;
    /* renamed from: e */
    public String f1614e;
    /* renamed from: f */
    public String f1615f;
    /* renamed from: g */
    public String f1616g;
    /* renamed from: h */
    public String f1617h;
    /* renamed from: i */
    public String f1618i;
    /* renamed from: j */
    public Map<String, String> f1619j;

    /* renamed from: a */
    public static KCGqEGAizh m1692a(zoToeBNOjF zotoebnojf) {
        KCGqEGAizh kCGqEGAizh = new KCGqEGAizh();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                int i;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1610a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (i = 0; i < e.f1045b; i++) {
                            int j = zotoebnojf.mo4333j();
                            ruWsnwUPKh findByValue = ruWsnwUPKh.findByValue(j);
                            if (findByValue == null) {
                                throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.PROTOCOL_ERROR, "Unexpected value for enum-type THAvailableField: " + j);
                            }
                            arrayList.add(findByValue);
                        }
                        kCGqEGAizh.f1611b = arrayList;
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1612c = zotoebnojf.mo4336m();
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1613d = zotoebnojf.mo4336m();
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1614e = zotoebnojf.mo4336m();
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1615f = zotoebnojf.mo4336m();
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1616g = zotoebnojf.mo4336m();
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1617h = zotoebnojf.mo4336m();
                        break;
                    case (short) 9:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        kCGqEGAizh.f1618i = zotoebnojf.mo4336m();
                        break;
                    case (short) 10:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        pdwpUtZXDT d = zotoebnojf.mo4327d();
                        Map hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        kCGqEGAizh.f1619j = hashMap;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return kCGqEGAizh;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof KCGqEGAizh)) {
            return false;
        }
        KCGqEGAizh kCGqEGAizh = (KCGqEGAizh) obj;
        if ((this.f1610a == kCGqEGAizh.f1610a || (this.f1610a != null && this.f1610a.equals(kCGqEGAizh.f1610a))) && ((this.f1611b == kCGqEGAizh.f1611b || (this.f1611b != null && this.f1611b.equals(kCGqEGAizh.f1611b))) && ((this.f1612c == kCGqEGAizh.f1612c || (this.f1612c != null && this.f1612c.equals(kCGqEGAizh.f1612c))) && ((this.f1613d == kCGqEGAizh.f1613d || (this.f1613d != null && this.f1613d.equals(kCGqEGAizh.f1613d))) && ((this.f1614e == kCGqEGAizh.f1614e || (this.f1614e != null && this.f1614e.equals(kCGqEGAizh.f1614e))) && ((this.f1615f == kCGqEGAizh.f1615f || (this.f1615f != null && this.f1615f.equals(kCGqEGAizh.f1615f))) && ((this.f1616g == kCGqEGAizh.f1616g || (this.f1616g != null && this.f1616g.equals(kCGqEGAizh.f1616g))) && ((this.f1617h == kCGqEGAizh.f1617h || (this.f1617h != null && this.f1617h.equals(kCGqEGAizh.f1617h))) && (this.f1618i == kCGqEGAizh.f1618i || (this.f1618i != null && this.f1618i.equals(kCGqEGAizh.f1618i))))))))))) {
            if (this.f1619j == kCGqEGAizh.f1619j) {
                return true;
            }
            if (this.f1619j != null && this.f1619j.equals(kCGqEGAizh.f1619j)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1610a == null ? 0 : this.f1610a.hashCode();
        int hashCode2 = this.f1611b == null ? 0 : this.f1611b.hashCode();
        int hashCode3 = this.f1612c == null ? 0 : this.f1612c.hashCode();
        int hashCode4 = this.f1613d == null ? 0 : this.f1613d.hashCode();
        int hashCode5 = this.f1614e == null ? 0 : this.f1614e.hashCode();
        int hashCode6 = this.f1615f == null ? 0 : this.f1615f.hashCode();
        int hashCode7 = this.f1616g == null ? 0 : this.f1616g.hashCode();
        int hashCode8 = this.f1617h == null ? 0 : this.f1617h.hashCode();
        int hashCode9 = this.f1618i == null ? 0 : this.f1618i.hashCode();
        if (this.f1619j != null) {
            i = this.f1619j.hashCode();
        }
        return (((((((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ hashCode8) * -2128831035) ^ hashCode9) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THInviteProperties{action=" + this.f1610a + ", availableFields=" + this.f1611b + ", contentType=" + this.f1612c + ", className=" + this.f1613d + ", data=" + this.f1614e + ", packageName=" + this.f1615f + ", uti=" + this.f1616g + ", urlScheme=" + this.f1617h + ", imageExtension=" + this.f1618i + ", annotations=" + this.f1619j + "}";
    }
}
