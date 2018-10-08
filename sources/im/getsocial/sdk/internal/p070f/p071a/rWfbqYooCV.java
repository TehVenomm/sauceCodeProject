package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.rWfbqYooCV */
public final class rWfbqYooCV {
    /* renamed from: a */
    public String f1835a;
    /* renamed from: b */
    public String f1836b;
    /* renamed from: c */
    public String f1837c;

    /* renamed from: a */
    public static rWfbqYooCV m1882a(zoToeBNOjF zotoebnojf) {
        rWfbqYooCV rwfbqyoocv = new rWfbqYooCV();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        rwfbqyoocv.f1835a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        rwfbqyoocv.f1836b = zotoebnojf.mo4336m();
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        rwfbqyoocv.f1837c = zotoebnojf.mo4336m();
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return rwfbqyoocv;
        }
    }

    /* renamed from: a */
    public static void m1883a(zoToeBNOjF zotoebnojf, rWfbqYooCV rwfbqyoocv) {
        if (rwfbqyoocv.f1835a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(rwfbqyoocv.f1835a);
        }
        if (rwfbqyoocv.f1836b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(rwfbqyoocv.f1836b);
        }
        if (rwfbqyoocv.f1837c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(rwfbqyoocv.f1837c);
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
        if (!(obj instanceof rWfbqYooCV)) {
            return false;
        }
        rWfbqYooCV rwfbqyoocv = (rWfbqYooCV) obj;
        if ((this.f1835a == rwfbqyoocv.f1835a || (this.f1835a != null && this.f1835a.equals(rwfbqyoocv.f1835a))) && (this.f1836b == rwfbqyoocv.f1836b || (this.f1836b != null && this.f1836b.equals(rwfbqyoocv.f1836b)))) {
            if (this.f1837c == rwfbqyoocv.f1837c) {
                return true;
            }
            if (this.f1837c != null && this.f1837c.equals(rwfbqyoocv.f1837c)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1835a == null ? 0 : this.f1835a.hashCode();
        int hashCode2 = this.f1836b == null ? 0 : this.f1836b.hashCode();
        if (this.f1837c != null) {
            i = this.f1837c.hashCode();
        }
        return (((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THIdentity{provider=" + this.f1835a + ", providerId=" + this.f1836b + ", accessToken=" + this.f1837c + "}";
    }
}
