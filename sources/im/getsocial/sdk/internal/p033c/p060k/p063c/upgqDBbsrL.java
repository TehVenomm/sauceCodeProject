package im.getsocial.sdk.internal.p033c.p060k.p063c;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p041b.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.ConcurrentMap;

/* renamed from: im.getsocial.sdk.internal.c.k.c.upgqDBbsrL */
public final class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1332a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    /* renamed from: b */
    private final jjbQypPegg f1333b;
    /* renamed from: c */
    private final im.getsocial.sdk.internal.p033c.p060k.upgqDBbsrL f1334c;
    /* renamed from: d */
    private final String f1335d;
    /* renamed from: e */
    private final ConcurrentLinkedQueue<im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL> f1336e = new ConcurrentLinkedQueue();
    /* renamed from: f */
    private final ConcurrentMap<Integer, im.getsocial.sdk.internal.p033c.p060k.cjrhisSQCL> f1337f = new ConcurrentHashMap();

    @XdbacJlTDQ
    upgqDBbsrL(jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p033c.p060k.upgqDBbsrL upgqdbbsrl, @KSZKMmRWhZ(a = "UserAgent") String str) {
        this.f1333b = jjbqyppegg;
        this.f1334c = upgqdbbsrl;
        this.f1335d = str;
    }

    /* renamed from: a */
    private im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL m1357a(im.getsocial.sdk.internal.p033c.p060k.jjbQypPegg jjbqyppegg) {
        f1332a.mo4387a("Connecting to " + jjbqyppegg.m1379a() + " isSecure = [" + jjbqyppegg.m1380b() + "]");
        im.getsocial.p018b.p021c.p024c.jjbQypPegg a = new im.getsocial.sdk.internal.p033c.p060k.cjrhisSQCL.jjbQypPegg(jjbqyppegg.m1379a()).m1366a(jjbqyppegg.m1380b()).m1364a(30000).m1368b(30000).m1365a(this.f1335d).m1367a();
        a.m1373c();
        im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL cjrhissqcl = new im.getsocial.sdk.internal.p070f.p071a.cjrhisSQCL(new im.getsocial.p018b.p021c.p022a.jjbQypPegg(a));
        this.f1337f.putIfAbsent(Integer.valueOf(cjrhissqcl.hashCode()), a);
        return cjrhissqcl;
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL mo4405a() {
        if (this.f1333b.mo4401a()) {
            im.getsocial.sdk.internal.p033c.p060k.jjbQypPegg a = this.f1334c.m1495a();
            im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl = (im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL) this.f1336e.poll();
            if (upgqdbbsrl == null) {
                return m1357a(a);
            }
            im.getsocial.sdk.internal.p033c.p060k.cjrhisSQCL cjrhissqcl = (im.getsocial.sdk.internal.p033c.p060k.cjrhisSQCL) this.f1337f.get(Integer.valueOf(upgqdbbsrl.hashCode()));
            if (cjrhissqcl.m1370a().equalsIgnoreCase(a.m1379a())) {
                cjrhissqcl.close();
                cjrhissqcl.m1373c();
                return upgqdbbsrl;
            }
            f1332a.mo4387a("Hades configuration changed, removing cached clients.");
            cjrhissqcl.close();
            this.f1337f.clear();
            this.f1336e.clear();
            return m1357a(a);
        }
        throw new GetSocialException(ErrorCode.NO_INTERNET, "No internet connection.");
    }
}
