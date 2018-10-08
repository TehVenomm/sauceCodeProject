package im.getsocial.sdk.internal.p033c.p052d;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.GlobalErrorListener;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p066m.cjrhisSQCL;
import java.util.concurrent.Callable;

/* renamed from: im.getsocial.sdk.internal.c.d.jjbQypPegg */
public abstract class jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f1265a;
    /* renamed from: b */
    private GlobalErrorListener f1266b = ((GlobalErrorListener) cjrhisSQCL.m1509a(GlobalErrorListener.class));

    /* renamed from: im.getsocial.sdk.internal.c.d.jjbQypPegg$jjbQypPegg */
    public interface jjbQypPegg {
        /* renamed from: a */
        void mo4382a(GetSocialException getSocialException);
    }

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private void m1237a(GetSocialException getSocialException) {
        if (this.f1265a != null) {
            this.f1265a.m1051a(getSocialException);
        }
    }

    /* renamed from: b */
    private void m1238b(GetSocialException getSocialException, jjbQypPegg jjbqyppegg) {
        m1237a(getSocialException);
        this.f1266b.onError(getSocialException);
        mo4383a(getSocialException, jjbqyppegg);
    }

    /* renamed from: a */
    public final <T> T m1239a(Callable<T> callable, T t) {
        try {
            t = callable.call();
        } catch (Throwable th) {
            m1238b(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th), (jjbQypPegg) cjrhisSQCL.m1509a(jjbQypPegg.class));
        }
        return t;
    }

    /* renamed from: a */
    public final void m1240a() {
        this.f1266b = (GlobalErrorListener) cjrhisSQCL.m1509a(GlobalErrorListener.class);
    }

    /* renamed from: a */
    protected abstract void mo4383a(GetSocialException getSocialException, jjbQypPegg jjbqyppegg);

    /* renamed from: a */
    public final void m1242a(GlobalErrorListener globalErrorListener) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) globalErrorListener), "Error listener could not be null value");
        this.f1266b = new im.getsocial.sdk.internal.p047b.jjbQypPegg(globalErrorListener);
    }

    /* renamed from: a */
    public final void m1243a(Runnable runnable, jjbQypPegg jjbqyppegg) {
        try {
            runnable.run();
        } catch (Throwable th) {
            m1238b(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th), jjbqyppegg);
        }
    }

    /* renamed from: a */
    public final boolean m1244a(final Runnable runnable) {
        return ((Boolean) m1239a(new Callable<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1264b;

            public /* synthetic */ Object call() {
                runnable.run();
                return Boolean.valueOf(true);
            }
        }, Boolean.valueOf(false))).booleanValue();
    }

    /* renamed from: b */
    public final void m1245b(Runnable runnable) {
        try {
            runnable.run();
        } catch (Throwable th) {
            GetSocialException a = im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th);
            m1237a(a);
            mo4383a(a, (jjbQypPegg) cjrhisSQCL.m1509a(jjbQypPegg.class));
        }
    }
}
