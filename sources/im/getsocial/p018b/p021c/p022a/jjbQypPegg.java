package im.getsocial.p018b.p021c.p022a;

import android.support.v4.internal.view.SupportMenu;
import java.io.EOFException;
import java.io.UnsupportedEncodingException;
import java.net.ProtocolException;

/* renamed from: im.getsocial.b.c.a.jjbQypPegg */
public class jjbQypPegg extends zoToeBNOjF {
    /* renamed from: b */
    private static final KSZKMmRWhZ f1047b = new KSZKMmRWhZ("");
    /* renamed from: c */
    private final long f1048c;
    /* renamed from: d */
    private final long f1049d;
    /* renamed from: e */
    private final byte[] f1050e;

    public jjbQypPegg(im.getsocial.p018b.p021c.p024c.jjbQypPegg jjbqyppegg) {
        this(jjbqyppegg, -1, -1);
    }

    private jjbQypPegg(im.getsocial.p018b.p021c.p024c.jjbQypPegg jjbqyppegg, int i, int i2) {
        super(jjbqyppegg);
        this.f1050e = new byte[8];
        this.f1048c = -1;
        this.f1049d = -1;
    }

    /* renamed from: a */
    private void m828a(byte b) {
        this.f1050e[0] = (byte) b;
        this.a.mo4408b(this.f1050e, 0, 1);
    }

    /* renamed from: a */
    private void m829a(byte[] bArr, int i) {
        int i2 = 0;
        int i3 = i;
        while (i3 > 0) {
            int a = this.a.mo4406a(bArr, i2, i3);
            if (a == -1) {
                throw new EOFException("Expected " + i + " bytes; got " + i2);
            }
            i3 -= a;
            i2 += a;
        }
    }

    /* renamed from: b */
    private String m830b(int i) {
        byte[] bArr = new byte[i];
        m829a(bArr, i);
        return new String(bArr, "UTF-8");
    }

    /* renamed from: a */
    public final void mo4316a() {
        m828a((byte) 0);
    }

    /* renamed from: a */
    public final void mo4317a(byte b, byte b2, int i) {
        m828a(b);
        m828a(b2);
        mo4319a(i);
    }

    /* renamed from: a */
    public final void mo4318a(byte b, int i) {
        m828a(b);
        mo4319a(i);
    }

    /* renamed from: a */
    public final void mo4319a(int i) {
        this.f1050e[0] = (byte) ((byte) (i >>> 24));
        this.f1050e[1] = (byte) ((byte) (i >> 16));
        this.f1050e[2] = (byte) ((byte) (i >> 8));
        this.f1050e[3] = (byte) ((byte) i);
        this.a.mo4408b(this.f1050e, 0, 4);
    }

    /* renamed from: a */
    public final void mo4320a(int i, byte b) {
        m828a(b);
        short s = (short) i;
        this.f1050e[0] = (byte) ((byte) (s >> 8));
        this.f1050e[1] = (byte) ((byte) s);
        this.a.mo4408b(this.f1050e, 0, 2);
    }

    /* renamed from: a */
    public final void mo4321a(long j) {
        this.f1050e[0] = (byte) ((byte) ((int) ((j >> 56) & 255)));
        this.f1050e[1] = (byte) ((byte) ((int) ((j >> 48) & 255)));
        this.f1050e[2] = (byte) ((byte) ((int) ((j >> 40) & 255)));
        this.f1050e[3] = (byte) ((byte) ((int) ((j >> 32) & 255)));
        this.f1050e[4] = (byte) ((byte) ((int) ((j >> 24) & 255)));
        this.f1050e[5] = (byte) ((byte) ((int) ((j >> 16) & 255)));
        this.f1050e[6] = (byte) ((byte) ((int) ((j >> 8) & 255)));
        this.f1050e[7] = (byte) ((byte) ((int) (j & 255)));
        this.a.mo4408b(this.f1050e, 0, 8);
    }

