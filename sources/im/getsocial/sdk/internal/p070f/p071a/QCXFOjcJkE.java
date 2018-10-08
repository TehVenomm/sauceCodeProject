package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.QCXFOjcJkE */
public final class QCXFOjcJkE {
    /* renamed from: a */
    public String f1629a;
    /* renamed from: b */
    public String f1630b;

    /* renamed from: a */
    public static QCXFOjcJkE m1696a(zoToeBNOjF zotoebnojf) {
        QCXFOjcJkE qCXFOjcJkE = new QCXFOjcJkE();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qCXFOjcJkE.f1629a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qCXFOjcJkE.f1630b = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return qCXFOjcJkE;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof QCXFOjcJkE)) {
            return false;
        }
        QCXFOjcJkE qCXFOjcJkE = (QCXFOjcJkE) obj;
        if (this.f1629a == qCXFOjcJkE.f1629a || (this.f1629a != null && this.f1629a.equals(qCXFOjcJkE.f1629a))) {
            if (this.f1630b == qCXFOjcJkE.f1630b) {
                return true;
            }
            if (this.f1630b != null && this.f1630b.equals(qCXFOjcJkE.f1630b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1629a == null ? 0 : this.f1629a.hashCode();
        if (this.f1630b != null) {
            i = this.f1630b.hashCode();
        }
        return (((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THCreateTokenResponse{url=" + this.f1629a + ", token=" + this.f1630b + "}";
    }
}
