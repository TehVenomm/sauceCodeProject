package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.bpiSwUyLit */
public final class bpiSwUyLit {
    /* renamed from: a */
    public String f1669a;
    /* renamed from: b */
    public String f1670b;
    /* renamed from: c */
    public qdyNCsqjKt f1671c;
    /* renamed from: d */
    public String f1672d;

    /* renamed from: a */
    public static bpiSwUyLit m1704a(zoToeBNOjF zotoebnojf) {
        bpiSwUyLit bpiswuylit = new bpiSwUyLit();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpiswuylit.f1669a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpiswuylit.f1670b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpiswuylit.f1671c = qdyNCsqjKt.m1881a(zotoebnojf);
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpiswuylit.f1672d = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return bpiswuylit;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof bpiSwUyLit)) {
            return false;
        }
        bpiSwUyLit bpiswuylit = (bpiSwUyLit) obj;
        if ((this.f1669a == bpiswuylit.f1669a || (this.f1669a != null && this.f1669a.equals(bpiswuylit.f1669a))) && ((this.f1670b == bpiswuylit.f1670b || (this.f1670b != null && this.f1670b.equals(bpiswuylit.f1670b))) && (this.f1671c == bpiswuylit.f1671c || (this.f1671c != null && this.f1671c.equals(bpiswuylit.f1671c))))) {
            if (this.f1672d == bpiswuylit.f1672d) {
                return true;
            }
            if (this.f1672d != null && this.f1672d.equals(bpiswuylit.f1672d)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1669a == null ? 0 : this.f1669a.hashCode();
        int hashCode2 = this.f1670b == null ? 0 : this.f1670b.hashCode();
        int hashCode3 = this.f1671c == null ? 0 : this.f1671c.hashCode();
        if (this.f1672d != null) {
            i = this.f1672d.hashCode();
        }
        return (((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THContent{text=" + this.f1669a + ", imageUrl=" + this.f1670b + ", button=" + this.f1671c + ", videoUrl=" + this.f1672d + "}";
    }
}
