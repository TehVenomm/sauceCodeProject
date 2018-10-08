package im.getsocial.p018b.p020b;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.net.SocketTimeoutException;
import java.util.logging.Level;
import java.util.logging.Logger;

/* renamed from: im.getsocial.b.b.ruWsnwUPKh */
public final class ruWsnwUPKh {

    /* renamed from: im.getsocial.b.b.ruWsnwUPKh$1 */
    static final class C09181 implements rFvvVpjzZH {
        /* renamed from: a */
        final /* synthetic */ IbawHMWljm f1023a;
        /* renamed from: b */
        final /* synthetic */ OutputStream f1024b;

        public final void a_(cjrhisSQCL cjrhissqcl, long j) {
            rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, j);
            while (j > 0) {
                this.f1023a.mo4295d();
                QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
                int min = (int) Math.min(j, (long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b));
                this.f1024b.write(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, min);
                qCXFOjcJkE.f984b += min;
                j -= (long) min;
                cjrhissqcl.f998b -= (long) min;
                if (qCXFOjcJkE.f984b == qCXFOjcJkE.f985c) {
                    cjrhissqcl.f997a = qCXFOjcJkE.m753a();
                    iFpupLCESp.m770a(qCXFOjcJkE);
                }
            }
        }

        public final void close() {
            this.f1024b.close();
        }

        public final void flush() {
            this.f1024b.flush();
        }

        public final String toString() {
            return "sink(" + this.f1024b + ")";
        }
    }

    /* renamed from: im.getsocial.b.b.ruWsnwUPKh$2 */
    static final class C09192 implements KkSvQPDhNi {
        /* renamed from: a */
        final /* synthetic */ IbawHMWljm f1025a;
        /* renamed from: b */
        final /* synthetic */ InputStream f1026b;

        /* renamed from: a */
        public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
            if (j < 0) {
                throw new IllegalArgumentException("byteCount < 0: " + j);
            } else if (j == 0) {
                return 0;
            } else {
                try {
                    this.f1025a.mo4295d();
                    QCXFOjcJkE b = cjrhissqcl.m766b(1);
                    int read = this.f1026b.read(b.f983a, b.f985c, (int) Math.min(j, (long) (8192 - b.f985c)));
                    if (read == -1) {
                        return -1;
                    }
                    b.f985c += read;
                    cjrhissqcl.f998b += (long) read;
                    return (long) read;
                } catch (Throwable e) {
                    if (ruWsnwUPKh.m788a(e)) {
                        throw new IOException(e);
                    }
                    throw e;
                }
            }
        }

        public final void close() {
            this.f1026b.close();
        }

        public final String toString() {
            return "source(" + this.f1026b + ")";
        }
    }

    /* renamed from: im.getsocial.b.b.ruWsnwUPKh$3 */
    static final class C09203 implements rFvvVpjzZH {
        C09203() {
        }

        public final void a_(cjrhisSQCL cjrhissqcl, long j) {
            cjrhissqcl.m764a(j);
        }

        public final void close() {
        }

        public final void flush() {
        }
    }

    /* renamed from: im.getsocial.b.b.ruWsnwUPKh$4 */
    static final class C09214 extends jjbQypPegg {
        /* renamed from: b */
        final /* synthetic */ Socket f1027b;

        /* renamed from: b */
        protected final IOException mo4304b(IOException iOException) {
            IOException socketTimeoutException = new SocketTimeoutException("timeout");
            if (iOException != null) {
                socketTimeoutException.initCause(iOException);
            }
            return socketTimeoutException;
        }

        protected final void e_() {
            try {
                this.f1027b.close();
            } catch (Throwable e) {
                Logger.getLogger(ruWsnwUPKh.class.getName()).log(Level.WARNING, "Failed to close timed out socket " + this.f1027b, e);
            } catch (Throwable e2) {
                if (e2.getCause() == null || e2.getMessage() == null || !e2.getMessage().contains("getsockname failed")) {
                    throw e2;
                }
                Logger.getLogger(ruWsnwUPKh.class.getName()).log(Level.WARNING, "Failed to close timed out socket " + this.f1027b, e2);
            }
        }
    }

    private ruWsnwUPKh() {
    }

    /* renamed from: a */
    public static boolean m788a(AssertionError assertionError) {
        return (assertionError.getCause() == null || assertionError.getMessage() == null || !assertionError.getMessage().contains("getsockname failed")) ? false : true;
    }
}
