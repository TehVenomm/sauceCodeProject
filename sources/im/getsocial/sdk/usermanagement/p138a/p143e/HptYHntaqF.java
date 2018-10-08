package im.getsocial.sdk.usermanagement.p138a.p143e;

import im.getsocial.sdk.internal.p033c.SKUqohGtGQ;
import im.getsocial.sdk.internal.p033c.p034l.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p059j.jjbQypPegg;
import im.getsocial.sdk.usermanagement.OnUserChangedListener;

/* renamed from: im.getsocial.sdk.usermanagement.a.e.HptYHntaqF */
public final class HptYHntaqF implements upgqDBbsrL {
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f3307a;
    @XdbacJlTDQ
    /* renamed from: b */
    SKUqohGtGQ f3308b;

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.HptYHntaqF$1 */
    class C12201 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ HptYHntaqF f3306a;

        C12201(HptYHntaqF hptYHntaqF) {
            this.f3306a = hptYHntaqF;
        }

        public void run() {
            OnUserChangedListener c = this.f3306a.f3307a.m1313c();
            if (c != null) {
                c.onUserChanged();
            }
        }
    }

    public HptYHntaqF() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final void m3710a() {
        this.f3308b.mo4358a(new C12201(this));
    }
}
