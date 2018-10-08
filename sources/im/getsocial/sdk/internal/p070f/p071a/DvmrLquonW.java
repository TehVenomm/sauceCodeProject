package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.DvmrLquonW */
public final class DvmrLquonW {
    /* renamed from: a */
    public Integer f1575a;
    /* renamed from: b */
    public String f1576b;
    /* renamed from: c */
    public String f1577c;
    /* renamed from: d */
    public List<Integer> f1578d;
    /* renamed from: e */
    public Boolean f1579e;

    /* renamed from: a */
    public static void m1685a(zoToeBNOjF zotoebnojf, DvmrLquonW dvmrLquonW) {
        if (dvmrLquonW.f1575a != null) {
            zotoebnojf.mo4320a(1, (byte) 8);
            zotoebnojf.mo4319a(dvmrLquonW.f1575a.intValue());
        }
        if (dvmrLquonW.f1576b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(dvmrLquonW.f1576b);
        }
        if (dvmrLquonW.f1577c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(dvmrLquonW.f1577c);
        }
        if (dvmrLquonW.f1578d != null) {
            zotoebnojf.mo4320a(4, (byte) 15);
            zotoebnojf.mo4318a((byte) 8, dvmrLquonW.f1578d.size());
            for (Integer intValue : dvmrLquonW.f1578d) {
                zotoebnojf.mo4319a(intValue.intValue());
            }
        }
        if (dvmrLquonW.f1579e != null) {
            zotoebnojf.mo4320a(5, (byte) 2);
            zotoebnojf.mo4324a(dvmrLquonW.f1579e.booleanValue());
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
        if (!(obj instanceof DvmrLquonW)) {
            return false;
        }
        DvmrLquonW dvmrLquonW = (DvmrLquonW) obj;
        if ((this.f1575a == dvmrLquonW.f1575a || (this.f1575a != null && this.f1575a.equals(dvmrLquonW.f1575a))) && ((this.f1576b == dvmrLquonW.f1576b || (this.f1576b != null && this.f1576b.equals(dvmrLquonW.f1576b))) && ((this.f1577c == dvmrLquonW.f1577c || (this.f1577c != null && this.f1577c.equals(dvmrLquonW.f1577c))) && (this.f1578d == dvmrLquonW.f1578d || (this.f1578d != null && this.f1578d.equals(dvmrLquonW.f1578d)))))) {
            if (this.f1579e == dvmrLquonW.f1579e) {
                return true;
            }
            if (this.f1579e != null && this.f1579e.equals(dvmrLquonW.f1579e)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1575a == null ? 0 : this.f1575a.hashCode();
        int hashCode2 = this.f1576b == null ? 0 : this.f1576b.hashCode();
        int hashCode3 = this.f1577c == null ? 0 : this.f1577c.hashCode();
        int hashCode4 = this.f1578d == null ? 0 : this.f1578d.hashCode();
        if (this.f1579e != null) {
            i = this.f1579e.hashCode();
        }
        return ((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ i) * -2128831035) * -2128831035;
    }

    public final String toString() {
        return "THNotificationsQuery{limit=" + this.f1575a + ", newer=" + this.f1576b + ", older=" + this.f1577c + ", type=" + this.f1578d + ", isRead=" + this.f1579e + ", appId=" + null + "}";
    }
}
