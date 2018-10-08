package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.SKUqohGtGQ */
public final class SKUqohGtGQ {
    /* renamed from: a */
    public Integer f1636a;
    /* renamed from: b */
    public Integer f1637b;
    /* renamed from: c */
    public String f1638c;
    /* renamed from: d */
    public String f1639d;

    /* renamed from: a */
    public static SKUqohGtGQ m1698a(zoToeBNOjF zotoebnojf) {
        SKUqohGtGQ sKUqohGtGQ = new SKUqohGtGQ();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        sKUqohGtGQ.f1636a = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        sKUqohGtGQ.f1637b = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        sKUqohGtGQ.f1638c = zotoebnojf.mo4336m();
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        sKUqohGtGQ.f1639d = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return sKUqohGtGQ;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof SKUqohGtGQ)) {
            return false;
        }
        SKUqohGtGQ sKUqohGtGQ = (SKUqohGtGQ) obj;
        if ((this.f1636a == sKUqohGtGQ.f1636a || (this.f1636a != null && this.f1636a.equals(sKUqohGtGQ.f1636a))) && ((this.f1637b == sKUqohGtGQ.f1637b || (this.f1637b != null && this.f1637b.equals(sKUqohGtGQ.f1637b))) && (this.f1638c == sKUqohGtGQ.f1638c || (this.f1638c != null && this.f1638c.equals(sKUqohGtGQ.f1638c))))) {
            if (this.f1639d == sKUqohGtGQ.f1639d) {
                return true;
            }
            if (this.f1639d != null && this.f1639d.equals(sKUqohGtGQ.f1639d)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1636a == null ? 0 : this.f1636a.hashCode();
        int hashCode2 = this.f1637b == null ? 0 : this.f1637b.hashCode();
        int hashCode3 = this.f1638c == null ? 0 : this.f1638c.hashCode();
        if (this.f1639d != null) {
            i = this.f1639d.hashCode();
        }
        return (((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THMention{startIdx=" + this.f1636a + ", endIdx=" + this.f1637b + ", userId=" + this.f1638c + ", type=" + this.f1639d + "}";
    }
}
