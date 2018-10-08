package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.f.a.CJZnJxRuoc */
public final class CJZnJxRuoc {
    /* renamed from: a */
    public IbawHMWljm f1568a;
    /* renamed from: b */
    public Boolean f1569b;
    /* renamed from: c */
    public Map<qZypgoeblR, Map<HptYHntaqF, String>> f1570c;
    /* renamed from: d */
    public Map<String, String> f1571d;

    /* renamed from: a */
    public static void m1683a(zoToeBNOjF zotoebnojf, CJZnJxRuoc cJZnJxRuoc) {
        if (cJZnJxRuoc.f1568a != null) {
            zotoebnojf.mo4320a(1, (byte) 12);
            IbawHMWljm.m1688a(zotoebnojf, cJZnJxRuoc.f1568a);
        }
        if (cJZnJxRuoc.f1569b != null) {
            zotoebnojf.mo4320a(4, (byte) 2);
            zotoebnojf.mo4324a(cJZnJxRuoc.f1569b.booleanValue());
        }
        if (cJZnJxRuoc.f1570c != null) {
            zotoebnojf.mo4320a(6, (byte) 13);
            zotoebnojf.mo4317a((byte) 8, (byte) 13, cJZnJxRuoc.f1570c.size());
            for (Entry entry : cJZnJxRuoc.f1570c.entrySet()) {
                qZypgoeblR qzypgoeblr = (qZypgoeblR) entry.getKey();
                Map map = (Map) entry.getValue();
                zotoebnojf.mo4319a(qzypgoeblr.value);
                zotoebnojf.mo4317a((byte) 8, (byte) 11, map.size());
                for (Entry entry2 : map.entrySet()) {
                    HptYHntaqF hptYHntaqF = (HptYHntaqF) entry2.getKey();
                    String str = (String) entry2.getValue();
                    zotoebnojf.mo4319a(hptYHntaqF.value);
                    zotoebnojf.mo4322a(str);
                }
            }
        }
        if (cJZnJxRuoc.f1571d != null) {
            zotoebnojf.mo4320a(7, (byte) 13);
            zotoebnojf.mo4317a((byte) 11, (byte) 11, cJZnJxRuoc.f1571d.size());
            for (Entry entry22 : cJZnJxRuoc.f1571d.entrySet()) {
                String str2 = (String) entry22.getKey();
                str = (String) entry22.getValue();
                zotoebnojf.mo4322a(str2);
                zotoebnojf.mo4322a(str);
            }
        }
        zotoebnojf.mo4316a();
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof CJZnJxRuoc)) {
            return false;
        }
        CJZnJxRuoc cJZnJxRuoc = (CJZnJxRuoc) obj;
        if ((this.f1568a == cJZnJxRuoc.f1568a || (this.f1568a != null && this.f1568a.equals(cJZnJxRuoc.f1568a))) && ((this.f1569b == cJZnJxRuoc.f1569b || (this.f1569b != null && this.f1569b.equals(cJZnJxRuoc.f1569b))) && (this.f1570c == cJZnJxRuoc.f1570c || (this.f1570c != null && this.f1570c.equals(cJZnJxRuoc.f1570c))))) {
            if (this.f1571d == cJZnJxRuoc.f1571d) {
                return true;
            }
            if (this.f1571d != null && this.f1571d.equals(cJZnJxRuoc.f1571d)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1568a == null ? 0 : this.f1568a.hashCode();
        int hashCode2 = this.f1569b == null ? 0 : this.f1569b.hashCode();
        int hashCode3 = this.f1570c == null ? 0 : this.f1570c.hashCode();
        if (this.f1571d != null) {
            i = this.f1571d.hashCode();
        }
        return ((((((((((hashCode ^ 16777619) * -2128831035) * -2128831035) * -2128831035) ^ hashCode2) * -2128831035) * -2128831035) ^ hashCode3) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THProcessAppOpenRequest{fingerprint=" + this.f1568a + ", referrer=" + null + ", deepLinkUrl=" + null + ", isNewInstall=" + this.f1569b + ", referrers=" + null + ", referrerData=" + this.f1570c + ", deviceInfo=" + this.f1571d + "}";
    }
}
