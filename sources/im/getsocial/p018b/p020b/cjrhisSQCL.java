package im.getsocial.p018b.p020b;

import android.support.v4.media.session.PlaybackStateCompat;
import java.io.EOFException;
import java.io.InputStream;
import java.io.OutputStream;

/* renamed from: im.getsocial.b.b.cjrhisSQCL */
public final class cjrhisSQCL implements XdbacJlTDQ, pdwpUtZXDT, Cloneable {
    /* renamed from: c */
    private static final byte[] f996c = new byte[]{(byte) 48, (byte) 49, (byte) 50, (byte) 51, (byte) 52, (byte) 53, (byte) 54, (byte) 55, (byte) 56, (byte) 57, (byte) 97, (byte) 98, (byte) 99, (byte) 100, (byte) 101, (byte) 102};
    /* renamed from: a */
    QCXFOjcJkE f997a;
    /* renamed from: b */
    long f998b;

    /* renamed from: im.getsocial.b.b.cjrhisSQCL$1 */
    class C09141 extends OutputStream {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f994a;

        public void close() {
        }

        public void flush() {
        }

        public String toString() {
            return this.f994a + ".outputStream()";
        }

        public void write(int i) {
            this.f994a.m763a((byte) i);
        }

        public void write(byte[] bArr, int i, int i2) {
            this.f994a.m767b(bArr, i, i2);
        }
    }

    /* renamed from: im.getsocial.b.b.cjrhisSQCL$2 */
    class C09152 extends InputStream {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f995a;

        public int available() {
            return (int) Math.min(this.f995a.f998b, 2147483647L);
        }

        public void close() {
        }

        public int read() {
            return this.f995a.f998b > 0 ? this.f995a.m765b() & 255 : -1;
        }

        public int read(byte[] bArr, int i, int i2) {
            return this.f995a.m760a(bArr, i, i2);
        }

        public String toString() {
            return this.f995a + ".inputStream()";
        }
    }

    /* renamed from: a */
    public final int m760a(byte[] bArr, int i, int i2) {
        rWfbqYooCV.m783a((long) bArr.length, (long) i, (long) i2);
        QCXFOjcJkE qCXFOjcJkE = this.f997a;
        if (qCXFOjcJkE == null) {
            return -1;
        }
        int min = Math.min(i2, qCXFOjcJkE.f985c - qCXFOjcJkE.f984b);
        System.arraycopy(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, bArr, i, min);
        qCXFOjcJkE.f984b += min;
        this.f998b -= (long) min;
        if (qCXFOjcJkE.f984b != qCXFOjcJkE.f985c) {
            return min;
        }
        this.f997a = qCXFOjcJkE.m753a();
        iFpupLCESp.m770a(qCXFOjcJkE);
        return min;
    }

