package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.f.a.KdkQzTlDzz */
public final class KdkQzTlDzz {
    /* renamed from: a */
    public String f1620a;
    /* renamed from: b */
    public String f1621b;
    /* renamed from: c */
    public String f1622c;
    /* renamed from: d */
    public icjTFWWVFN f1623d;
    /* renamed from: e */
    public String f1624e;

    /* renamed from: a */
    public static void m1693a(zoToeBNOjF zotoebnojf, KdkQzTlDzz kdkQzTlDzz) {
        if (kdkQzTlDzz.f1620a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(kdkQzTlDzz.f1620a);
        }
        if (kdkQzTlDzz.f1621b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(kdkQzTlDzz.f1621b);
        }
        if (kdkQzTlDzz.f1622c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(kdkQzTlDzz.f1622c);
        }
        if (kdkQzTlDzz.f1623d != null) {
            zotoebnojf.mo4320a(4, (byte) 12);
            icjTFWWVFN.m1876a(zotoebnojf, kdkQzTlDzz.f1623d);
        }
        if (kdkQzTlDzz.f1624e != null) {
            zotoebnojf.mo4320a(5, (byte) 11);
            zotoebnojf.mo4322a(kdkQzTlDzz.f1624e);
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
        if (!(obj instanceof KdkQzTlDzz)) {
            return false;
        }
        KdkQzTlDzz kdkQzTlDzz = (KdkQzTlDzz) obj;
        if ((this.f1620a == kdkQzTlDzz.f1620a || (this.f1620a != null && this.f1620a.equals(kdkQzTlDzz.f1620a))) && ((this.f1621b == kdkQzTlDzz.f1621b || (this.f1621b != null && this.f1621b.equals(kdkQzTlDzz.f1621b))) && ((this.f1622c == kdkQzTlDzz.f1622c || (this.f1622c != null && this.f1622c.equals(kdkQzTlDzz.f1622c))) && (this.f1623d == kdkQzTlDzz.f1623d || (this.f1623d != null && this.f1623d.equals(kdkQzTlDzz.f1623d)))))) {
            if (this.f1624e == kdkQzTlDzz.f1624e) {
                return true;
            }
            if (this.f1624e != null && this.f1624e.equals(kdkQzTlDzz.f1624e)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1620a == null ? 0 : this.f1620a.hashCode();
        int hashCode2 = this.f1621b == null ? 0 : this.f1621b.hashCode();
        int hashCode3 = this.f1622c == null ? 0 : this.f1622c.hashCode();
        int hashCode4 = this.f1623d == null ? 0 : this.f1623d.hashCode();
        if (this.f1624e != null) {
            i = this.f1624e.hashCode();
        }
        return (((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THSdkAuthRequest{appId=" + this.f1620a + ", userId=" + this.f1621b + ", password=" + this.f1622c + ", sessionProperties=" + this.f1623d + ", appSignatureFingerprint=" + this.f1624e + "}";
    }
}
