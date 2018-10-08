package im.getsocial.sdk.pushnotifications.p067a.p105d;

import im.getsocial.sdk.internal.p033c.p041b.KluUZYuxme;
import im.getsocial.sdk.internal.p033c.p041b.jMsobIMeui;
import im.getsocial.sdk.pushnotifications.NotificationListener;
import im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ;
import java.util.Date;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.pushnotifications.a.d.upgqDBbsrL */
public class upgqDBbsrL implements KluUZYuxme {
    /* renamed from: a */
    private static final long f2480a = TimeUnit.SECONDS.toMillis(5);
    /* renamed from: b */
    private upgqDBbsrL f2481b = upgqDBbsrL.NOT_REGISTERED;
    /* renamed from: c */
    private jjbQypPegg<XdbacJlTDQ> f2482c;
    /* renamed from: d */
    private NotificationListener f2483d;

    /* renamed from: im.getsocial.sdk.pushnotifications.a.d.upgqDBbsrL$jjbQypPegg */
    private static class jjbQypPegg<T> {
        /* renamed from: a */
        private final T f2478a;
        /* renamed from: b */
        private final long f2479b;

        jjbQypPegg(T t, long j) {
            this.f2478a = t;
            this.f2479b = jjbQypPegg.m2433b() + j;
        }

        /* renamed from: b */
        private static long m2433b() {
            return new Date().getTime();
        }

        /* renamed from: a */
        public final T m2434a() {
            return ((this.f2479b > jjbQypPegg.m2433b() ? 1 : (this.f2479b == jjbQypPegg.m2433b() ? 0 : -1)) < 0 ? 1 : null) != null ? null : this.f2478a;
        }
    }

    /* renamed from: im.getsocial.sdk.pushnotifications.a.d.upgqDBbsrL$upgqDBbsrL */
    public enum upgqDBbsrL {
        REGISTERED,
        NOT_REGISTERED
    }

    /* renamed from: a */
    public final jMsobIMeui mo4351a() {
        return jMsobIMeui.APP;
    }

    /* renamed from: a */
    public final void m2436a(NotificationListener notificationListener) {
        this.f2483d = notificationListener;
    }

    /* renamed from: a */
    public final void m2437a(XdbacJlTDQ xdbacJlTDQ) {
        if (xdbacJlTDQ == null) {
            this.f2482c = null;
        } else {
            this.f2482c = new jjbQypPegg(xdbacJlTDQ, f2480a);
        }
    }

    /* renamed from: a */
    public final void m2438a(upgqDBbsrL upgqdbbsrl) {
        this.f2481b = upgqdbbsrl;
    }

    /* renamed from: b */
    public final XdbacJlTDQ m2439b() {
        if (this.f2482c == null) {
            return null;
        }
        XdbacJlTDQ xdbacJlTDQ = (XdbacJlTDQ) this.f2482c.m2434a();
        this.f2482c = null;
        return xdbacJlTDQ;
    }

    /* renamed from: c */
    public final NotificationListener m2440c() {
        return this.f2483d;
    }
}
