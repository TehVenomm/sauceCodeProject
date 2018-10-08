package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import im.getsocial.p018b.p021c.ztWNWCuZiM;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.f.a.sqEuGXwfLT */
public final class sqEuGXwfLT {
    /* renamed from: a */
    public rFvvVpjzZH f1838a;
    /* renamed from: b */
    public String f1839b;
    /* renamed from: c */
    public Map<String, String> f1840c;

    /* renamed from: a */
    public static sqEuGXwfLT m1884a(zoToeBNOjF zotoebnojf) {
        sqEuGXwfLT sqeugxwflt = new sqEuGXwfLT();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                int j;
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        j = zotoebnojf.mo4333j();
                        rFvvVpjzZH findByValue = rFvvVpjzZH.findByValue(j);
                        if (findByValue != null) {
                            sqeugxwflt.f1838a = findByValue;
                            break;
                        }
                        throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.PROTOCOL_ERROR, "Unexpected value for enum-type THErrorCode: " + j);
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        sqeugxwflt.f1839b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 13) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        pdwpUtZXDT d = zotoebnojf.mo4327d();
                        Map hashMap = new HashMap(d.f1053c);
                        for (j = 0; j < d.f1053c; j++) {
                            hashMap.put(zotoebnojf.mo4336m(), zotoebnojf.mo4336m());
                        }
                        sqeugxwflt.f1840c = hashMap;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return sqeugxwflt;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof sqEuGXwfLT)) {
            return false;
        }
        sqEuGXwfLT sqeugxwflt = (sqEuGXwfLT) obj;
        if ((this.f1838a == sqeugxwflt.f1838a || (this.f1838a != null && this.f1838a.equals(sqeugxwflt.f1838a))) && (this.f1839b == sqeugxwflt.f1839b || (this.f1839b != null && this.f1839b.equals(sqeugxwflt.f1839b)))) {
            if (this.f1840c == sqeugxwflt.f1840c) {
                return true;
            }
            if (this.f1840c != null && this.f1840c.equals(sqeugxwflt.f1840c)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1838a == null ? 0 : this.f1838a.hashCode();
        int hashCode2 = this.f1839b == null ? 0 : this.f1839b.hashCode();
        if (this.f1840c != null) {
            i = this.f1840c.hashCode();
        }
        return (((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THError{errorCode=" + this.f1838a + ", errorMsg=" + this.f1839b + ", context=" + this.f1840c + "}";
    }
}
