package im.getsocial.sdk.internal.p033c.p060k.p061a;

import im.getsocial.sdk.internal.p030e.XdbacJlTDQ;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.k.a.jjbQypPegg */
public final class jjbQypPegg<R, C> {
    /* renamed from: a */
    private final String f1326a;
    /* renamed from: b */
    private cjrhisSQCL f1327b = new cjrhisSQCL();
    /* renamed from: c */
    private upgqDBbsrL<R> f1328c;
    /* renamed from: d */
    private upgqDBbsrL<R, C> f1329d = new C09451(this);
    /* renamed from: e */
    private XdbacJlTDQ<im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg> f1330e = new C09462(this);

    /* renamed from: im.getsocial.sdk.internal.c.k.a.jjbQypPegg$1 */
    class C09451 implements upgqDBbsrL<R, C> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1323a;

        C09451(jjbQypPegg jjbqyppegg) {
            this.f1323a = jjbqyppegg;
        }

        /* renamed from: a */
        public final C mo4344a(R r) {
            return r;
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.a.jjbQypPegg$2 */
    class C09462 extends XdbacJlTDQ<im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1324a;

        C09462(jjbQypPegg jjbqyppegg) {
            this.f1324a = jjbqyppegg;
        }

        /* renamed from: b */
        protected final /* bridge */ /* synthetic */ boolean mo4404b(Object obj) {
            return true;
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.a.jjbQypPegg$3 */
    class C09473 implements upgqDBbsrL<R, Void> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1325a;

        C09473(jjbQypPegg jjbqyppegg) {
            this.f1325a = jjbqyppegg;
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return null;
        }
    }

    private jjbQypPegg(String str) {
        this.f1326a = str;
    }

    /* renamed from: a */
    public static <T> jjbQypPegg<T, T> m1341a(String str) {
        return new jjbQypPegg(str);
    }

    /* renamed from: a */
    public final jjbQypPegg<R, Void> m1342a() {
        return m1346a(new C09473(this));
    }

    /* renamed from: a */
    public final jjbQypPegg<R, C> m1343a(cjrhisSQCL cjrhissqcl) {
        this.f1327b = cjrhissqcl;
        return this;
    }

    /* renamed from: a */
    public final jjbQypPegg<R, C> m1344a(upgqDBbsrL<R> upgqdbbsrl) {
        this.f1328c = upgqdbbsrl;
        return this;
    }

    /* renamed from: a */
    public final jjbQypPegg<R, C> m1345a(XdbacJlTDQ<im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg> xdbacJlTDQ) {
        this.f1330e = xdbacJlTDQ;
        return this;
    }

    /* renamed from: a */
    public final <C1> jjbQypPegg<R, C1> m1346a(upgqDBbsrL<R, C1> upgqdbbsrl) {
        jjbQypPegg<R, C1> jjbqyppegg = new jjbQypPegg(this.f1326a);
        jjbqyppegg.f1327b = this.f1327b;
        jjbqyppegg.f1328c = this.f1328c;
        jjbqyppegg.f1329d = upgqdbbsrl;
        return jjbqyppegg;
    }

    /* renamed from: a */
    public final boolean m1347a(Throwable th) {
        return th instanceof im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg ? this.f1330e.m1338c((im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg) th).booleanValue() : false;
    }

    /* renamed from: b */
    public final upgqDBbsrL<R> m1348b() {
        return this.f1328c;
    }

    /* renamed from: c */
    public final upgqDBbsrL<R, C> m1349c() {
        return this.f1329d;
    }

    /* renamed from: d */
    public final String m1350d() {
        return this.f1326a;
    }

    /* renamed from: e */
    public final Map<String, Object> m1351e() {
        return this.f1327b.f1322a;
    }
}
