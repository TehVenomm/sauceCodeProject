package im.getsocial.p018b.p020b;

/* renamed from: im.getsocial.b.b.QCXFOjcJkE */
final class QCXFOjcJkE {
    /* renamed from: a */
    final byte[] f983a;
    /* renamed from: b */
    int f984b;
    /* renamed from: c */
    int f985c;
    /* renamed from: d */
    boolean f986d;
    /* renamed from: e */
    boolean f987e;
    /* renamed from: f */
    QCXFOjcJkE f988f;
    /* renamed from: g */
    QCXFOjcJkE f989g;

    QCXFOjcJkE() {
        this.f983a = new byte[8192];
        this.f987e = true;
        this.f986d = false;
    }

    QCXFOjcJkE(QCXFOjcJkE qCXFOjcJkE) {
        this(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, qCXFOjcJkE.f985c);
        qCXFOjcJkE.f986d = true;
    }

    private QCXFOjcJkE(byte[] bArr, int i, int i2) {
        this.f983a = bArr;
        this.f984b = i;
        this.f985c = i2;
        this.f987e = false;
        this.f986d = true;
    }

    /* renamed from: a */
    public final QCXFOjcJkE m753a() {
        QCXFOjcJkE qCXFOjcJkE = this.f988f != this ? this.f988f : null;
        this.f989g.f988f = this.f988f;
        this.f988f.f989g = this.f989g;
        this.f988f = null;
        this.f989g = null;
        return qCXFOjcJkE;
    }

    /* renamed from: a */
    public final QCXFOjcJkE m754a(QCXFOjcJkE qCXFOjcJkE) {
        qCXFOjcJkE.f989g = this;
        qCXFOjcJkE.f988f = this.f988f;
        this.f988f.f989g = qCXFOjcJkE;
        this.f988f = qCXFOjcJkE;
        return qCXFOjcJkE;
    }

    /* renamed from: a */
    public final void m755a(QCXFOjcJkE qCXFOjcJkE, int i) {
        if (qCXFOjcJkE.f987e) {
            if (qCXFOjcJkE.f985c + i > 8192) {
                if (qCXFOjcJkE.f986d) {
                    throw new IllegalArgumentException();
                } else if ((qCXFOjcJkE.f985c + i) - qCXFOjcJkE.f984b > 8192) {
                    throw new IllegalArgumentException();
                } else {
                    System.arraycopy(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, qCXFOjcJkE.f983a, 0, qCXFOjcJkE.f985c - qCXFOjcJkE.f984b);
                    qCXFOjcJkE.f985c -= qCXFOjcJkE.f984b;
                    qCXFOjcJkE.f984b = 0;
                }
            }
            System.arraycopy(this.f983a, this.f984b, qCXFOjcJkE.f983a, qCXFOjcJkE.f985c, i);
            qCXFOjcJkE.f985c += i;
            this.f984b += i;
            return;
        }
        throw new IllegalArgumentException();
    }
}
