package im.getsocial.sdk.internal.p033c.p060k.p061a;

import im.getsocial.sdk.internal.p033c.p060k.p062b.jjbQypPegg;
import im.getsocial.sdk.internal.p070f.p071a.KkSvQPDhNi;

/* renamed from: im.getsocial.sdk.internal.c.k.a.upgqDBbsrL */
public abstract class upgqDBbsrL<T> implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL, T> {
    /* renamed from: b */
    private T m1352b(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
        try {
            return mo4410a(upgqdbbsrl);
        } catch (RuntimeException e) {
            throw e;
        } catch (KkSvQPDhNi e2) {
            throw jjbQypPegg.m1355a(e2);
        } catch (Throwable e3) {
            throw new RuntimeException(e3);
        }
    }

    /* renamed from: a */
    protected abstract T mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl);

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        return m1352b((im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL) obj);
    }
}
