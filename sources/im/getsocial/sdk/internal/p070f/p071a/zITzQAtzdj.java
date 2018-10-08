package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.zITzQAtzdj */
public final class zITzQAtzdj {
    /* renamed from: a */
    public Long f1863a;
    /* renamed from: b */
    public Long f1864b;
    /* renamed from: c */
    public Long f1865c;
    /* renamed from: d */
    public Long f1866d;

    /* renamed from: a */
    public static zITzQAtzdj m1887a(zoToeBNOjF zotoebnojf) {
        zITzQAtzdj zitzqatzdj = new zITzQAtzdj();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 10) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        zitzqatzdj.f1863a = Long.valueOf(zotoebnojf.mo4334k());
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 10) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        zitzqatzdj.f1864b = Long.valueOf(zotoebnojf.mo4334k());
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 10) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        zitzqatzdj.f1865c = Long.valueOf(zotoebnojf.mo4334k());
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 10) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        zitzqatzdj.f1866d = Long.valueOf(zotoebnojf.mo4334k());
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return zitzqatzdj;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof zITzQAtzdj)) {
            return false;
        }
        zITzQAtzdj zitzqatzdj = (zITzQAtzdj) obj;
        if ((this.f1863a == zitzqatzdj.f1863a || (this.f1863a != null && this.f1863a.equals(zitzqatzdj.f1863a))) && ((this.f1864b == zitzqatzdj.f1864b || (this.f1864b != null && this.f1864b.equals(zitzqatzdj.f1864b))) && (this.f1865c == zitzqatzdj.f1865c || (this.f1865c != null && this.f1865c.equals(zitzqatzdj.f1865c))))) {
            if (this.f1866d == zitzqatzdj.f1866d) {
                return true;
            }
            if (this.f1866d != null && this.f1866d.equals(zitzqatzdj.f1866d)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1863a == null ? 0 : this.f1863a.hashCode();
        int hashCode2 = this.f1864b == null ? 0 : this.f1864b.hashCode();
        int hashCode3 = this.f1865c == null ? 0 : this.f1865c.hashCode();
        if (this.f1866d != null) {
            i = this.f1866d.hashCode();
        }
        return (((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THUploadChunkSize{wifi=" + this.f1863a + ", lte=" + this.f1864b + ", tg=" + this.f1865c + ", other=" + this.f1866d + "}";
    }
}
