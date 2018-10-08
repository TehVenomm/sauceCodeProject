package im.getsocial.p018b.p020b;

import android.support.v4.media.session.PlaybackStateCompat;
import java.io.IOException;
import java.io.InterruptedIOException;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.b.b.jjbQypPegg */
public class jjbQypPegg extends IbawHMWljm {
    /* renamed from: a */
    static jjbQypPegg f1008a;
    /* renamed from: b */
    private static final long f1009b = TimeUnit.SECONDS.toMillis(60);
    /* renamed from: d */
    private static final long f1010d = TimeUnit.MILLISECONDS.toNanos(f1009b);
    /* renamed from: e */
    private boolean f1011e;
    /* renamed from: f */
    private jjbQypPegg f1012f;
    /* renamed from: g */
    private long f1013g;

    /* renamed from: im.getsocial.b.b.jjbQypPegg$1 */
    class C09161 implements rFvvVpjzZH {
        /* renamed from: a */
        final /* synthetic */ rFvvVpjzZH f1004a;
        /* renamed from: b */
        final /* synthetic */ jjbQypPegg f1005b;

        public final void a_(cjrhisSQCL cjrhissqcl, long j) {
            rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, j);
            long j2 = j;
            while (j2 > 0) {
                QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
                long j3 = 0;
                while (j3 < PlaybackStateCompat.ACTION_PREPARE_FROM_SEARCH) {
                    j3 += (long) (cjrhissqcl.f997a.f985c - cjrhissqcl.f997a.f984b);
                    if (j3 >= j2) {
                        j3 = j2;
                        break;
                    }
                }
                this.f1005b.m777a();
                try {
                    this.f1004a.a_(cjrhissqcl, j3);
                    j2 -= j3;
                    this.f1005b.m778a(true);
                } catch (IOException e) {
                    throw this.f1005b.m776a(e);
                } catch (Throwable th) {
                    this.f1005b.m778a(false);
                }
            }
        }

        public void close() {
            this.f1005b.m777a();
            try {
                this.f1004a.close();
                this.f1005b.m778a(true);
            } catch (IOException e) {
                throw this.f1005b.m776a(e);
            } catch (Throwable th) {
                this.f1005b.m778a(false);
            }
        }

        public void flush() {
            this.f1005b.m777a();
            try {
                this.f1004a.flush();
                this.f1005b.m778a(true);
            } catch (IOException e) {
                throw this.f1005b.m776a(e);
            } catch (Throwable th) {
                this.f1005b.m778a(false);
            }
        }

