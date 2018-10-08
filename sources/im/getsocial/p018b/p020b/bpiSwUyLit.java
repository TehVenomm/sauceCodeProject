package im.getsocial.p018b.p020b;

import java.io.IOException;
import java.io.OutputStream;

/* renamed from: im.getsocial.b.b.bpiSwUyLit */
final class bpiSwUyLit implements pdwpUtZXDT {
    /* renamed from: a */
    public final cjrhisSQCL f991a;
    /* renamed from: b */
    public final rFvvVpjzZH f992b;
    /* renamed from: c */
    boolean f993c;

    /* renamed from: im.getsocial.b.b.bpiSwUyLit$1 */
    class C09131 extends OutputStream {
        /* renamed from: a */
        final /* synthetic */ bpiSwUyLit f990a;

        public void close() {
            this.f990a.close();
        }

        public void flush() {
            if (!this.f990a.f993c) {
                this.f990a.flush();
            }
        }

        public String toString() {
            return this.f990a + ".outputStream()";
        }

        public void write(int i) {
            if (this.f990a.f993c) {
                throw new IOException("closed");
            }
            this.f990a.f991a.m763a((byte) i);
            this.f990a.mo4300c();
        }

        public void write(byte[] bArr, int i, int i2) {
            if (this.f990a.f993c) {
                throw new IOException("closed");
            }
            this.f990a.f991a.m767b(bArr, i, i2);
            this.f990a.mo4300c();
        }
    }

    /* renamed from: a */
    public final cjrhisSQCL mo4299a() {
        return this.f991a;
    }

    public final void a_(cjrhisSQCL cjrhissqcl, long j) {
        if (this.f993c) {
            throw new IllegalStateException("closed");
        }
        this.f991a.a_(cjrhissqcl, j);
        mo4300c();
    }

    /* renamed from: c */
    public final pdwpUtZXDT mo4300c() {
        if (this.f993c) {
            throw new IllegalStateException("closed");
        }
        cjrhisSQCL cjrhissqcl = this.f991a;
        long j = cjrhissqcl.f998b;
        if (j == 0) {
            j = 0;
        } else {
            QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a.f989g;
            if (qCXFOjcJkE.f985c < 8192 && qCXFOjcJkE.f987e) {
                j -= (long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b);
            }
        }
        if (j > 0) {
            this.f992b.a_(this.f991a, j);
        }
        return this;
    }

    public final void close() {
        if (!this.f993c) {
            Throwable th = null;
            try {
                if (this.f991a.f998b > 0) {
                    this.f992b.a_(this.f991a, this.f991a.f998b);
                }
            } catch (Throwable th2) {
                th = th2;
            }
            try {
                this.f992b.close();
            } catch (Throwable th3) {
                if (th == null) {
                    th = th3;
                }
            }
            this.f993c = true;
            if (th != null) {
                rWfbqYooCV.m784a(th);
            }
        }
    }

    public final void flush() {
        if (this.f993c) {
            throw new IllegalStateException("closed");
        }
        if (this.f991a.f998b > 0) {
            this.f992b.a_(this.f991a, this.f991a.f998b);
        }
        this.f992b.flush();
    }

    public final String toString() {
        return "buffer(" + this.f992b + ")";
    }
}
