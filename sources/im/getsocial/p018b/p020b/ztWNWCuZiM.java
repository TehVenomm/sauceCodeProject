package im.getsocial.p018b.p020b;

import im.getsocial.p018b.p019a.jjbQypPegg;
import java.util.zip.Deflater;

/* renamed from: im.getsocial.b.b.ztWNWCuZiM */
public final class ztWNWCuZiM implements rFvvVpjzZH {
    /* renamed from: a */
    private final pdwpUtZXDT f1037a;
    /* renamed from: b */
    private final Deflater f1038b;
    /* renamed from: c */
    private boolean f1039c;

    @jjbQypPegg
    /* renamed from: a */
    private void m805a(boolean z) {
        cjrhisSQCL a = this.f1037a.mo4299a();
        while (true) {
            QCXFOjcJkE b = a.m766b(1);
            int deflate = z ? this.f1038b.deflate(b.f983a, b.f985c, 8192 - b.f985c, 2) : this.f1038b.deflate(b.f983a, b.f985c, 8192 - b.f985c);
            if (deflate > 0) {
                b.f985c += deflate;
                a.f998b += (long) deflate;
                this.f1037a.mo4300c();
            } else if (this.f1038b.needsInput()) {
                break;
            }
        }
        if (b.f984b == b.f985c) {
            a.f997a = b.m753a();
            iFpupLCESp.m770a(b);
        }
    }

    public final void a_(cjrhisSQCL cjrhissqcl, long j) {
        rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, j);
        while (j > 0) {
            QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
            int min = (int) Math.min(j, (long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b));
            this.f1038b.setInput(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, min);
            m805a(false);
            cjrhissqcl.f998b -= (long) min;
            qCXFOjcJkE.f984b += min;
            if (qCXFOjcJkE.f984b == qCXFOjcJkE.f985c) {
                cjrhissqcl.f997a = qCXFOjcJkE.m753a();
                iFpupLCESp.m770a(qCXFOjcJkE);
            }
            j -= (long) min;
        }
    }

    public final void close() {
        Throwable th;
        if (!this.f1039c) {
            Throwable th2 = null;
            try {
                this.f1038b.finish();
                m805a(false);
            } catch (Throwable th3) {
                th2 = th3;
            }
            try {
                this.f1038b.end();
                th3 = th2;
            } catch (Throwable th4) {
                th3 = th4;
                if (th2 != null) {
                    th3 = th2;
                }
            }
            try {
                this.f1037a.close();
            } catch (Throwable th22) {
                if (th3 == null) {
                    th3 = th22;
                }
            }
            this.f1039c = true;
            if (th3 != null) {
                rWfbqYooCV.m784a(th3);
            }
        }
    }

    public final void flush() {
        m805a(true);
        this.f1037a.flush();
    }

    public final String toString() {
        return "DeflaterSink(" + this.f1037a + ")";
    }
}