    /* renamed from: a */
    public final void mo4322a(String str) {
        try {
            byte[] bytes = str.getBytes("UTF-8");
            mo4319a(bytes.length);
            this.a.mo4408b(bytes, 0, bytes.length);
        } catch (UnsupportedEncodingException e) {
            throw new AssertionError(e);
        }
    }

    /* renamed from: a */
    public final void mo4323a(String str, byte b, int i) {
        mo4322a(str);
        m828a(b);
        mo4319a(i);
    }

    /* renamed from: a */
    public final void mo4324a(boolean z) {
        m828a(z ? (byte) 1 : (byte) 0);
    }

    /* renamed from: b */
    public final XdbacJlTDQ mo4325b() {
        int j = mo4333j();
        if (j >= 0) {
            return new XdbacJlTDQ(m830b(j), mo4331h(), mo4333j());
        }
        if ((SupportMenu.CATEGORY_MASK & j) == -2147418112) {
            return new XdbacJlTDQ(mo4336m(), (byte) j, mo4333j());
        }
        throw new ProtocolException("Bad version in readMessageBegin");
    }

    /* renamed from: c */
    public final upgqDBbsrL mo4326c() {
        byte h = mo4331h();
        return new upgqDBbsrL("", h, h == (byte) 0 ? (short) 0 : mo4332i());
    }

    /* renamed from: d */
    public final pdwpUtZXDT mo4327d() {
        byte h = mo4331h();
        byte h2 = mo4331h();
        int j = mo4333j();
        if (this.f1049d == -1 || ((long) j) <= this.f1049d) {
            return new pdwpUtZXDT(h, h2, j);
        }
        throw new ProtocolException("Container size limit exceeded");
    }

    /* renamed from: e */
    public final cjrhisSQCL mo4328e() {
        byte h = mo4331h();
        int j = mo4333j();
        if (this.f1049d == -1 || ((long) j) <= this.f1049d) {
            return new cjrhisSQCL(h, j);
        }
        throw new ProtocolException("Container size limit exceeded");
    }

    /* renamed from: f */
    public final ztWNWCuZiM mo4329f() {
        byte h = mo4331h();
        int j = mo4333j();
        if (this.f1049d == -1 || ((long) j) <= this.f1049d) {
            return new ztWNWCuZiM(h, j);
        }
        throw new ProtocolException("Container size limit exceeded");
    }

    /* renamed from: g */
    public final boolean mo4330g() {
        return mo4331h() == (byte) 1;
    }

    /* renamed from: h */
    public final byte mo4331h() {
        m829a(this.f1050e, 1);
        return this.f1050e[0];
    }

    /* renamed from: i */
    public final short mo4332i() {
        m829a(this.f1050e, 2);
        return (short) (((this.f1050e[0] & 255) << 8) | (this.f1050e[1] & 255));
    }

    /* renamed from: j */
    public final int mo4333j() {
        m829a(this.f1050e, 4);
        return ((((this.f1050e[0] & 255) << 24) | ((this.f1050e[1] & 255) << 16)) | ((this.f1050e[2] & 255) << 8)) | (this.f1050e[3] & 255);
    }

    /* renamed from: k */
    public final long mo4334k() {
        m829a(this.f1050e, 8);
        return ((((((((((long) this.f1050e[0]) & 255) << 56) | ((((long) this.f1050e[1]) & 255) << 48)) | ((((long) this.f1050e[2]) & 255) << 40)) | ((((long) this.f1050e[3]) & 255) << 32)) | ((((long) this.f1050e[4]) & 255) << 24)) | ((((long) this.f1050e[5]) & 255) << 16)) | ((((long) this.f1050e[6]) & 255) << 8)) | (((long) this.f1050e[7]) & 255);
    }

    /* renamed from: l */
    public final double mo4335l() {
        return Double.longBitsToDouble(mo4334k());
    }

    /* renamed from: m */
    public final String mo4336m() {
        int j = mo4333j();
        if (this.f1048c == -1 || ((long) j) <= this.f1048c) {
            return m830b(j);
        }
        throw new ProtocolException("String size limit exceeded");
    }
}
