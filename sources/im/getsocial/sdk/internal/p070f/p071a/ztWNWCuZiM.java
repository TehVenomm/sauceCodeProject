package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.f.a.ztWNWCuZiM */
public final class ztWNWCuZiM {
    /* renamed from: a */
    public Map<String, String> f1873a;
    /* renamed from: b */
    public Long f1874b;
    /* renamed from: c */
    public String f1875c;
    /* renamed from: d */
    public String f1876d;
    /* renamed from: e */
    public Long f1877e;

    /* renamed from: a */
    public static void m1889a(zoToeBNOjF zotoebnojf, ztWNWCuZiM ztwnwcuzim) {
        if (ztwnwcuzim.f1873a != null) {
            zotoebnojf.mo4320a(1, (byte) 13);
            zotoebnojf.mo4317a((byte) 11, (byte) 11, ztwnwcuzim.f1873a.size());
            for (Entry entry : ztwnwcuzim.f1873a.entrySet()) {
                String str = (String) entry.getKey();
                String str2 = (String) entry.getValue();
                zotoebnojf.mo4322a(str);
                zotoebnojf.mo4322a(str2);
            }
        }
        if (ztwnwcuzim.f1874b != null) {
            zotoebnojf.mo4320a(2, (byte) 10);
            zotoebnojf.mo4321a(ztwnwcuzim.f1874b.longValue());
        }
        if (ztwnwcuzim.f1875c != null) {
            zotoebnojf.mo4320a(3, (byte) 11);
            zotoebnojf.mo4322a(ztwnwcuzim.f1875c);
        }
        if (ztwnwcuzim.f1876d != null) {
            zotoebnojf.mo4320a(4, (byte) 11);
            zotoebnojf.mo4322a(ztwnwcuzim.f1876d);
        }
        if (ztwnwcuzim.f1877e != null) {
            zotoebnojf.mo4320a(5, (byte) 10);
            zotoebnojf.mo4321a(ztwnwcuzim.f1877e.longValue());
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
        if (!(obj instanceof ztWNWCuZiM)) {
            return false;
        }
        ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
        if ((this.f1873a == ztwnwcuzim.f1873a || (this.f1873a != null && this.f1873a.equals(ztwnwcuzim.f1873a))) && ((this.f1874b == ztwnwcuzim.f1874b || (this.f1874b != null && this.f1874b.equals(ztwnwcuzim.f1874b))) && ((this.f1875c == ztwnwcuzim.f1875c || (this.f1875c != null && this.f1875c.equals(ztwnwcuzim.f1875c))) && (this.f1876d == ztwnwcuzim.f1876d || (this.f1876d != null && this.f1876d.equals(ztwnwcuzim.f1876d)))))) {
            if (this.f1877e == ztwnwcuzim.f1877e) {
                return true;
            }
            if (this.f1877e != null && this.f1877e.equals(ztwnwcuzim.f1877e)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1873a == null ? 0 : this.f1873a.hashCode();
        int hashCode2 = this.f1874b == null ? 0 : this.f1874b.hashCode();
        int hashCode3 = this.f1875c == null ? 0 : this.f1875c.hashCode();
        int hashCode4 = this.f1876d == null ? 0 : this.f1876d.hashCode();
        if (this.f1877e != null) {
            i = this.f1877e.hashCode();
        }
        return (((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THAnalyticsBaseEvent{customProperties=" + this.f1873a + ", deviceTime=" + this.f1874b + ", name=" + this.f1875c + ", id=" + this.f1876d + ", retryCount=" + this.f1877e + "}";
    }
}
