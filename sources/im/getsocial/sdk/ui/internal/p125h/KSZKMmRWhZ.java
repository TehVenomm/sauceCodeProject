package im.getsocial.sdk.ui.internal.p125h;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.internal.h.KSZKMmRWhZ */
public class KSZKMmRWhZ {
    /* renamed from: a */
    private final List<jjbQypPegg> f2963a = new ArrayList(2);
    /* renamed from: b */
    private final Object f2964b = new Object();
    /* renamed from: c */
    private int f2965c = 2;

    /* renamed from: im.getsocial.sdk.ui.internal.h.KSZKMmRWhZ$1 */
    class C11541 implements Comparator<jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ KSZKMmRWhZ f2960a;

        C11541(KSZKMmRWhZ kSZKMmRWhZ) {
            this.f2960a = kSZKMmRWhZ;
        }

        public /* bridge */ /* synthetic */ int compare(Object obj, Object obj2) {
            return ((jjbQypPegg) obj).f2961a - ((jjbQypPegg) obj2).f2961a;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.h.KSZKMmRWhZ$jjbQypPegg */
    private static final class jjbQypPegg {
        /* renamed from: a */
        final int f2961a;
        /* renamed from: b */
        final Runnable f2962b;

        jjbQypPegg(int i, Runnable runnable) {
            this.f2961a = i;
            this.f2962b = runnable;
        }
    }

    public KSZKMmRWhZ(int i) {
    }

    /* renamed from: b */
    private void m3296b() {
        int i = this.f2965c - 1;
        this.f2965c = i;
        if (i == 0) {
            Collections.sort(this.f2963a, new C11541(this));
            for (jjbQypPegg jjbqyppegg : this.f2963a) {
                jjbqyppegg.f2962b.run();
            }
            this.f2963a.clear();
        }
    }

    /* renamed from: a */
    public final void m3297a() {
        synchronized (this.f2964b) {
            m3296b();
        }
    }

    /* renamed from: a */
    public final void m3298a(int i, Runnable runnable) {
        synchronized (this.f2964b) {
            this.f2963a.add(new jjbQypPegg(i, runnable));
            m3296b();
        }
    }
}
