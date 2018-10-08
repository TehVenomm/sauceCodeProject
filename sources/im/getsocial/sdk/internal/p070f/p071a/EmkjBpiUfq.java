package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.f.a.EmkjBpiUfq */
public final class EmkjBpiUfq {
    /* renamed from: a */
    public String f1582a;
    /* renamed from: b */
    public Map<String, String> f1583b;

    /* renamed from: a */
    public static void m1687a(zoToeBNOjF zotoebnojf, EmkjBpiUfq emkjBpiUfq) {
        if (emkjBpiUfq.f1582a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(emkjBpiUfq.f1582a);
        }
        if (emkjBpiUfq.f1583b != null) {
            zotoebnojf.mo4320a(3, (byte) 13);
            zotoebnojf.mo4317a((byte) 11, (byte) 11, emkjBpiUfq.f1583b.size());
            for (Entry entry : emkjBpiUfq.f1583b.entrySet()) {
                String str = (String) entry.getKey();
                String str2 = (String) entry.getValue();
                zotoebnojf.mo4322a(str);
                zotoebnojf.mo4322a(str2);
            }
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
        if (!(obj instanceof EmkjBpiUfq)) {
            return false;
        }
        EmkjBpiUfq emkjBpiUfq = (EmkjBpiUfq) obj;
        if (this.f1582a == emkjBpiUfq.f1582a || (this.f1582a != null && this.f1582a.equals(emkjBpiUfq.f1582a))) {
            if (this.f1583b == emkjBpiUfq.f1583b) {
                return true;
            }
            if (this.f1583b != null && this.f1583b.equals(emkjBpiUfq.f1583b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1582a == null ? 0 : this.f1582a.hashCode();
        if (this.f1583b != null) {
            i = this.f1583b.hashCode();
        }
        return ((((hashCode ^ 16777619) * -2128831035) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THCreateTokenRequest{providerId=" + this.f1582a + ", type=" + null + ", linkParams=" + this.f1583b + "}";
    }
}
