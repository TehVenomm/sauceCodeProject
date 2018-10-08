package im.getsocial.sdk.ui.internal.p125h;

/* renamed from: im.getsocial.sdk.ui.internal.h.zoToeBNOjF */
final class zoToeBNOjF {
    /* renamed from: a */
    private static final Object f3003a = new Object();
    /* renamed from: b */
    private static jjbQypPegg f3004b;

    private zoToeBNOjF() {
    }

    /* renamed from: a */
    static void m3366a(jjbQypPegg jjbqyppegg) {
        synchronized (f3003a) {
            if (f3004b == jjbqyppegg) {
                f3004b = null;
            }
        }
    }

    /* renamed from: b */
    static void m3367b(jjbQypPegg jjbqyppegg) {
        synchronized (f3003a) {
            if (f3004b != null && f3004b.m3357c()) {
                f3004b.m3359e();
            }
            f3004b = jjbqyppegg;
            jjbqyppegg.m3356b();
        }
    }
}
