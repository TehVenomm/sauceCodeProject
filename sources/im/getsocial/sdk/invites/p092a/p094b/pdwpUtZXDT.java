package im.getsocial.sdk.invites.p092a.p094b;

import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg;
import im.getsocial.sdk.invites.LocalizableText;
import im.getsocial.sdk.invites.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.invites.a.b.pdwpUtZXDT */
public class pdwpUtZXDT {
    /* renamed from: a */
    private final String f2314a;
    /* renamed from: b */
    private final byte[] f2315b;
    /* renamed from: c */
    private final LocalizableText f2316c;
    /* renamed from: d */
    private final LocalizableText f2317d;
    /* renamed from: e */
    private final String f2318e;
    /* renamed from: f */
    private final String f2319f;
    /* renamed from: g */
    private final byte[] f2320g;

    /* renamed from: im.getsocial.sdk.invites.a.b.pdwpUtZXDT$jjbQypPegg */
    public static class jjbQypPegg {
        /* renamed from: a */
        private byte[] f2307a;
        /* renamed from: b */
        private String f2308b;
        /* renamed from: c */
        private LocalizableText f2309c;
        /* renamed from: d */
        private LocalizableText f2310d;
        /* renamed from: e */
        private String f2311e;
        /* renamed from: f */
        private String f2312f;
        /* renamed from: g */
        private byte[] f2313g;

        jjbQypPegg() {
        }

        /* renamed from: a */
        public final jjbQypPegg m2266a(LocalizableText localizableText) {
            this.f2309c = localizableText;
            return this;
        }

        /* renamed from: a */
        public final jjbQypPegg m2267a(String str) {
            this.f2309c = cjrhisSQCL.m2404a(str);
            return this;
        }

        /* renamed from: a */
        public final jjbQypPegg m2268a(byte[] bArr) {
            if (bArr != null) {
                this.f2307a = (byte[]) bArr.clone();
            }
            return this;
        }

        /* renamed from: a */
        public final pdwpUtZXDT m2269a() {
            return new pdwpUtZXDT(this.f2309c, this.f2310d, this.f2308b, this.f2307a, this.f2311e, this.f2312f, this.f2313g);
        }

        /* renamed from: b */
        public final jjbQypPegg m2270b(LocalizableText localizableText) {
            this.f2310d = localizableText;
            return this;
        }

        /* renamed from: b */
        public final jjbQypPegg m2271b(String str) {
            this.f2310d = cjrhisSQCL.m2404a(str);
            return this;
        }

        /* renamed from: b */
        public final jjbQypPegg m2272b(byte[] bArr) {
            if (bArr != null) {
                this.f2313g = (byte[]) bArr.clone();
            }
            return this;
        }

        /* renamed from: c */
        public final jjbQypPegg m2273c(String str) {
            this.f2308b = str;
            return this;
        }

        /* renamed from: d */
        public final jjbQypPegg m2274d(String str) {
            this.f2311e = str;
            return this;
        }

        /* renamed from: e */
        public final jjbQypPegg m2275e(String str) {
            this.f2312f = str;
            return this;
        }
    }

    pdwpUtZXDT(LocalizableText localizableText, LocalizableText localizableText2, String str, byte[] bArr, String str2, String str3, byte[] bArr2) {
        this.f2314a = str;
        this.f2315b = bArr;
        this.f2316c = localizableText;
        this.f2317d = localizableText2;
        this.f2318e = str2;
        this.f2319f = str3;
        this.f2320g = bArr2;
    }

    /* renamed from: a */
    public static jjbQypPegg m2276a() {
        return new jjbQypPegg();
    }

    /* renamed from: b */
    public final boolean m2277b() {
        return (jjbQypPegg.m1517c(this.f2314a) && this.f2315b == null) ? false : true;
    }

    /* renamed from: c */
    public final boolean m2278c() {
        return "-".equals(this.f2314a);
    }

    /* renamed from: d */
    public final LocalizableText m2279d() {
        return this.f2316c;
    }

    /* renamed from: e */
    public final LocalizableText m2280e() {
        return this.f2317d;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            pdwpUtZXDT pdwputzxdt = (pdwpUtZXDT) obj;
            if (this.f2318e != null) {
                if (!this.f2318e.equals(pdwputzxdt.f2318e)) {
                    return false;
                }
            } else if (pdwputzxdt.f2318e != null) {
                return false;
            }
            if (this.f2319f != null) {
                if (!this.f2319f.equals(pdwputzxdt.f2319f)) {
                    return false;
                }
            } else if (pdwputzxdt.f2319f != null) {
                return false;
            }
            if (this.f2314a != null) {
                if (!this.f2314a.equals(pdwputzxdt.f2314a)) {
                    return false;
                }
            } else if (pdwputzxdt.f2314a != null) {
                return false;
            }
            if (this.f2316c != null) {
                if (!this.f2316c.equals(pdwputzxdt.f2316c)) {
                    return false;
                }
            } else if (pdwputzxdt.f2316c != null) {
                return false;
            }
            if (this.f2317d != null) {
                return this.f2317d.equals(pdwputzxdt.f2317d);
            }
            if (pdwputzxdt.f2317d != null) {
                return false;
            }
        }
        return true;
    }

    /* renamed from: f */
    public final String m2281f() {
        return this.f2314a;
    }

    /* renamed from: g */
    public final byte[] m2282g() {
        return this.f2315b == null ? null : (byte[]) this.f2315b.clone();
    }

    /* renamed from: h */
    public final String m2283h() {
        return this.f2318e;
    }

    public int hashCode() {
        int i = 0;
        int hashCode = this.f2314a != null ? this.f2314a.hashCode() : 0;
        int hashCode2 = this.f2316c != null ? this.f2316c.hashCode() : 0;
        int hashCode3 = this.f2317d != null ? this.f2317d.hashCode() : 0;
        int hashCode4 = this.f2318e != null ? this.f2318e.hashCode() : 0;
        if (this.f2319f != null) {
            i = this.f2319f.hashCode();
        }
        return (((((((hashCode * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    /* renamed from: i */
    public final String m2284i() {
        return this.f2319f;
    }

    /* renamed from: j */
    public final byte[] m2285j() {
        return this.f2320g == null ? null : (byte[]) this.f2320g.clone();
    }
}
