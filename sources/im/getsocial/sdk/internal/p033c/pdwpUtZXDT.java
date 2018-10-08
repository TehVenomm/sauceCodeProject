package im.getsocial.sdk.internal.p033c;

import android.content.Context;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;
import im.getsocial.sdk.pushnotifications.p067a.p068a.jjbQypPegg;
import java.util.concurrent.Callable;

/* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT */
public class pdwpUtZXDT implements jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1494a = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    /* renamed from: b */
    private final im.getsocial.sdk.internal.upgqDBbsrL f1495b;
    /* renamed from: c */
    private final im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg f1496c;

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$1 */
    class C09681 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f1483a;

        C09681(pdwpUtZXDT pdwputzxdt) {
            this.f1483a = pdwputzxdt;
        }

        public void run() {
            pdwpUtZXDT.m1530a(this.f1483a);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$2 */
    class C09692 implements CompletionCallback {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f1484a;

        C09692(pdwpUtZXDT pdwputzxdt) {
            this.f1484a = pdwputzxdt;
        }

        public void onFailure(GetSocialException getSocialException) {
            pdwpUtZXDT.f1494a.mo4391b("Failed to register for a push notifications. Error: %s", getSocialException.getLocalizedMessage());
        }

        public void onSuccess() {
            pdwpUtZXDT.f1494a.mo4387a("Successfully register for a push notifications.");
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$jjbQypPegg */
    private static abstract class jjbQypPegg<V> implements Callable<V> {
        /* renamed from: a */
        private final long f1488a;
        /* renamed from: b */
        private final float f1489b;
        /* renamed from: c */
        private final int f1490c;

        jjbQypPegg(long j, float f, int i) {
            this.f1488a = j;
            this.f1489b = f;
            this.f1490c = i;
        }

        /* renamed from: a */
        private V m1522a() {
            try {
                return call();
            } catch (Exception e) {
                return null;
            }
        }

        /* renamed from: a */
        public abstract boolean mo4456a(V v);

        /* renamed from: b */
        public final V m1524b(V v) {
            for (int i = 0; i < this.f1490c; i++) {
                V a = m1522a();
                if (mo4456a(a)) {
                    return a;
                }
                try {
                    Thread.sleep((long) (((double) this.f1488a) * Math.pow((double) this.f1489b, (double) i)));
                } catch (InterruptedException e) {
                }
            }
            return null;
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$4 */
    class C09714 extends jjbQypPegg<String> {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f1491a;

        C09714(pdwpUtZXDT pdwputzxdt, long j, float f, int i) {
            this.f1491a = pdwputzxdt;
            super(1000, 2.0f, 3);
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ boolean mo4456a(Object obj) {
            return !ztWNWCuZiM.m1521a((String) obj);
        }

        public /* synthetic */ Object call() {
            return this.f1491a.m1533c();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$pdwpUtZXDT */
    private interface pdwpUtZXDT {
        /* renamed from: a */
        String mo4457a();
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$cjrhisSQCL */
    private static final class cjrhisSQCL implements pdwpUtZXDT {
        @XdbacJlTDQ
        /* renamed from: a */
        Context f1492a;
        @XdbacJlTDQ
        /* renamed from: b */
        im.getsocial.sdk.pushnotifications.p067a.p105d.jjbQypPegg f1493b;

        @XdbacJlTDQ
        cjrhisSQCL() {
            im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
        }

        /* renamed from: a */
        public final String mo4457a() {
            String a = this.f1493b.m2432b().m2425a();
            return (String) im.getsocial.sdk.internal.p090k.jjbQypPegg.m2088a("com.google.android.gms.iid.InstanceID").m2089a("getInstance", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(this.f1492a.getApplicationContext(), Context.class)).m2090a("getToken", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(a, String.class), im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a("GCM", String.class)).m2091a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.pdwpUtZXDT$upgqDBbsrL */
    private static final class upgqDBbsrL implements pdwpUtZXDT {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        public final String mo4457a() {
            return (String) im.getsocial.sdk.internal.p090k.jjbQypPegg.m2088a("com.google.firebase.iid.FirebaseInstanceId").m2089a("getInstance", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]).m2090a("getToken", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]).m2091a();
        }
    }

    @XdbacJlTDQ
    pdwpUtZXDT(im.getsocial.sdk.internal.upgqDBbsrL upgqdbbsrl, im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg jjbqyppegg) {
        this.f1495b = upgqdbbsrl;
        this.f1496c = jjbqyppegg;
    }

    /* renamed from: a */
    static /* synthetic */ void m1530a(pdwpUtZXDT pdwputzxdt) {
        final String str = (String) new C09714(pdwputzxdt, 1000, 2.0f, 3).m1524b(null);
        if (str == null) {
            f1494a.mo4393c("Failed to obtain push token");
            return;
        }
        final CompletionCallback c09692 = new C09692(pdwputzxdt);
        pdwputzxdt.f1496c.m1243a(new Runnable(pdwputzxdt) {
            /* renamed from: c */
            final /* synthetic */ pdwpUtZXDT f1487c;

            public void run() {
                im.getsocial.sdk.internal.cjrhisSQCL.m1573a(this.f1487c.f1495b, str, c09692);
            }
        }, im.getsocial.sdk.internal.p033c.p052d.p053a.jjbQypPegg.m1230a(c09692));
    }

    /* renamed from: c */
    private String m1533c() {
        try {
            return new upgqDBbsrL().mo4457a();
        } catch (Throwable e) {
            try {
                return new cjrhisSQCL().mo4457a();
            } catch (Throwable e2) {
                f1494a.mo4387a("Failed to obtain push token");
                f1494a.mo4387a("GCM:");
                f1494a.mo4389a(e2);
                f1494a.mo4387a("Firebase:");
                f1494a.mo4389a(e);
                return null;
            }
        }
    }

    /* renamed from: a */
    public final void mo4458a() {
        new Thread(new C09681(this)).start();
    }
}
