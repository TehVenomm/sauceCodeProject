package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.QWVUXapsSm */
public final class QWVUXapsSm {
    /* renamed from: a */
    public String f1631a;
    /* renamed from: b */
    public Map<String, String> f1632b;
    /* renamed from: c */
    public Map<String, String> f1633c;
    /* renamed from: d */
    public String f1634d;
    /* renamed from: e */
    public String f1635e;

    /* renamed from: a */
    public static QWVUXapsSm m1697a(zoToeBNOjF zotoebnojf) {
        QWVUXapsSm qWVUXapsSm = new QWVUXapsSm();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                pdwpUtZXDT d;
                Map hashMap;
                int i;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qWVUXapsSm.f1631a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        d = zotoebnojf.mo4327d();
                        hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        qWVUXapsSm.f1632b = hashMap;
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        d = zotoebnojf.mo4327d();
                        hashMap = new HashMap(d.f1053c);
                        for (i = 0; i < d.f1053c; i++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        qWVUXapsSm.f1633c = hashMap;
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qWVUXapsSm.f1634d = zotoebnojf.mo4336m();
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        qWVUXapsSm.f1635e = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return qWVUXapsSm;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof QWVUXapsSm)) {
            return false;
        }
        QWVUXapsSm qWVUXapsSm = (QWVUXapsSm) obj;
        if ((this.f1631a == qWVUXapsSm.f1631a || (this.f1631a != null && this.f1631a.equals(qWVUXapsSm.f1631a))) && ((this.f1632b == qWVUXapsSm.f1632b || (this.f1632b != null && this.f1632b.equals(qWVUXapsSm.f1632b))) && ((this.f1633c == qWVUXapsSm.f1633c || (this.f1633c != null && this.f1633c.equals(qWVUXapsSm.f1633c))) && (this.f1634d == qWVUXapsSm.f1634d || (this.f1634d != null && this.f1634d.equals(qWVUXapsSm.f1634d)))))) {
            if (this.f1635e == qWVUXapsSm.f1635e) {
                return true;
            }
            if (this.f1635e != null && this.f1635e.equals(qWVUXapsSm.f1635e)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1631a == null ? 0 : this.f1631a.hashCode();
        int hashCode2 = this.f1632b == null ? 0 : this.f1632b.hashCode();
        int hashCode3 = this.f1633c == null ? 0 : this.f1633c.hashCode();
        int hashCode4 = this.f1634d == null ? 0 : this.f1634d.hashCode();
        if (this.f1635e != null) {
            i = this.f1635e.hashCode();
        }
        return (((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THInviteContent{imageUrl=" + this.f1631a + ", subject=" + this.f1632b + ", text=" + this.f1633c + ", gifUrl=" + this.f1634d + ", videoUrl=" + this.f1635e + "}";
    }
}
