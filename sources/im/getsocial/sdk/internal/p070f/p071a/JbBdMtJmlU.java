package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.JbBdMtJmlU */
public final class JbBdMtJmlU {
    /* renamed from: a */
    public QWVUXapsSm f1608a;
    /* renamed from: b */
    public List<wWemqSpYTx> f1609b;

    /* renamed from: a */
    public static JbBdMtJmlU m1691a(zoToeBNOjF zotoebnojf) {
        JbBdMtJmlU jbBdMtJmlU = new JbBdMtJmlU();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jbBdMtJmlU.f1608a = QWVUXapsSm.m1697a(zotoebnojf);
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (int i = 0; i < e.f1045b; i++) {
                            arrayList.add(wWemqSpYTx.m1885a(zotoebnojf));
                        }
                        jbBdMtJmlU.f1609b = arrayList;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return jbBdMtJmlU;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof JbBdMtJmlU)) {
            return false;
        }
        JbBdMtJmlU jbBdMtJmlU = (JbBdMtJmlU) obj;
        if (this.f1608a == jbBdMtJmlU.f1608a || (this.f1608a != null && this.f1608a.equals(jbBdMtJmlU.f1608a))) {
            if (this.f1609b == jbBdMtJmlU.f1609b) {
                return true;
            }
            if (this.f1609b != null && this.f1609b.equals(jbBdMtJmlU.f1609b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1608a == null ? 0 : this.f1608a.hashCode();
        if (this.f1609b != null) {
            i = this.f1609b.hashCode();
        }
        return (((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THInviteProviders{defaultInviteContent=" + this.f1608a + ", providers=" + this.f1609b + "}";
    }
}
