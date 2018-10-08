package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.f.a.ZWjsSaCmFq */
public final class ZWjsSaCmFq {
    /* renamed from: a */
    public Integer f1666a;
    /* renamed from: b */
    public String f1667b;
    /* renamed from: c */
    public String f1668c;

    /* renamed from: a */
    public static void m1703a(zoToeBNOjF zotoebnojf, ZWjsSaCmFq zWjsSaCmFq) {
        if (zWjsSaCmFq.f1666a != null) {
            zotoebnojf.mo4320a(1, (byte) 8);
            zotoebnojf.mo4319a(zWjsSaCmFq.f1666a.intValue());
        }
        if (zWjsSaCmFq.f1667b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(zWjsSaCmFq.f1667b);
        }
        if (zWjsSaCmFq.f1668c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(zWjsSaCmFq.f1668c);
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
        if (!(obj instanceof ZWjsSaCmFq)) {
            return false;
        }
        ZWjsSaCmFq zWjsSaCmFq = (ZWjsSaCmFq) obj;
        if ((this.f1666a == zWjsSaCmFq.f1666a || (this.f1666a != null && this.f1666a.equals(zWjsSaCmFq.f1666a))) && (this.f1667b == zWjsSaCmFq.f1667b || (this.f1667b != null && this.f1667b.equals(zWjsSaCmFq.f1667b)))) {
            if (this.f1668c == zWjsSaCmFq.f1668c) {
                return true;
            }
            if (this.f1668c != null && this.f1668c.equals(zWjsSaCmFq.f1668c)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1666a == null ? 0 : this.f1666a.hashCode();
        int hashCode2 = this.f1667b == null ? 0 : this.f1667b.hashCode();
        if (this.f1668c != null) {
            i = this.f1668c.hashCode();
        }
        return (((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THTagsQuery{limit=" + this.f1666a + ", name=" + this.f1667b + ", feedId=" + this.f1668c + "}";
    }
}
