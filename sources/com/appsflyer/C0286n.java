package com.appsflyer;

/* renamed from: com.appsflyer.n */
final class C0286n {
    /* renamed from: ˊ */
    private String f291;
    /* renamed from: ˋ */
    private C0285b f292;
    /* renamed from: ˏ */
    private boolean f293;

    /* renamed from: com.appsflyer.n$b */
    enum C0285b {
        GOOGLE(0),
        AMAZON(1);
        
        /* renamed from: ˎ */
        private int f290;

        private C0285b(int i) {
            this.f290 = i;
        }

        public final String toString() {
            return String.valueOf(this.f290);
        }
    }

    C0286n(C0285b c0285b, String str, boolean z) {
        this.f292 = c0285b;
        this.f291 = str;
        this.f293 = z;
    }

    /* renamed from: ˋ */
    final String m332() {
        return this.f291;
    }

    /* renamed from: ˊ */
    final boolean m331() {
        return this.f293;
    }

    public final String toString() {
        return String.format("%s,%s", new Object[]{this.f291, Boolean.valueOf(this.f293)});
    }
}
