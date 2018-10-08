package im.getsocial.p018b.p020b;

import android.support.v4.media.session.PlaybackStateCompat;
import java.io.EOFException;
import java.io.IOException;
import java.io.InputStream;

/* renamed from: im.getsocial.b.b.EmkjBpiUfq */
final class EmkjBpiUfq implements XdbacJlTDQ {
    /* renamed from: a */
    public final cjrhisSQCL f975a;
    /* renamed from: b */
    public final KkSvQPDhNi f976b;
    /* renamed from: c */
    boolean f977c;

    /* renamed from: im.getsocial.b.b.EmkjBpiUfq$1 */
    class C09111 extends InputStream {
        /* renamed from: a */
        final /* synthetic */ EmkjBpiUfq f974a;

        public int available() {
            if (!this.f974a.f977c) {
                return (int) Math.min(this.f974a.f975a.f998b, 2147483647L);
            }
            throw new IOException("closed");
        }

        public void close() {
            this.f974a.close();
        }

        public int read() {
            if (!this.f974a.f977c) {
                return (this.f974a.f975a.f998b == 0 && this.f974a.f976b.mo4293a(this.f974a.f975a, PlaybackStateCompat.ACTION_PLAY_FROM_URI) == -1) ? -1 : this.f974a.f975a.m765b() & 255;
            } else {
                throw new IOException("closed");
            }
        }

        public int read(byte[] bArr, int i, int i2) {
            if (this.f974a.f977c) {
                throw new IOException("closed");
            }
            rWfbqYooCV.m783a((long) bArr.length, (long) i, (long) i2);
            return (this.f974a.f975a.f998b == 0 && this.f974a.f976b.mo4293a(this.f974a.f975a, PlaybackStateCompat.ACTION_PLAY_FROM_URI) == -1) ? -1 : this.f974a.f975a.m760a(bArr, i, i2);
        }

        public String toString() {
            return this.f974a + ".inputStream()";
        }
    }

    /* renamed from: a */
    public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
        if (cjrhissqcl == null) {
            throw new IllegalArgumentException("sink == null");
        } else if (j < 0) {
            throw new IllegalArgumentException("byteCount < 0: " + j);
        } else if (this.f977c) {
            throw new IllegalStateException("closed");
        } else if (this.f975a.f998b == 0 && this.f976b.mo4293a(this.f975a, PlaybackStateCompat.ACTION_PLAY_FROM_URI) == -1) {
            return -1;
        } else {
            return this.f975a.mo4293a(cjrhissqcl, Math.min(j, this.f975a.f998b));
        }
    }

    public final void close() {
        if (!this.f977c) {
            this.f977c = true;
            this.f976b.close();
            cjrhisSQCL cjrhissqcl = this.f975a;
            try {
                cjrhissqcl.m764a(cjrhissqcl.f998b);
            } catch (EOFException e) {
                throw new AssertionError(e);
            }
        }
    }

    public final String toString() {
        return "buffer(" + this.f976b + ")";
    }
}
