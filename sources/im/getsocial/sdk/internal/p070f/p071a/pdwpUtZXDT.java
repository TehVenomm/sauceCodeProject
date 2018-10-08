package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.pdwpUtZXDT */
public final class pdwpUtZXDT {
    /* renamed from: a */
    public Integer f1827a;
    /* renamed from: b */
    public String f1828b;
    /* renamed from: c */
    public String f1829c;
    /* renamed from: d */
    public String f1830d;
    /* renamed from: e */
    public Boolean f1831e;
    /* renamed from: f */
    public List<String> f1832f;

    /* renamed from: a */
    public static void m1880a(zoToeBNOjF zotoebnojf, pdwpUtZXDT pdwputzxdt) {
        if (pdwputzxdt.f1827a != null) {
            zotoebnojf.mo4320a(1, (byte) 8);
            zotoebnojf.mo4319a(pdwputzxdt.f1827a.intValue());
        }
        if (pdwputzxdt.f1828b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(pdwputzxdt.f1828b);
        }
        if (pdwputzxdt.f1829c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(pdwputzxdt.f1829c);
        }
        if (pdwputzxdt.f1830d != null) {
            zotoebnojf.mo4320a(4, (byte) 11);
            zotoebnojf.mo4322a(pdwputzxdt.f1830d);
        }
        if (pdwputzxdt.f1831e != null) {
            zotoebnojf.mo4320a(5, (byte) 2);
            zotoebnojf.mo4324a(pdwputzxdt.f1831e.booleanValue());
        }
        if (pdwputzxdt.f1832f != null) {
            zotoebnojf.mo4320a(6, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, pdwputzxdt.f1832f.size());
            for (String a : pdwputzxdt.f1832f) {
                zotoebnojf.mo4322a(a);
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
        if (!(obj instanceof pdwpUtZXDT)) {
            return false;
        }
        pdwpUtZXDT pdwputzxdt = (pdwpUtZXDT) obj;
        if ((this.f1827a == pdwputzxdt.f1827a || (this.f1827a != null && this.f1827a.equals(pdwputzxdt.f1827a))) && ((this.f1828b == pdwputzxdt.f1828b || (this.f1828b != null && this.f1828b.equals(pdwputzxdt.f1828b))) && ((this.f1829c == pdwputzxdt.f1829c || (this.f1829c != null && this.f1829c.equals(pdwputzxdt.f1829c))) && ((this.f1830d == pdwputzxdt.f1830d || (this.f1830d != null && this.f1830d.equals(pdwputzxdt.f1830d))) && (this.f1831e == pdwputzxdt.f1831e || (this.f1831e != null && this.f1831e.equals(pdwputzxdt.f1831e))))))) {
            if (this.f1832f == pdwputzxdt.f1832f) {
                return true;
            }
            if (this.f1832f != null && this.f1832f.equals(pdwputzxdt.f1832f)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1827a == null ? 0 : this.f1827a.hashCode();
        int hashCode2 = this.f1828b == null ? 0 : this.f1828b.hashCode();
        int hashCode3 = this.f1829c == null ? 0 : this.f1829c.hashCode();
        int hashCode4 = this.f1830d == null ? 0 : this.f1830d.hashCode();
        int hashCode5 = this.f1831e == null ? 0 : this.f1831e.hashCode();
        if (this.f1832f != null) {
            i = this.f1832f.hashCode();
        }
        return (((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THActivitiesQuery{limit=" + this.f1827a + ", beforeId=" + this.f1828b + ", afterId=" + this.f1829c + ", userId=" + this.f1830d + ", friendsFeed=" + this.f1831e + ", tags=" + this.f1832f + "}";
    }
}
