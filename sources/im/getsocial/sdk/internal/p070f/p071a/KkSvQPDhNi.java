package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.KkSvQPDhNi */
public final class KkSvQPDhNi extends Exception {
    /* renamed from: a */
    public List<sqEuGXwfLT> f1625a;

    /* renamed from: a */
    public static KkSvQPDhNi m1694a(zoToeBNOjF zotoebnojf) {
        KkSvQPDhNi kkSvQPDhNi = new KkSvQPDhNi();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (int i = 0; i < e.f1045b; i++) {
                            arrayList.add(sqEuGXwfLT.m1884a(zotoebnojf));
                        }
                        kkSvQPDhNi.f1625a = arrayList;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return kkSvQPDhNi;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof KkSvQPDhNi)) {
            return false;
        }
        KkSvQPDhNi kkSvQPDhNi = (KkSvQPDhNi) obj;
        return this.f1625a != kkSvQPDhNi.f1625a ? this.f1625a != null && this.f1625a.equals(kkSvQPDhNi.f1625a) : true;
    }

    public final int hashCode() {
        return ((this.f1625a == null ? 0 : this.f1625a.hashCode()) ^ 16777619) * -2128831035;
    }

    public final String toString() {
        return "THErrors{errors=" + this.f1625a + "}";
    }
}
