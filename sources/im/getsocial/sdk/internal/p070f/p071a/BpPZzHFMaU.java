package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.f.a.BpPZzHFMaU */
public final class BpPZzHFMaU {
    /* renamed from: a */
    public YgeTlQwUNa f1566a;
    /* renamed from: b */
    public Integer f1567b;

    /* renamed from: a */
    public static BpPZzHFMaU m1682a(zoToeBNOjF zotoebnojf) {
        BpPZzHFMaU bpPZzHFMaU = new BpPZzHFMaU();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpPZzHFMaU.f1566a = YgeTlQwUNa.m1702a(zotoebnojf);
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        bpPZzHFMaU.f1567b = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return bpPZzHFMaU;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof BpPZzHFMaU)) {
            return false;
        }
        BpPZzHFMaU bpPZzHFMaU = (BpPZzHFMaU) obj;
        if (this.f1566a == bpPZzHFMaU.f1566a || (this.f1566a != null && this.f1566a.equals(bpPZzHFMaU.f1566a))) {
            if (this.f1567b == bpPZzHFMaU.f1567b) {
                return true;
            }
            if (this.f1567b != null && this.f1567b.equals(bpPZzHFMaU.f1567b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1566a == null ? 0 : this.f1566a.hashCode();
        if (this.f1567b != null) {
            i = this.f1567b.hashCode();
        }
        return (((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THSuggestedFriend{user=" + this.f1566a + ", mutualFriends=" + this.f1567b + "}";
    }
}
