package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.zoToeBNOjF */
public final class zoToeBNOjF {
    /* renamed from: a */
    public String f1867a;
    /* renamed from: b */
    public String f1868b;
    /* renamed from: c */
    public String f1869c;
    /* renamed from: d */
    public String f1870d;
    /* renamed from: e */
    public String f1871e;
    /* renamed from: f */
    public String f1872f;

    /* renamed from: a */
    public static void m1888a(im.getsocial.p018b.p021c.p022a.zoToeBNOjF zotoebnojf, zoToeBNOjF zotoebnojf2) {
        if (zotoebnojf2.f1867a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1867a);
        }
        if (zotoebnojf2.f1868b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1868b);
        }
        if (zotoebnojf2.f1869c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1869c);
        }
        if (zotoebnojf2.f1870d != null) {
            zotoebnojf.mo4320a(4, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1870d);
        }
        if (zotoebnojf2.f1871e != null) {
            zotoebnojf.mo4320a(5, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1871e);
        }
        if (zotoebnojf2.f1872f != null) {
            zotoebnojf.mo4320a(6, (byte) 11);
            zotoebnojf.mo4322a(zotoebnojf2.f1872f);
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
        if (!(obj instanceof zoToeBNOjF)) {
            return false;
        }
        zoToeBNOjF zotoebnojf = (zoToeBNOjF) obj;
        if ((this.f1867a == zotoebnojf.f1867a || (this.f1867a != null && this.f1867a.equals(zotoebnojf.f1867a))) && ((this.f1868b == zotoebnojf.f1868b || (this.f1868b != null && this.f1868b.equals(zotoebnojf.f1868b))) && ((this.f1869c == zotoebnojf.f1869c || (this.f1869c != null && this.f1869c.equals(zotoebnojf.f1869c))) && ((this.f1870d == zotoebnojf.f1870d || (this.f1870d != null && this.f1870d.equals(zotoebnojf.f1870d))) && (this.f1871e == zotoebnojf.f1871e || (this.f1871e != null && this.f1871e.equals(zotoebnojf.f1871e))))))) {
            if (this.f1872f == zotoebnojf.f1872f) {
                return true;
            }
            if (this.f1872f != null && this.f1872f.equals(zotoebnojf.f1872f)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1867a == null ? 0 : this.f1867a.hashCode();
        int hashCode2 = this.f1868b == null ? 0 : this.f1868b.hashCode();
        int hashCode3 = this.f1869c == null ? 0 : this.f1869c.hashCode();
        int hashCode4 = this.f1870d == null ? 0 : this.f1870d.hashCode();
        int hashCode5 = this.f1871e == null ? 0 : this.f1871e.hashCode();
        if (this.f1872f != null) {
            i = this.f1872f.hashCode();
        }
        return (((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THActivityPostContent{text=" + this.f1867a + ", imageUrl=" + this.f1868b + ", buttonTitle=" + this.f1869c + ", buttonAction=" + this.f1870d + ", language=" + this.f1871e + ", videoUrl=" + this.f1872f + "}";
    }
}
