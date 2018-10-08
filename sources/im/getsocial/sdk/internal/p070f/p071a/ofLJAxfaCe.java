package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.ofLJAxfaCe */
public final class ofLJAxfaCe {
    /* renamed from: a */
    public String f1821a;
    /* renamed from: b */
    public String f1822b;
    /* renamed from: c */
    public String f1823c;
    /* renamed from: d */
    public List<rWfbqYooCV> f1824d;
    /* renamed from: e */
    public Boolean f1825e;
    /* renamed from: f */
    public Boolean f1826f;

    /* renamed from: a */
    public static ofLJAxfaCe m1879a(zoToeBNOjF zotoebnojf) {
        ofLJAxfaCe ofljaxface = new ofLJAxfaCe();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ofljaxface.f1821a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ofljaxface.f1822b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ofljaxface.f1823c = zotoebnojf.mo4336m();
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (int i = 0; i < e.f1045b; i++) {
                            arrayList.add(rWfbqYooCV.m1882a(zotoebnojf));
                        }
                        ofljaxface.f1824d = arrayList;
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ofljaxface.f1825e = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        ofljaxface.f1826f = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return ofljaxface;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof ofLJAxfaCe)) {
            return false;
        }
        ofLJAxfaCe ofljaxface = (ofLJAxfaCe) obj;
        if ((this.f1821a == ofljaxface.f1821a || (this.f1821a != null && this.f1821a.equals(ofljaxface.f1821a))) && ((this.f1822b == ofljaxface.f1822b || (this.f1822b != null && this.f1822b.equals(ofljaxface.f1822b))) && ((this.f1823c == ofljaxface.f1823c || (this.f1823c != null && this.f1823c.equals(ofljaxface.f1823c))) && ((this.f1824d == ofljaxface.f1824d || (this.f1824d != null && this.f1824d.equals(ofljaxface.f1824d))) && (this.f1825e == ofljaxface.f1825e || (this.f1825e != null && this.f1825e.equals(ofljaxface.f1825e))))))) {
            if (this.f1826f == ofljaxface.f1826f) {
                return true;
            }
            if (this.f1826f != null && this.f1826f.equals(ofljaxface.f1826f)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1821a == null ? 0 : this.f1821a.hashCode();
        int hashCode2 = this.f1822b == null ? 0 : this.f1822b.hashCode();
        int hashCode3 = this.f1823c == null ? 0 : this.f1823c.hashCode();
        int hashCode4 = this.f1824d == null ? 0 : this.f1824d.hashCode();
        int hashCode5 = this.f1825e == null ? 0 : this.f1825e.hashCode();
        if (this.f1826f != null) {
            i = this.f1826f.hashCode();
        }
        return (((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THPostAuthor{id=" + this.f1821a + ", displayName=" + this.f1822b + ", avatarUrl=" + this.f1823c + ", identities=" + this.f1824d + ", verified=" + this.f1825e + ", isApp=" + this.f1826f + "}";
    }
}
