package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.f.a.UwIeQkAzJH */
public final class UwIeQkAzJH {
    /* renamed from: a */
    public String f1640a;
    /* renamed from: b */
    public String f1641b;
    /* renamed from: c */
    public iFpupLCESp f1642c;
    /* renamed from: d */
    public Boolean f1643d;

    /* renamed from: a */
    public static void m1699a(zoToeBNOjF zotoebnojf, UwIeQkAzJH uwIeQkAzJH) {
        if (uwIeQkAzJH.f1640a != null) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(uwIeQkAzJH.f1640a);
        }
        if (uwIeQkAzJH.f1641b != null) {
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(uwIeQkAzJH.f1641b);
        }
        if (uwIeQkAzJH.f1642c != null) {
            zotoebnojf.mo4320a(3, (byte) 8);
            zotoebnojf.mo4319a(uwIeQkAzJH.f1642c.value);
        }
        if (uwIeQkAzJH.f1643d != null) {
            zotoebnojf.mo4320a(4, (byte) 2);
            zotoebnojf.mo4324a(uwIeQkAzJH.f1643d.booleanValue());
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
        if (!(obj instanceof UwIeQkAzJH)) {
            return false;
        }
        UwIeQkAzJH uwIeQkAzJH = (UwIeQkAzJH) obj;
        if ((this.f1640a == uwIeQkAzJH.f1640a || (this.f1640a != null && this.f1640a.equals(uwIeQkAzJH.f1640a))) && ((this.f1641b == uwIeQkAzJH.f1641b || (this.f1641b != null && this.f1641b.equals(uwIeQkAzJH.f1641b))) && (this.f1642c == uwIeQkAzJH.f1642c || (this.f1642c != null && this.f1642c.equals(uwIeQkAzJH.f1642c))))) {
            if (this.f1643d == uwIeQkAzJH.f1643d) {
                return true;
            }
            if (this.f1643d != null && this.f1643d.equals(uwIeQkAzJH.f1643d)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1640a == null ? 0 : this.f1640a.hashCode();
        int hashCode2 = this.f1641b == null ? 0 : this.f1641b.hashCode();
        int hashCode3 = this.f1642c == null ? 0 : this.f1642c.hashCode();
        if (this.f1643d != null) {
            i = this.f1643d.hashCode();
        }
        return (((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THPushTarget{token=" + this.f1640a + ", language=" + this.f1641b + ", platformId=" + this.f1642c + ", iosSandbox=" + this.f1643d + "}";
    }
}
