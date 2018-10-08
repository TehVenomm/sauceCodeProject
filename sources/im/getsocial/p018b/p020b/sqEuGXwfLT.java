package im.getsocial.p018b.p020b;

import java.util.Arrays;

/* renamed from: im.getsocial.b.b.sqEuGXwfLT */
final class sqEuGXwfLT extends zoToeBNOjF {
    /* renamed from: f */
    final transient byte[][] f1033f;
    /* renamed from: g */
    final transient int[] f1034g;

    sqEuGXwfLT(cjrhisSQCL cjrhissqcl, int i) {
        int i2 = 0;
        super(null);
        rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, (long) i);
        QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
        int i3 = 0;
        int i4 = 0;
        while (i4 < i) {
            if (qCXFOjcJkE.f985c == qCXFOjcJkE.f984b) {
                throw new AssertionError("s.limit == s.pos");
            }
            i4 += qCXFOjcJkE.f985c - qCXFOjcJkE.f984b;
            i3++;
            qCXFOjcJkE = qCXFOjcJkE.f988f;
        }
        this.f1033f = new byte[i3][];
        this.f1034g = new int[(i3 << 1)];
        i3 = 0;
        QCXFOjcJkE qCXFOjcJkE2 = cjrhissqcl.f997a;
        while (i2 < i) {
            this.f1033f[i3] = qCXFOjcJkE2.f983a;
            int i5 = (qCXFOjcJkE2.f985c - qCXFOjcJkE2.f984b) + i2;
            if (i5 > i) {
                i5 = i;
            }
            this.f1034g[i3] = i5;
            this.f1034g[this.f1033f.length + i3] = qCXFOjcJkE2.f984b;
            qCXFOjcJkE2.f986d = true;
            i3++;
            qCXFOjcJkE2 = qCXFOjcJkE2.f988f;
            i2 = i5;
        }
    }

    /* renamed from: b */
    private int m796b(int i) {
        int binarySearch = Arrays.binarySearch(this.f1034g, 0, this.f1033f.length, i + 1);
        return binarySearch >= 0 ? binarySearch : binarySearch ^ -1;
    }

    /* renamed from: e */
    private zoToeBNOjF m797e() {
        return new zoToeBNOjF(mo4312d());
    }

    /* renamed from: a */
    public final byte mo4306a(int i) {
        rWfbqYooCV.m783a((long) this.f1034g[this.f1033f.length - 1], (long) i, 1);
        int b = m796b(i);
        return this.f1033f[b][(i - (b == 0 ? 0 : this.f1034g[b - 1])) + this.f1034g[this.f1033f.length + b]];
    }

    /* renamed from: a */
    public final zoToeBNOjF mo4307a(int i, int i2) {
        return m797e().mo4307a(i, i2);
    }

    /* renamed from: a */
    public final String mo4308a() {
        return m797e().mo4308a();
    }

    /* renamed from: a */
    public final boolean mo4309a(int i, byte[] bArr, int i2, int i3) {
        if (i < 0 || i > mo4311c() - i3 || i2 < 0 || i2 > bArr.length - i3) {
            return false;
        }
        int b = m796b(i);
        while (i3 > 0) {
            int i4 = b == 0 ? 0 : this.f1034g[b - 1];
            int min = Math.min(i3, ((this.f1034g[b] - i4) + i4) - i);
            if (!rWfbqYooCV.m785a(this.f1033f[b], (i - i4) + this.f1034g[this.f1033f.length + b], bArr, i2, min)) {
                return false;
            }
            i += min;
            i2 += min;
            i3 -= min;
            b++;
        }
        return true;
    }

    /* renamed from: b */
    public final String mo4310b() {
        return m797e().mo4310b();
    }

    /* renamed from: c */
    public final int mo4311c() {
        return this.f1034g[this.f1033f.length - 1];
    }

    /* renamed from: d */
    public final byte[] mo4312d() {
        int i = 0;
        Object obj = new byte[this.f1034g[this.f1033f.length - 1]];
        int length = this.f1033f.length;
        int i2 = 0;
        while (i < length) {
            int i3 = this.f1034g[length + i];
            int i4 = this.f1034g[i];
            System.arraycopy(this.f1033f[i], i3, obj, i2, i4 - i2);
            i++;
            i2 = i4;
        }
        return obj;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if ((obj instanceof zoToeBNOjF) && ((zoToeBNOjF) obj).mo4311c() == mo4311c()) {
            int i;
            zoToeBNOjF zotoebnojf = (zoToeBNOjF) obj;
            int c = mo4311c();
            if (mo4311c() - c < 0) {
                i = 0;
            } else {
                int i2 = c;
                int i3 = 0;
                int i4 = 0;
                c = m796b(0);
                while (i2 > 0) {
                    i = c == 0 ? 0 : this.f1034g[c - 1];
                    int min = Math.min(i2, ((this.f1034g[c] - i) + i) - i4);
                    if (!zotoebnojf.mo4309a(i3, this.f1033f[c], (i4 - i) + this.f1034g[this.f1033f.length + c], min)) {
                        i = 0;
                        break;
                    }
                    i4 += min;
                    i3 += min;
                    i2 -= min;
                    c++;
                }
                i = 1;
            }
            if (i != 0) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = this.d;
        if (i == 0) {
            i = 1;
            int length = this.f1033f.length;
            int i2 = 0;
            int i3 = 0;
            while (i2 < length) {
                byte[] bArr = this.f1033f[i2];
                int i4 = this.f1034g[length + i2];
                int i5 = this.f1034g[i2];
                int i6 = i;
                for (i = i4; i < (i5 - i3) + i4; i++) {
                    i6 = (i6 * 31) + bArr[i];
                }
                i2++;
                i3 = i5;
                i = i6;
            }
            this.d = i;
        }
        return i;
    }

    public final String toString() {
        return m797e().toString();
    }
}
