package im.getsocial.p026c.p027a;

import java.io.BufferedInputStream;
import java.io.InputStream;

/* renamed from: im.getsocial.c.a.KSZKMmRWhZ */
class KSZKMmRWhZ {
    /* renamed from: a */
    private InputStream f1068a;
    /* renamed from: b */
    private long f1069b;
    /* renamed from: c */
    private long f1070c = -1;

    public KSZKMmRWhZ(InputStream inputStream) {
        if (!inputStream.markSupported()) {
            inputStream = new BufferedInputStream(inputStream);
        }
        this.f1068a = inputStream;
    }

    /* renamed from: a */
    public final int m870a(byte[] bArr, int i) {
        int read = this.f1068a.read(bArr, 0, i);
        this.f1069b += (long) read;
        return read;
    }

    /* renamed from: a */
    public final void m871a() {
        this.f1068a.close();
    }

    /* renamed from: a */
    public final void m872a(int i) {
        this.f1070c = this.f1069b;
        this.f1068a.mark(i);
    }

    /* renamed from: a */
    public final void m873a(long j) {
        if (this.f1070c != -1) {
            this.f1068a.reset();
            this.f1068a.skip(j - this.f1070c);
            this.f1070c = -1;
        } else {
            this.f1068a.skip(j);
        }
        this.f1069b = j;
    }
}
