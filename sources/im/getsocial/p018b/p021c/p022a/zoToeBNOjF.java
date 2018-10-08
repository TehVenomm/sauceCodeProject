package im.getsocial.p018b.p021c.p022a;

import im.getsocial.p018b.p021c.p024c.jjbQypPegg;
import java.io.Closeable;

/* renamed from: im.getsocial.b.c.a.zoToeBNOjF */
public abstract class zoToeBNOjF implements Closeable {
    /* renamed from: a */
    protected final jjbQypPegg f1046a;

    protected zoToeBNOjF(jjbQypPegg jjbqyppegg) {
        if (jjbqyppegg == null) {
            throw new NullPointerException("transport");
        }
        this.f1046a = jjbqyppegg;
    }

    /* renamed from: a */
    public abstract void mo4316a();

    /* renamed from: a */
    public abstract void mo4317a(byte b, byte b2, int i);

    /* renamed from: a */
    public abstract void mo4318a(byte b, int i);

    /* renamed from: a */
    public abstract void mo4319a(int i);

    /* renamed from: a */
    public abstract void mo4320a(int i, byte b);

    /* renamed from: a */
    public abstract void mo4321a(long j);

    /* renamed from: a */
    public abstract void mo4322a(String str);

    /* renamed from: a */
    public abstract void mo4323a(String str, byte b, int i);

    /* renamed from: a */
    public abstract void mo4324a(boolean z);

    /* renamed from: b */
    public abstract XdbacJlTDQ mo4325b();

    /* renamed from: c */
    public abstract upgqDBbsrL mo4326c();

    public void close() {
        this.f1046a.close();
    }

    /* renamed from: d */
    public abstract pdwpUtZXDT mo4327d();

    /* renamed from: e */
    public abstract cjrhisSQCL mo4328e();

    /* renamed from: f */
    public abstract ztWNWCuZiM mo4329f();

    /* renamed from: g */
    public abstract boolean mo4330g();

    /* renamed from: h */
    public abstract byte mo4331h();

    /* renamed from: i */
    public abstract short mo4332i();

    /* renamed from: j */
    public abstract int mo4333j();

    /* renamed from: k */
    public abstract long mo4334k();

    /* renamed from: l */
    public abstract double mo4335l();

    /* renamed from: m */
    public abstract String mo4336m();

    /* renamed from: n */
    public final void m827n() {
        this.f1046a.mo4407b();
    }
}
