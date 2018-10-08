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

/* renamed from: im.getsocial.sdk.internal.f.a.YgeTlQwUNa */
public final class YgeTlQwUNa {
    /* renamed from: a */
    public String f1657a;
    /* renamed from: b */
    public String f1658b;
    /* renamed from: c */
    public String f1659c;
    /* renamed from: d */
    public List<rWfbqYooCV> f1660d;
    /* renamed from: e */
    public Map<String, String> f1661e;
    /* renamed from: f */
    public Map<String, String> f1662f;
    /* renamed from: g */
    public String f1663g;
    /* renamed from: h */
    public String f1664h;
    /* renamed from: i */
    public iFpupLCESp f1665i;

    /* renamed from: a */
    public static YgeTlQwUNa m1702a(zoToeBNOjF zotoebnojf) {
        YgeTlQwUNa ygeTlQwUNa = new YgeTlQwUNa();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                int i;
                pdwpUtZXDT d;
                Map hashMap;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ygeTlQwUNa.f1657a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ygeTlQwUNa.f1658b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ygeTlQwUNa.f1659c = zotoebnojf.mo4336m();
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (i = 0; i < e.f1045b; i++) {
                            arrayList.add(rWfbqYooCV.m1882a(zotoebnojf));
                        }
                        ygeTlQwUNa.f1660d = arrayList;
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
                        ygeTlQwUNa.f1661e = hashMap;
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
                        ygeTlQwUNa.f1662f = hashMap;
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ygeTlQwUNa.f1663g = zotoebnojf.mo4336m();
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ygeTlQwUNa.f1664h = zotoebnojf.mo4336m();
                        break;
                    case (short) 9:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        i = zotoebnojf.mo4333j();
                        iFpupLCESp findByValue = iFpupLCESp.findByValue(i);
                        if (findByValue != null) {
                            ygeTlQwUNa.f1665i = findByValue;
                            break;
                        }
                        throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.PROTOCOL_ERROR, "Unexpected value for enum-type THDeviceOs: " + i);
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return ygeTlQwUNa;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof YgeTlQwUNa)) {
            return false;
        }
        YgeTlQwUNa ygeTlQwUNa = (YgeTlQwUNa) obj;
        if ((this.f1657a == ygeTlQwUNa.f1657a || (this.f1657a != null && this.f1657a.equals(ygeTlQwUNa.f1657a))) && ((this.f1658b == ygeTlQwUNa.f1658b || (this.f1658b != null && this.f1658b.equals(ygeTlQwUNa.f1658b))) && ((this.f1659c == ygeTlQwUNa.f1659c || (this.f1659c != null && this.f1659c.equals(ygeTlQwUNa.f1659c))) && ((this.f1660d == ygeTlQwUNa.f1660d || (this.f1660d != null && this.f1660d.equals(ygeTlQwUNa.f1660d))) && ((this.f1661e == ygeTlQwUNa.f1661e || (this.f1661e != null && this.f1661e.equals(ygeTlQwUNa.f1661e))) && ((this.f1662f == ygeTlQwUNa.f1662f || (this.f1662f != null && this.f1662f.equals(ygeTlQwUNa.f1662f))) && ((this.f1663g == ygeTlQwUNa.f1663g || (this.f1663g != null && this.f1663g.equals(ygeTlQwUNa.f1663g))) && (this.f1664h == ygeTlQwUNa.f1664h || (this.f1664h != null && this.f1664h.equals(ygeTlQwUNa.f1664h)))))))))) {
            if (this.f1665i == ygeTlQwUNa.f1665i) {
                return true;
            }
            if (this.f1665i != null && this.f1665i.equals(ygeTlQwUNa.f1665i)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1657a == null ? 0 : this.f1657a.hashCode();
        int hashCode2 = this.f1658b == null ? 0 : this.f1658b.hashCode();
        int hashCode3 = this.f1659c == null ? 0 : this.f1659c.hashCode();
        int hashCode4 = this.f1660d == null ? 0 : this.f1660d.hashCode();
        int hashCode5 = this.f1661e == null ? 0 : this.f1661e.hashCode();
        int hashCode6 = this.f1662f == null ? 0 : this.f1662f.hashCode();
        int hashCode7 = this.f1663g == null ? 0 : this.f1663g.hashCode();
        int hashCode8 = this.f1664h == null ? 0 : this.f1664h.hashCode();
        if (this.f1665i != null) {
            i = this.f1665i.hashCode();
        }
        return (((((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ hashCode8) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THPublicUser{id=" + this.f1657a + ", displayName=" + this.f1658b + ", avatarUrl=" + this.f1659c + ", identities=" + this.f1660d + ", publicProperties=" + this.f1661e + ", internalPublicProperties=" + this.f1662f + ", installDate=" + this.f1663g + ", installProvider=" + this.f1664h + ", installPlatform=" + this.f1665i + "}";
    }
}
