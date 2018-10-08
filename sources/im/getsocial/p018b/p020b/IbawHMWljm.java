package im.getsocial.p018b.p020b;

import java.io.InterruptedIOException;

/* renamed from: im.getsocial.b.b.IbawHMWljm */
public class IbawHMWljm {
    /* renamed from: c */
    public static final IbawHMWljm f979c = new C09121();

    /* renamed from: im.getsocial.b.b.IbawHMWljm$1 */
    static final class C09121 extends IbawHMWljm {
        C09121() {
        }

        /* renamed from: d */
        public final void mo4295d() {
        }
    }

    /* renamed from: a */
    public final void m748a(Object obj) {
        long j = 0;
        try {
            boolean b = mo4301b();
            long f_ = f_();
            if (b || f_ != 0) {
                long nanoTime = System.nanoTime();
                if (b && f_ != 0) {
                    f_ = Math.min(f_, g_() - nanoTime);
                } else if (b) {
                    f_ = g_() - nanoTime;
                }
                if (f_ > 0) {
                    j = f_ / 1000000;
                    obj.wait(j, (int) (f_ - (j * 1000000)));
                    j = System.nanoTime() - nanoTime;
                }
                if (j >= f_) {
                    throw new InterruptedIOException("timeout");
                }
                return;
            }
            obj.wait();
        } catch (InterruptedException e) {
            throw new InterruptedIOException("interrupted");
        }
    }

    /* renamed from: b */
    public boolean mo4301b() {
        return false;
    }

    /* renamed from: d */
    public void mo4295d() {
        if (Thread.interrupted()) {
            throw new InterruptedIOException("thread interrupted");
        }
    }

    public long f_() {
        return 0;
    }

    public long g_() {
        throw new IllegalStateException("No deadline");
    }
}
