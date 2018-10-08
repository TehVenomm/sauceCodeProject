package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.CyDeXbQkhA */
public final class CyDeXbQkhA {
    /* renamed from: a */
    public String f1572a;
    /* renamed from: b */
    public String f1573b;
    /* renamed from: c */
    public String f1574c;

    /* renamed from: a */
    public static CyDeXbQkhA m1684a(zoToeBNOjF zotoebnojf) {
        CyDeXbQkhA cyDeXbQkhA = new CyDeXbQkhA();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cyDeXbQkhA.f1572a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cyDeXbQkhA.f1573b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cyDeXbQkhA.f1574c = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return cyDeXbQkhA;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof CyDeXbQkhA)) {
            return false;
        }
        CyDeXbQkhA cyDeXbQkhA = (CyDeXbQkhA) obj;
        if ((this.f1572a == cyDeXbQkhA.f1572a || (this.f1572a != null && this.f1572a.equals(cyDeXbQkhA.f1572a))) && (this.f1573b == cyDeXbQkhA.f1573b || (this.f1573b != null && this.f1573b.equals(cyDeXbQkhA.f1573b)))) {
            if (this.f1574c == cyDeXbQkhA.f1574c) {
                return true;
            }
            if (this.f1574c != null && this.f1574c.equals(cyDeXbQkhA.f1574c)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1572a == null ? 0 : this.f1572a.hashCode();
        int hashCode2 = this.f1573b == null ? 0 : this.f1573b.hashCode();
        if (this.f1574c != null) {
            i = this.f1574c.hashCode();
        }
        return (((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THUserReference{id=" + this.f1572a + ", displayName=" + this.f1573b + ", avatarUrl=" + this.f1574c + "}";
    }
}
