package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.f.a.VuXsWfriFX */
public final class VuXsWfriFX {
    /* renamed from: a */
    public Integer f1644a;
    /* renamed from: b */
    public String f1645b;

    /* renamed from: a */
    public static void m1700a(zoToeBNOjF zotoebnojf, VuXsWfriFX vuXsWfriFX) {
        if (vuXsWfriFX.f1644a != null) {
            zotoebnojf.mo4320a(1, (byte) 8);
            zotoebnojf.mo4319a(vuXsWfriFX.f1644a.intValue());
        }
        if (vuXsWfriFX.f1645b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(vuXsWfriFX.f1645b);
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
        if (!(obj instanceof VuXsWfriFX)) {
            return false;
        }
        VuXsWfriFX vuXsWfriFX = (VuXsWfriFX) obj;
        if (this.f1644a == vuXsWfriFX.f1644a || (this.f1644a != null && this.f1644a.equals(vuXsWfriFX.f1644a))) {
            if (this.f1645b == vuXsWfriFX.f1645b) {
                return true;
            }
            if (this.f1645b != null && this.f1645b.equals(vuXsWfriFX.f1645b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1644a == null ? 0 : this.f1644a.hashCode();
        if (this.f1645b != null) {
            i = this.f1645b.hashCode();
        }
        return (((hashCode ^ 16777619) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THUsersQuery{limit=" + this.f1644a + ", name=" + this.f1645b + "}";
    }
}
