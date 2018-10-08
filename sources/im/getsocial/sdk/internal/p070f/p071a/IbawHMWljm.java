package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.f.a.IbawHMWljm */
public final class IbawHMWljm {
    /* renamed from: a */
    public String f1584a;
    /* renamed from: b */
    public String f1585b;
    /* renamed from: c */
    public String f1586c;
    /* renamed from: d */
    public String f1587d;
    /* renamed from: e */
    public String f1588e;
    /* renamed from: f */
    public String f1589f;
    /* renamed from: g */
    public String f1590g;

    /* renamed from: a */
    public static void m1688a(zoToeBNOjF zotoebnojf, IbawHMWljm ibawHMWljm) {
        if (ibawHMWljm.f1584a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1584a);
        }
        if (ibawHMWljm.f1585b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1585b);
        }
        if (ibawHMWljm.f1586c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1586c);
        }
        if (ibawHMWljm.f1587d != null) {
            zotoebnojf.mo4320a(4, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1587d);
        }
        if (ibawHMWljm.f1588e != null) {
            zotoebnojf.mo4320a(5, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1588e);
        }
        if (ibawHMWljm.f1589f != null) {
            zotoebnojf.mo4320a(7, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1589f);
        }
        if (ibawHMWljm.f1590g != null) {
            zotoebnojf.mo4320a(9, (byte) 11);
            zotoebnojf.mo4322a(ibawHMWljm.f1590g);
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
        if (!(obj instanceof IbawHMWljm)) {
            return false;
        }
        IbawHMWljm ibawHMWljm = (IbawHMWljm) obj;
        if ((this.f1584a == ibawHMWljm.f1584a || (this.f1584a != null && this.f1584a.equals(ibawHMWljm.f1584a))) && ((this.f1585b == ibawHMWljm.f1585b || (this.f1585b != null && this.f1585b.equals(ibawHMWljm.f1585b))) && ((this.f1586c == ibawHMWljm.f1586c || (this.f1586c != null && this.f1586c.equals(ibawHMWljm.f1586c))) && ((this.f1587d == ibawHMWljm.f1587d || (this.f1587d != null && this.f1587d.equals(ibawHMWljm.f1587d))) && ((this.f1588e == ibawHMWljm.f1588e || (this.f1588e != null && this.f1588e.equals(ibawHMWljm.f1588e))) && (this.f1589f == ibawHMWljm.f1589f || (this.f1589f != null && this.f1589f.equals(ibawHMWljm.f1589f)))))))) {
            if (this.f1590g == ibawHMWljm.f1590g) {
                return true;
            }
            if (this.f1590g != null && this.f1590g.equals(ibawHMWljm.f1590g)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1584a == null ? 0 : this.f1584a.hashCode();
        int hashCode2 = this.f1585b == null ? 0 : this.f1585b.hashCode();
        int hashCode3 = this.f1586c == null ? 0 : this.f1586c.hashCode();
        int hashCode4 = this.f1587d == null ? 0 : this.f1587d.hashCode();
        int hashCode5 = this.f1588e == null ? 0 : this.f1588e.hashCode();
        int hashCode6 = this.f1589f == null ? 0 : this.f1589f.hashCode();
        if (this.f1590g != null) {
            i = this.f1590g.hashCode();
        }
        return (((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) * -2128831035) ^ hashCode6) * -2128831035) ^ i) * -2128831035) * -2128831035;
    }

    public final String toString() {
        return "THFingerprint{screenWidth=" + this.f1584a + ", screenHeight=" + this.f1585b + ", devicePixelRatio=" + this.f1586c + ", osName=" + this.f1587d + ", osVersion=" + this.f1588e + ", userAgent=" + null + ", deviceBrand=" + this.f1589f + ", deviceModel=" + this.f1590g + ", ip=" + null + "}";
    }
}