        public String toString() {
            return "AsyncTimeout.sink(" + this.f1004a + ")";
        }
    }

    /* renamed from: im.getsocial.b.b.jjbQypPegg$2 */
    class C09172 implements KkSvQPDhNi {
        /* renamed from: a */
        final /* synthetic */ KkSvQPDhNi f1006a;
        /* renamed from: b */
        final /* synthetic */ jjbQypPegg f1007b;

        /* renamed from: a */
        public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
            this.f1007b.m777a();
            try {
                long a = this.f1006a.mo4293a(cjrhissqcl, j);
                this.f1007b.m778a(true);
                return a;
            } catch (IOException e) {
                throw this.f1007b.m776a(e);
            } catch (Throwable th) {
                this.f1007b.m778a(false);
            }
        }

        public void close() {
            try {
                this.f1006a.close();
                this.f1007b.m778a(true);
            } catch (IOException e) {
                throw this.f1007b.m776a(e);
            } catch (Throwable th) {
                this.f1007b.m778a(false);
            }
        }

        public String toString() {
            return "AsyncTimeout.source(" + this.f1006a + ")";
        }
    }

    /* renamed from: im.getsocial.b.b.jjbQypPegg$jjbQypPegg */
    private static final class jjbQypPegg extends Thread {
        public jjbQypPegg() {
            super("Okio Watchdog");
            setDaemon(true);
        }

        /* JADX WARNING: inconsistent code. */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public final void run() {
            /*
            r2 = this;
        L_0x0000:
            r0 = im.getsocial.p018b.p020b.jjbQypPegg.class;
            monitor-enter(r0);	 Catch:{ InterruptedException -> 0x0012 }
            r0 = im.getsocial.p018b.p020b.jjbQypPegg.m774c();	 Catch:{ all -> 0x000d }
            if (r0 != 0) goto L_0x0014;
        L_0x0009:
            r0 = im.getsocial.p018b.p020b.jjbQypPegg.class;
            monitor-exit(r0);	 Catch:{ all -> 0x000d }
            goto L_0x0000;
        L_0x000d:
            r0 = move-exception;
            r1 = im.getsocial.p018b.p020b.jjbQypPegg.class;
            monitor-exit(r1);	 Catch:{ all -> 0x000d }
            throw r0;	 Catch:{ InterruptedException -> 0x0012 }
        L_0x0012:
            r0 = move-exception;
            goto L_0x0000;
        L_0x0014:
            r1 = im.getsocial.p018b.p020b.jjbQypPegg.f1008a;	 Catch:{ all -> 0x000d }
            if (r0 != r1) goto L_0x001f;
        L_0x0018:
            r0 = 0;
            im.getsocial.p018b.p020b.jjbQypPegg.f1008a = r0;	 Catch:{ all -> 0x000d }
            r0 = im.getsocial.p018b.p020b.jjbQypPegg.class;
            monitor-exit(r0);	 Catch:{ all -> 0x000d }
            return;
        L_0x001f:
            r1 = im.getsocial.p018b.p020b.jjbQypPegg.class;
            monitor-exit(r1);	 Catch:{ all -> 0x000d }
            r0.e_();	 Catch:{ InterruptedException -> 0x0012 }
            goto L_0x0000;
            */
            throw new UnsupportedOperationException("Method not decompiled: im.getsocial.b.b.jjbQypPegg.jjbQypPegg.run():void");
        }
    }

    /* renamed from: a */
    private static void m772a(jjbQypPegg jjbqyppegg, long j, boolean z) {
        synchronized (jjbQypPegg.class) {
            try {
                if (f1008a == null) {
                    f1008a = new jjbQypPegg();
                    new jjbQypPegg().start();
                }
                long nanoTime = System.nanoTime();
                if (j != 0 && z) {
                    jjbqyppegg.f1013g = Math.min(j, jjbqyppegg.g_() - nanoTime) + nanoTime;
                } else if (j != 0) {
                    jjbqyppegg.f1013g = nanoTime + j;
                } else if (z) {
                    jjbqyppegg.f1013g = jjbqyppegg.g_();
                } else {
                    throw new AssertionError();
                }
                long j2 = jjbqyppegg.f1013g;
                jjbQypPegg jjbqyppegg2 = f1008a;
                while (jjbqyppegg2.f1012f != null && j2 - nanoTime >= jjbqyppegg2.f1012f.f1013g - nanoTime) {
                    jjbqyppegg2 = jjbqyppegg2.f1012f;
                }
                jjbqyppegg.f1012f = jjbqyppegg2.f1012f;
                jjbqyppegg2.f1012f = jjbqyppegg;
                if (jjbqyppegg2 == f1008a) {
                    jjbqyppegg2.notify();
                }
            } finally {
                Class cls = jjbQypPegg.class;
            }
        }
    }

    /* renamed from: a */
    private static boolean m773a(jjbQypPegg jjbqyppegg) {
        boolean z;
        synchronized (jjbQypPegg.class) {
            try {
                for (jjbQypPegg jjbqyppegg2 = f1008a; jjbqyppegg2 != null; jjbqyppegg2 = jjbqyppegg2.f1012f) {
                    if (jjbqyppegg2.f1012f == jjbqyppegg) {
                        jjbqyppegg2.f1012f = jjbqyppegg.f1012f;
                        jjbqyppegg.f1012f = null;
                        z = false;
                        break;
                    }
                }
                z = true;
            } catch (Throwable th) {
                Class cls = jjbQypPegg.class;
            }
        }
        return z;
    }

    /* renamed from: c */
    static jjbQypPegg m774c() {
        jjbQypPegg jjbqyppegg = f1008a.f1012f;
        long nanoTime;
        if (jjbqyppegg == null) {
            nanoTime = System.nanoTime();
            jjbQypPegg.class.wait(f1009b);
            return (f1008a.f1012f != null || System.nanoTime() - nanoTime < f1010d) ? null : f1008a;
        } else {
            nanoTime = jjbqyppegg.f1013g - System.nanoTime();
            if (nanoTime > 0) {
                long j = nanoTime / 1000000;
                jjbQypPegg.class.wait(j, (int) (nanoTime - (1000000 * j)));
                return null;
            }
            f1008a.f1012f = jjbqyppegg.f1012f;
            jjbqyppegg.f1012f = null;
            return jjbqyppegg;
        }
    }

    /* renamed from: e */
    private boolean m775e() {
        if (!this.f1011e) {
            return false;
        }
        this.f1011e = false;
        return jjbQypPegg.m773a(this);
    }

    /* renamed from: a */
    final IOException m776a(IOException iOException) {
        return !m775e() ? iOException : mo4304b(iOException);
    }

    /* renamed from: a */
    public final void m777a() {
        if (this.f1011e) {
            throw new IllegalStateException("Unbalanced enter/exit");
        }
        long f_ = f_();
        boolean b = mo4301b();
        if (f_ != 0 || b) {
            this.f1011e = true;
            jjbQypPegg.m772a(this, f_, b);
        }
    }

    /* renamed from: a */
    final void m778a(boolean z) {
        if (m775e() && z) {
            throw mo4304b(null);
        }
    }

    /* renamed from: b */
    protected IOException mo4304b(IOException iOException) {
        IOException interruptedIOException = new InterruptedIOException("timeout");
        if (iOException != null) {
            interruptedIOException.initCause(iOException);
        }
        return interruptedIOException;
    }

    protected void e_() {
    }
}
