package im.getsocial.p018b.p020b;

import java.io.IOException;

/* renamed from: im.getsocial.b.b.qdyNCsqjKt */
public final class qdyNCsqjKt {
    /* renamed from: a */
    final long f1018a;
    /* renamed from: b */
    final cjrhisSQCL f1019b;
    /* renamed from: c */
    boolean f1020c;
    /* renamed from: d */
    boolean f1021d;

    /* renamed from: im.getsocial.b.b.qdyNCsqjKt$jjbQypPegg */
    final class jjbQypPegg implements rFvvVpjzZH {
        /* renamed from: a */
        final IbawHMWljm f1014a;
        /* renamed from: b */
        final /* synthetic */ qdyNCsqjKt f1015b;

        public final void a_(cjrhisSQCL cjrhissqcl, long j) {
            synchronized (this.f1015b.f1019b) {
                if (this.f1015b.f1020c) {
                    throw new IllegalStateException("closed");
                }
                while (j > 0) {
                    if (this.f1015b.f1021d) {
                        throw new IOException("source is closed");
                    }
                    long j2 = this.f1015b.f1018a - this.f1015b.f1019b.f998b;
                    if (j2 == 0) {
                        this.f1014a.m748a(this.f1015b.f1019b);
                    } else {
                        j2 = Math.min(j2, j);
                        this.f1015b.f1019b.a_(cjrhissqcl, j2);
                        j -= j2;
                        this.f1015b.f1019b.notifyAll();
                    }
                }
            }
        }

        /* JADX WARNING: inconsistent code. */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public final void close() {
            /*
            r4 = this;
            r0 = r4.f1015b;
            r1 = r0.f1019b;
            monitor-enter(r1);
            r0 = r4.f1015b;	 Catch:{ all -> 0x001e }
            r0 = r0.f1020c;	 Catch:{ all -> 0x001e }
            if (r0 == 0) goto L_0x000d;
        L_0x000b:
            monitor-exit(r1);	 Catch:{ all -> 0x001e }
        L_0x000c:
            return;
        L_0x000d:
            r4.flush();	 Catch:{ all -> 0x0021 }
            r0 = r4.f1015b;	 Catch:{ all -> 0x001e }
            r2 = 1;
            r0.f1020c = r2;	 Catch:{ all -> 0x001e }
            r0 = r4.f1015b;	 Catch:{ all -> 0x001e }
            r0 = r0.f1019b;	 Catch:{ all -> 0x001e }
            r0.notifyAll();	 Catch:{ all -> 0x001e }
            monitor-exit(r1);	 Catch:{ all -> 0x001e }
            goto L_0x000c;
        L_0x001e:
            r0 = move-exception;
            monitor-exit(r1);	 Catch:{ all -> 0x001e }
            throw r0;
        L_0x0021:
            r0 = move-exception;
            r2 = r4.f1015b;	 Catch:{ all -> 0x001e }
            r3 = 1;
            r2.f1020c = r3;	 Catch:{ all -> 0x001e }
            r2 = r4.f1015b;	 Catch:{ all -> 0x001e }
            r2 = r2.f1019b;	 Catch:{ all -> 0x001e }
            r2.notifyAll();	 Catch:{ all -> 0x001e }
            throw r0;	 Catch:{ all -> 0x001e }
            */
            throw new UnsupportedOperationException("Method not decompiled: im.getsocial.b.b.qdyNCsqjKt.jjbQypPegg.close():void");
        }

        public final void flush() {
            synchronized (this.f1015b.f1019b) {
                if (this.f1015b.f1020c) {
                    throw new IllegalStateException("closed");
                }
                while (this.f1015b.f1019b.f998b > 0) {
                    if (this.f1015b.f1021d) {
                        throw new IOException("source is closed");
                    }
                    this.f1014a.m748a(this.f1015b.f1019b);
                }
            }
        }
    }

    /* renamed from: im.getsocial.b.b.qdyNCsqjKt$upgqDBbsrL */
    final class upgqDBbsrL implements KkSvQPDhNi {
        /* renamed from: a */
        final IbawHMWljm f1016a;
        /* renamed from: b */
        final /* synthetic */ qdyNCsqjKt f1017b;

        /* renamed from: a */
        public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
            long j2;
            synchronized (this.f1017b.f1019b) {
                if (this.f1017b.f1021d) {
                    throw new IllegalStateException("closed");
                }
                while (this.f1017b.f1019b.f998b == 0) {
                    if (this.f1017b.f1020c) {
                        j2 = -1;
                        break;
                    }
                    this.f1016a.m748a(this.f1017b.f1019b);
                }
                j2 = this.f1017b.f1019b.mo4293a(cjrhissqcl, j);
                this.f1017b.f1019b.notifyAll();
            }
            return j2;
        }

        public final void close() {
            synchronized (this.f1017b.f1019b) {
                this.f1017b.f1021d = true;
                this.f1017b.f1019b.notifyAll();
            }
        }
    }
}
