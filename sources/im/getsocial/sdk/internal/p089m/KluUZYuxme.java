package im.getsocial.sdk.internal.p089m;

import im.getsocial.sdk.internal.p033c.SKUqohGtGQ;
import java.util.LinkedList;
import java.util.Queue;

/* renamed from: im.getsocial.sdk.internal.m.KluUZYuxme */
public final class KluUZYuxme implements SKUqohGtGQ {
    /* renamed from: a */
    private boolean f2212a;
    /* renamed from: b */
    private final Object f2213b = new Object();
    /* renamed from: c */
    private final SKUqohGtGQ f2214c;
    /* renamed from: d */
    private final Queue<Runnable> f2215d = new LinkedList();

    public KluUZYuxme(SKUqohGtGQ sKUqohGtGQ) {
        this.f2214c = sKUqohGtGQ;
    }

    /* renamed from: a */
    public final void m2109a() {
        synchronized (this.f2213b) {
            this.f2212a = true;
            for (Runnable a : this.f2215d) {
                this.f2214c.mo4358a(a);
            }
            this.f2215d.clear();
        }
    }

    /* renamed from: a */
    public final void mo4358a(Runnable runnable) {
        synchronized (this.f2213b) {
            if (this.f2212a) {
                this.f2214c.mo4358a(runnable);
            } else {
                this.f2215d.add(runnable);
            }
        }
    }
}
