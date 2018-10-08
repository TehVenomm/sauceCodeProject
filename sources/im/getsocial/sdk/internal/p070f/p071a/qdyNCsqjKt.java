package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.qdyNCsqjKt */
public final class qdyNCsqjKt {
    /* renamed from: a */
    public String f1833a;
    /* renamed from: b */
    public String f1834b;

    /* renamed from: a */
    public static qdyNCsqjKt m1881a(zoToeBNOjF zotoebnojf) {
        qdyNCsqjKt qdyncsqjkt = new qdyNCsqjKt();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qdyncsqjkt.f1833a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qdyncsqjkt.f1834b = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return qdyncsqjkt;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof qdyNCsqjKt)) {
            return false;
        }
        qdyNCsqjKt qdyncsqjkt = (qdyNCsqjKt) obj;
        if (this.f1833a == qdyncsqjkt.f1833a || (this.f1833a != null && this.f1833a.equals(qdyncsqjkt.f1833a))) {
            if (this.f1834b == qdyncsqjkt.f1834b) {
                return true;
            }
            if (this.f1834b != null && this.f1834b.equals(qdyncsqjkt.f1834b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1833a == null ? 0 : this.f1833a.hashCode();
        if (this.f1834b != null) {
            i = this.f1834b.hashCode();
        }
        return (((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THButton{buttonAction=" + this.f1833a + ", buttonTitle=" + this.f1834b + "}";
    }
}
