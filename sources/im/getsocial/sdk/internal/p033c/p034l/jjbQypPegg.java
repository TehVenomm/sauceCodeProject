package im.getsocial.sdk.internal.p033c.p034l;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.p065a.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.internal.c.l.jjbQypPegg */
public abstract class jjbQypPegg implements upgqDBbsrL {
    /* renamed from: c */
    protected static final cjrhisSQCL f1169c = upgqDBbsrL.m1274a(upgqDBbsrL.class);
    @XdbacJlTDQ
    /* renamed from: d */
    im.getsocial.sdk.internal.p030e.p065a.XdbacJlTDQ f1170d;
    @XdbacJlTDQ
    /* renamed from: e */
    im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg f1171e;

    /* renamed from: im.getsocial.sdk.internal.c.l.jjbQypPegg$1 */
    class C09591 implements im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Object> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1469a;

        C09591(jjbQypPegg jjbqyppegg) {
            this.f1469a = jjbqyppegg;
        }

        /* renamed from: a */
        public final void mo4455a(Object obj) {
            jjbQypPegg.f1169c.mo4387a(this.f1469a.getClass().getSimpleName() + " callback success");
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.l.jjbQypPegg$2 */
    class C09602 implements im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Throwable> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1470a;

        C09602(jjbQypPegg jjbqyppegg) {
            this.f1470a = jjbqyppegg;
        }

        /* renamed from: a */
        public final /* synthetic */ void mo4455a(Object obj) {
            Throwable th = (Throwable) obj;
            jjbQypPegg.f1169c.mo4387a(this.f1470a.getClass().getSimpleName() + " callback failure");
            jjbQypPegg.f1169c.mo4389a(th);
        }
    }

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: b */
    private <T> pdwpUtZXDT<T> m983b(pdwpUtZXDT<T> pdwputzxdt) {
        f1169c.mo4387a(getClass().getSimpleName() + " execution started");
        return pdwputzxdt.m1664a(this.f1170d).m1668b(zoToeBNOjF.m1674a());
    }

    /* renamed from: a */
    protected final void m984a(pdwpUtZXDT<?> pdwputzxdt) {
        m983b(pdwputzxdt).m1666a(new C09591(this), new C09602(this));
    }

    /* renamed from: a */
    protected final <T> void m985a(pdwpUtZXDT<T> pdwputzxdt, final Callback<T> callback) {
        m983b(pdwputzxdt).m1666a(new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<T>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1476b;

            /* renamed from: a */
            public final void mo4455a(T t) {
                jjbQypPegg.f1169c.mo4387a(this.f1476b.getClass().getSimpleName() + " callback success");
                callback.onSuccess(t);
            }
        }, new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Throwable>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1478b;

            /* renamed from: a */
            public final /* synthetic */ void mo4455a(Object obj) {
                Throwable th = (Throwable) obj;
                jjbQypPegg.f1169c.mo4387a(this.f1478b.getClass().getSimpleName() + " callback failure");
                jjbQypPegg.f1169c.mo4389a(th);
                callback.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th));
            }
        });
    }

    /* renamed from: a */
    protected final void m986a(pdwpUtZXDT<?> pdwputzxdt, final CompletionCallback completionCallback) {
        m983b(pdwputzxdt).m1666a(new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Object>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1472b;

            /* renamed from: a */
            public final void mo4455a(Object obj) {
                jjbQypPegg.f1169c.mo4387a(this.f1472b.getClass().getSimpleName() + " callback success");
                completionCallback.onSuccess();
            }
        }, new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Throwable>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1474b;

            /* renamed from: a */
            public final /* synthetic */ void mo4455a(Object obj) {
                Throwable th = (Throwable) obj;
                jjbQypPegg.f1169c.mo4387a(this.f1474b.getClass().getSimpleName() + " callback failure");
                jjbQypPegg.f1169c.mo4389a(th);
                completionCallback.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th));
            }
        });
    }

    /* renamed from: b */
    protected final void m987b(pdwpUtZXDT<?> pdwputzxdt, final CompletionCallback completionCallback) {
        f1169c.mo4387a(getClass().getSimpleName() + " execution started");
        pdwputzxdt.m1664a(this.f1170d).m1666a(new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Object>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1480b;

            /* renamed from: a */
            public final void mo4455a(Object obj) {
                jjbQypPegg.f1169c.mo4387a(this.f1480b.getClass().getSimpleName() + " callback success");
                completionCallback.onSuccess();
            }
        }, new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Throwable>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1482b;

            /* renamed from: a */
            public final /* synthetic */ void mo4455a(Object obj) {
                Throwable th = (Throwable) obj;
                jjbQypPegg.f1169c.mo4387a(this.f1482b.getClass().getSimpleName() + " callback failure");
                jjbQypPegg.f1169c.mo4389a(th);
                completionCallback.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th));
            }
        });
    }

    /* renamed from: c */
    protected final im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg m988c() {
        return this.f1171e;
    }
}
