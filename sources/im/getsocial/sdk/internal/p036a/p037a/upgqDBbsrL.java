package im.getsocial.sdk.internal.p036a.p037a;

import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.internal.a.a.upgqDBbsrL */
public final class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private static final long f1174a = TimeUnit.SECONDS.toMillis(10);
    /* renamed from: b */
    private final ScheduledExecutorService f1175b = Executors.newSingleThreadScheduledExecutor();
    /* renamed from: c */
    private final jjbQypPegg f1176c;
    /* renamed from: d */
    private final Runnable f1177d = new C09261(this);
    /* renamed from: e */
    private ScheduledFuture f1178e;
    /* renamed from: f */
    private long f1179f = f1174a;

    /* renamed from: im.getsocial.sdk.internal.a.a.upgqDBbsrL$1 */
    class C09261 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f1173a;

        C09261(upgqDBbsrL upgqdbbsrl) {
            this.f1173a = upgqdbbsrl;
        }

        public void run() {
            upgqDBbsrL.m1012a(this.f1173a);
        }
    }

    @XdbacJlTDQ
    public upgqDBbsrL(jjbQypPegg jjbqyppegg) {
        this.f1176c = jjbqyppegg;
    }

    /* renamed from: a */
    static /* synthetic */ void m1012a(upgqDBbsrL upgqdbbsrl) {
        if (upgqdbbsrl.f1176c.mo4401a()) {
            new im.getsocial.sdk.internal.p036a.p046i.jjbQypPegg().m1055a();
        }
    }

    /* renamed from: a */
    public final void mo4345a() {
        this.f1178e = this.f1175b.scheduleWithFixedDelay(this.f1177d, this.f1179f, this.f1179f, TimeUnit.MILLISECONDS);
    }

    /* renamed from: b */
    public final void mo4346b() {
        if (this.f1178e != null) {
            this.f1178e.cancel(false);
            this.f1178e = null;
        }
    }
}
