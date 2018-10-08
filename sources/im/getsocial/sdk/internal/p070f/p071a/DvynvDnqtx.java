package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.DvynvDnqtx */
public final class DvynvDnqtx {
    /* renamed from: a */
    public List<String> f1580a;
    /* renamed from: b */
    public Boolean f1581b;

    /* renamed from: a */
    public static void m1686a(zoToeBNOjF zotoebnojf, DvynvDnqtx dvynvDnqtx) {
        if (dvynvDnqtx.f1580a != null) {
            zotoebnojf.mo4320a(1, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, dvynvDnqtx.f1580a.size());
            for (String a : dvynvDnqtx.f1580a) {
                zotoebnojf.mo4322a(a);
            }
        }
        if (dvynvDnqtx.f1581b != null) {
            zotoebnojf.mo4320a(2, (byte) 2);
            zotoebnojf.mo4324a(dvynvDnqtx.f1581b.booleanValue());
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
        if (!(obj instanceof DvynvDnqtx)) {
            return false;
        }
        DvynvDnqtx dvynvDnqtx = (DvynvDnqtx) obj;
        if (this.f1580a == dvynvDnqtx.f1580a || (this.f1580a != null && this.f1580a.equals(dvynvDnqtx.f1580a))) {
            if (this.f1581b == dvynvDnqtx.f1581b) {
                return true;
            }
            if (this.f1581b != null && this.f1581b.equals(dvynvDnqtx.f1581b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1580a == null ? 0 : this.f1580a.hashCode();
        if (this.f1581b != null) {
            i = this.f1581b.hashCode();
        }
        return ((((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035) * -2128831035;
    }

    public final String toString() {
        return "THNotificationsSetStatusParams{ids=" + this.f1580a + ", isRead=" + this.f1581b + ", appId=" + null + "}";
    }
}