    /* renamed from: a */
    public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
        if (cjrhissqcl == null) {
            throw new IllegalArgumentException("sink == null");
        } else if (j < 0) {
            throw new IllegalArgumentException("byteCount < 0: " + j);
        } else if (this.f998b == 0) {
            return -1;
        } else {
            long j2 = j > this.f998b ? this.f998b : j;
            cjrhissqcl.a_(this, j2);
            return j2;
        }
    }

    /* renamed from: a */
    public final cjrhisSQCL mo4299a() {
        return this;
    }

    /* renamed from: a */
    public final cjrhisSQCL m763a(int i) {
        QCXFOjcJkE b = m766b(1);
        byte[] bArr = b.f983a;
        int i2 = b.f985c;
        b.f985c = i2 + 1;
        bArr[i2] = (byte) ((byte) i);
        this.f998b++;
        return this;
    }

    /* renamed from: a */
    public final void m764a(long j) {
        while (j > 0) {
            if (this.f997a == null) {
                throw new EOFException();
            }
            int min = (int) Math.min(j, (long) (this.f997a.f985c - this.f997a.f984b));
            this.f998b -= (long) min;
            j -= (long) min;
            QCXFOjcJkE qCXFOjcJkE = this.f997a;
            qCXFOjcJkE.f984b = min + qCXFOjcJkE.f984b;
            if (this.f997a.f984b == this.f997a.f985c) {
                QCXFOjcJkE qCXFOjcJkE2 = this.f997a;
                this.f997a = qCXFOjcJkE2.m753a();
                iFpupLCESp.m770a(qCXFOjcJkE2);
            }
        }
    }

    public final void a_(cjrhisSQCL cjrhissqcl, long j) {
        if (cjrhissqcl == null) {
            throw new IllegalArgumentException("source == null");
        } else if (cjrhissqcl == this) {
            throw new IllegalArgumentException("source == this");
        } else {
            rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, j);
            while (j > 0) {
                QCXFOjcJkE qCXFOjcJkE;
                QCXFOjcJkE qCXFOjcJkE2;
                if (j < ((long) (cjrhissqcl.f997a.f985c - cjrhissqcl.f997a.f984b))) {
                    qCXFOjcJkE = this.f997a != null ? this.f997a.f989g : null;
                    if (qCXFOjcJkE != null && qCXFOjcJkE.f987e) {
                        if ((((long) qCXFOjcJkE.f985c) + j) - ((long) (qCXFOjcJkE.f986d ? 0 : qCXFOjcJkE.f984b)) <= PlaybackStateCompat.ACTION_PLAY_FROM_URI) {
                            cjrhissqcl.f997a.m755a(qCXFOjcJkE, (int) j);
                            cjrhissqcl.f998b -= j;
                            this.f998b += j;
                            return;
                        }
                    }
                    qCXFOjcJkE = cjrhissqcl.f997a;
                    int i = (int) j;
                    if (i <= 0 || i > qCXFOjcJkE.f985c - qCXFOjcJkE.f984b) {
                        throw new IllegalArgumentException();
                    }
                    if (i >= 1024) {
                        qCXFOjcJkE2 = new QCXFOjcJkE(qCXFOjcJkE);
                    } else {
                        qCXFOjcJkE2 = iFpupLCESp.m769a();
                        System.arraycopy(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, qCXFOjcJkE2.f983a, 0, i);
                    }
                    qCXFOjcJkE2.f985c = qCXFOjcJkE2.f984b + i;
                    qCXFOjcJkE.f984b = i + qCXFOjcJkE.f984b;
                    qCXFOjcJkE.f989g.m754a(qCXFOjcJkE2);
                    cjrhissqcl.f997a = qCXFOjcJkE2;
                }
                qCXFOjcJkE2 = cjrhissqcl.f997a;
                long j2 = (long) (qCXFOjcJkE2.f985c - qCXFOjcJkE2.f984b);
                cjrhissqcl.f997a = qCXFOjcJkE2.m753a();
                if (this.f997a == null) {
                    this.f997a = qCXFOjcJkE2;
                    qCXFOjcJkE2 = this.f997a;
                    qCXFOjcJkE = this.f997a;
                    QCXFOjcJkE qCXFOjcJkE3 = this.f997a;
                    qCXFOjcJkE.f989g = qCXFOjcJkE3;
                    qCXFOjcJkE2.f988f = qCXFOjcJkE3;
                } else {
                    qCXFOjcJkE = this.f997a.f989g.m754a(qCXFOjcJkE2);
                    if (qCXFOjcJkE.f989g == qCXFOjcJkE) {
                        throw new IllegalStateException();
                    } else if (qCXFOjcJkE.f989g.f987e) {
                        int i2 = qCXFOjcJkE.f985c - qCXFOjcJkE.f984b;
                        if (i2 <= (qCXFOjcJkE.f989g.f986d ? 0 : qCXFOjcJkE.f989g.f984b) + (8192 - qCXFOjcJkE.f989g.f985c)) {
                            qCXFOjcJkE.m755a(qCXFOjcJkE.f989g, i2);
                            qCXFOjcJkE.m753a();
                            iFpupLCESp.m770a(qCXFOjcJkE);
                        }
                    }
                }
                cjrhissqcl.f998b -= j2;
                this.f998b += j2;
                j -= j2;
            }
        }
    }

    /* renamed from: b */
    public final byte m765b() {
        if (this.f998b == 0) {
            throw new IllegalStateException("size == 0");
        }
        QCXFOjcJkE qCXFOjcJkE = this.f997a;
        int i = qCXFOjcJkE.f984b;
        int i2 = qCXFOjcJkE.f985c;
        int i3 = i + 1;
        byte b = qCXFOjcJkE.f983a[i];
        this.f998b--;
        if (i3 == i2) {
            this.f997a = qCXFOjcJkE.m753a();
            iFpupLCESp.m770a(qCXFOjcJkE);
        } else {
            qCXFOjcJkE.f984b = i3;
        }
        return b;
    }

    /* renamed from: b */
    final QCXFOjcJkE m766b(int i) {
        if (this.f997a == null) {
            this.f997a = iFpupLCESp.m769a();
            QCXFOjcJkE qCXFOjcJkE = this.f997a;
            QCXFOjcJkE qCXFOjcJkE2 = this.f997a;
            QCXFOjcJkE qCXFOjcJkE3 = this.f997a;
            qCXFOjcJkE2.f989g = qCXFOjcJkE3;
            qCXFOjcJkE.f988f = qCXFOjcJkE3;
            return qCXFOjcJkE3;
        }
        qCXFOjcJkE3 = this.f997a.f989g;
        return (qCXFOjcJkE3.f985c + 1 > 8192 || !qCXFOjcJkE3.f987e) ? qCXFOjcJkE3.m754a(iFpupLCESp.m769a()) : qCXFOjcJkE3;
    }

    /* renamed from: b */
    public final cjrhisSQCL m767b(byte[] bArr, int i, int i2) {
        if (bArr == null) {
            throw new IllegalArgumentException("source == null");
        }
        rWfbqYooCV.m783a((long) bArr.length, (long) i, (long) i2);
        int i3 = i + i2;
        while (i < i3) {
            QCXFOjcJkE b = m766b(1);
            int min = Math.min(i3 - i, 8192 - b.f985c);
            System.arraycopy(bArr, i, b.f983a, b.f985c, min);
            i += min;
            b.f985c = min + b.f985c;
        }
        this.f998b += (long) i2;
        return this;
    }

    /* renamed from: c */
    public final /* bridge */ /* synthetic */ pdwpUtZXDT mo4300c() {
        return this;
    }

    public final /* synthetic */ Object clone() {
        cjrhisSQCL cjrhissqcl = new cjrhisSQCL();
        if (this.f998b != 0) {
            cjrhissqcl.f997a = new QCXFOjcJkE(this.f997a);
            QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
            QCXFOjcJkE qCXFOjcJkE2 = cjrhissqcl.f997a;
            QCXFOjcJkE qCXFOjcJkE3 = cjrhissqcl.f997a;
            qCXFOjcJkE2.f989g = qCXFOjcJkE3;
            qCXFOjcJkE.f988f = qCXFOjcJkE3;
            for (qCXFOjcJkE = this.f997a.f988f; qCXFOjcJkE != this.f997a; qCXFOjcJkE = qCXFOjcJkE.f988f) {
                cjrhissqcl.f997a.f989g.m754a(new QCXFOjcJkE(qCXFOjcJkE));
            }
            cjrhissqcl.f998b = this.f998b;
        }
        return cjrhissqcl;
    }

    public final void close() {
    }

    public final boolean equals(Object obj) {
        long j = 0;
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof cjrhisSQCL)) {
            return false;
        }
        cjrhisSQCL cjrhissqcl = (cjrhisSQCL) obj;
        if (this.f998b != cjrhissqcl.f998b) {
            return false;
        }
        if (this.f998b == 0) {
            return true;
        }
        QCXFOjcJkE qCXFOjcJkE = this.f997a;
        QCXFOjcJkE qCXFOjcJkE2 = cjrhissqcl.f997a;
        int i = qCXFOjcJkE.f984b;
        int i2 = qCXFOjcJkE2.f984b;
        while (j < this.f998b) {
            long min = (long) Math.min(qCXFOjcJkE.f985c - i, qCXFOjcJkE2.f985c - i2);
            int i3 = 0;
            while (((long) i3) < min) {
                if (qCXFOjcJkE.f983a[i] != qCXFOjcJkE2.f983a[i2]) {
                    return false;
                }
                i3++;
                i2++;
                i++;
            }
            if (i == qCXFOjcJkE.f985c) {
                qCXFOjcJkE = qCXFOjcJkE.f988f;
                i = qCXFOjcJkE.f984b;
            }
            if (i2 == qCXFOjcJkE2.f985c) {
                qCXFOjcJkE2 = qCXFOjcJkE2.f988f;
                i2 = qCXFOjcJkE2.f984b;
            }
            j += min;
        }
        return true;
    }

    public final void flush() {
    }

    public final int hashCode() {
        QCXFOjcJkE qCXFOjcJkE = this.f997a;
        if (qCXFOjcJkE == null) {
            return 0;
        }
        QCXFOjcJkE qCXFOjcJkE2 = qCXFOjcJkE;
        int i = 1;
        QCXFOjcJkE qCXFOjcJkE3 = qCXFOjcJkE2;
        do {
            int i2 = qCXFOjcJkE3.f984b;
            int i3 = qCXFOjcJkE3.f985c;
            while (i2 < i3) {
                byte b = qCXFOjcJkE3.f983a[i2];
                i2++;
                i = (i * 31) + b;
            }
            qCXFOjcJkE3 = qCXFOjcJkE3.f988f;
        } while (qCXFOjcJkE3 != this.f997a);
        return i;
    }

    public final String toString() {
        if (this.f998b > 2147483647L) {
            throw new IllegalArgumentException("size > Integer.MAX_VALUE: " + this.f998b);
        }
        int i = (int) this.f998b;
        return (i == 0 ? zoToeBNOjF.f1029b : new sqEuGXwfLT(this, i)).toString();
    }
}
