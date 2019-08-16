package com.appsflyer;

/* renamed from: com.appsflyer.n */
final class C0452n {

    /* renamed from: ˊ */
    private String f308;

    /* renamed from: ˋ */
    private C0453b f309;

    /* renamed from: ˏ */
    private boolean f310;

    /* renamed from: com.appsflyer.n$b */
    enum C0453b {
        GOOGLE(0),
        AMAZON(1);
        

        /* renamed from: ˎ */
        private int f314;

        private C0453b(int i) {
            this.f314 = i;
        }

        public final String toString() {
            return String.valueOf(this.f314);
        }
    }

    C0452n(C0453b bVar, String str, boolean z) {
        this.f309 = bVar;
        this.f308 = str;
        this.f310 = z;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˋ */
    public final String mo6598() {
        return this.f308;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final boolean mo6597() {
        return this.f310;
    }

    public final String toString() {
        return String.format("%s,%s", new Object[]{this.f308, Boolean.valueOf(this.f310)});
    }
}
