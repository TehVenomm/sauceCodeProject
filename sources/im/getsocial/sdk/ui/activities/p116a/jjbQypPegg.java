package im.getsocial.sdk.ui.activities.p116a;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocial.User;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivitiesQuery.Filter;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p121d.pdwpUtZXDT;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.upgqDBbsrL;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.jjbQypPegg */
public abstract class jjbQypPegg<P extends upgqDBbsrL> extends im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.jjbQypPegg<P> implements pdwpUtZXDT, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.jjbQypPegg<ActivityPost>, im.getsocial.sdk.ui.activities.p116a.p122h.upgqDBbsrL {
    /* renamed from: b */
    private static final cjrhisSQCL f2614b = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg f2615a;
    /* renamed from: c */
    private final im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> f2616c = new C11221(this);
    /* renamed from: d */
    private final im.getsocial.sdk.ui.activities.p116a.p121d.cjrhisSQCL f2617d;
    /* renamed from: e */
    private final im.getsocial.sdk.ui.activities.p116a.p122h.cjrhisSQCL f2618e;
    /* renamed from: f */
    private final String f2619f;

    /* renamed from: im.getsocial.sdk.ui.activities.a.jjbQypPegg$1 */
    class C11221 extends im.getsocial.sdk.ui.activities.p116a.p117f.upgqDBbsrL<String, ActivityPost> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2755a;

        C11221(jjbQypPegg jjbqyppegg) {
            this.f2755a = jjbqyppegg;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4590a(Object obj) {
            return ((ActivityPost) obj).getId();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.jjbQypPegg$3 */
    class C11243 implements Callback<List<ActivityPost>> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2759a;

        C11243(jjbQypPegg jjbqyppegg) {
            this.f2759a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (!this.f2759a.mo4656a(getSocialException)) {
                ((upgqDBbsrL) this.f2759a.m2521j()).mo4637l();
                ((upgqDBbsrL) this.f2759a.m2521j()).m2580a((Throwable) getSocialException);
            }
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2759a.mo4649d().mo4585a((List) obj);
            ((upgqDBbsrL) this.f2759a.m2521j()).mo4637l();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.jjbQypPegg$4 */
    class C11254 implements Callback<List<ActivityPost>> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2760a;

        C11254(jjbQypPegg jjbqyppegg) {
            this.f2760a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (!this.f2760a.mo4656a(getSocialException)) {
                ((upgqDBbsrL) this.f2760a.m2521j()).mo4638m();
                ((upgqDBbsrL) this.f2760a.m2521j()).m2580a((Throwable) getSocialException);
            }
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2760a.mo4649d().mo4587b((List) obj);
            ((upgqDBbsrL) this.f2760a.m2521j()).mo4638m();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.jjbQypPegg$6 */
    class C11276 implements CompletionCallback {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2763a;

        C11276(jjbQypPegg jjbqyppegg) {
            this.f2763a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            jjbQypPegg.f2614b.mo4388a("could not delete activity, error: ", getSocialException);
        }

        public void onSuccess() {
        }
    }

    public jjbQypPegg(String str) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f2616c.mo4583a((im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.jjbQypPegg) this);
        this.f2619f = str;
        this.f2617d = new im.getsocial.sdk.ui.activities.p116a.p121d.cjrhisSQCL(this);
        this.f2618e = new im.getsocial.sdk.ui.activities.p116a.p122h.cjrhisSQCL(this, str);
    }

    /* renamed from: a */
    static /* synthetic */ boolean m2760a(jjbQypPegg jjbqyppegg, GetSocialException getSocialException, ActivityPost activityPost) {
        if (getSocialException.getErrorCode() != ErrorCode.NOT_FOUND) {
            return false;
        }
        jjbqyppegg.f2616c.mo4588c(activityPost);
        ((upgqDBbsrL) jjbqyppegg.m2521j()).m2580a((Throwable) getSocialException);
        return true;
    }

    /* renamed from: i */
    private List<ActivityPost> mo4651i() {
        return this.f2616c.mo4582a();
    }

    /* renamed from: a */
    protected final void mo4641a() {
        im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL b = this.f2615a.m1378b();
        if (b != null) {
            im.getsocial.sdk.internal.p033c.ztWNWCuZiM e = b.m1325e();
            this.f2617d.m3055a(new im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg("app", e.m1569a(), e.m1570b()));
            this.f2617d.m3055a(new im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg(User.getId(), User.getDisplayName(), User.getAvatarUrl()));
        }
    }

    /* renamed from: a */
    public void mo4652a(ActivityPost activityPost) {
        this.f2616c.mo4588c(activityPost);
        GetSocial.deleteActivity(activityPost.getId(), new C11276(this));
    }

    /* renamed from: a */
    public final void m2771a(final ActivityPost activityPost, ReportingReason reportingReason) {
        GetSocial.reportActivity(activityPost.getId(), reportingReason, new CompletionCallback(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2762b;

            public void onFailure(GetSocialException getSocialException) {
                jjbQypPegg.m2760a(this.f2762b, getSocialException, activityPost);
            }

            public void onSuccess() {
                ((upgqDBbsrL) this.f2762b.m2521j()).mo4627j();
            }
        });
    }

    /* renamed from: a */
    public final void m2772a(final ActivityPost activityPost, boolean z) {
        final Object a = im.getsocial.sdk.ui.activities.p116a.p127e.jjbQypPegg.m3065a(activityPost);
        this.f2616c.mo4586b(a);
        GetSocial.likeActivity(activityPost.getId(), z, new Callback<ActivityPost>(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2758c;

            public void onFailure(GetSocialException getSocialException) {
                if (a == this.f2758c.mo4649d().mo4589d(a.getId())) {
                    this.f2758c.mo4649d().mo4586b(activityPost);
                }
                if (!this.f2758c.m2785d(getSocialException)) {
                    jjbQypPegg.m2760a(this.f2758c, getSocialException, activityPost);
                }
            }

            public /* synthetic */ void onSuccess(Object obj) {
                obj = (ActivityPost) obj;
                if (a == this.f2758c.mo4649d().mo4589d(a.getId())) {
                    this.f2758c.mo4649d().mo4586b(obj);
                }
            }
        });
    }

    /* renamed from: a */
    public final void mo4642a(String str) {
        this.f2617d.m3047c(str);
    }

    /* renamed from: a */
    public final void mo4643a(List<im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg> list) {
        ((upgqDBbsrL) m2521j()).mo4620a(list);
    }

    /* renamed from: a */
    protected abstract boolean mo4656a(GetSocialException getSocialException);

    /* renamed from: b */
    public final void m2776b() {
        ActivityPost activityPost;
        ActivitiesQuery f = mo4658f();
        List i = mo4651i();
        for (int size = i.size() - 1; size >= 0; size--) {
            activityPost = (ActivityPost) i.get(size);
            if (activityPost.getStickyEnd() <= 0 && activityPost.getStickyStart() <= 0) {
                break;
            }
        }
        activityPost = null;
        if (activityPost != null) {
            f.withFilter(Filter.OLDER, activityPost.getId());
        }
        GetSocial.getActivities(f, new C11243(this));
    }

    /* renamed from: b */
    public final void mo4644b(GetSocialException getSocialException) {
        ((upgqDBbsrL) m2521j()).m2580a((Throwable) getSocialException);
        ((upgqDBbsrL) m2521j()).mo4620a(Collections.emptyList());
    }

    /* renamed from: b */
    public final void mo4645b(String str) {
        this.f2618e.m3047c(str);
    }

    /* renamed from: b */
    public final void mo4646b(List<String> list) {
        upgqDBbsrL upgqdbbsrl = (upgqDBbsrL) m2521j();
        List arrayList = new ArrayList();
        for (String jjbqyppegg : list) {
            arrayList.add(new im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg(jjbqyppegg));
        }
        upgqdbbsrl.mo4621b(arrayList);
    }

    /* renamed from: c */
    public final void m2780c() {
        ActivityPost activityPost;
        ActivitiesQuery f = mo4658f();
        List i = mo4651i();
        for (int i2 = 0; i2 < i.size(); i2++) {
            activityPost = (ActivityPost) i.get(i2);
            if (activityPost.getStickyEnd() <= 0 && activityPost.getStickyStart() <= 0) {
                break;
            }
        }
        activityPost = null;
        if (activityPost != null) {
            f.withFilter(Filter.NEWER, activityPost.getId());
        }
        GetSocial.getActivities(f, new C11254(this));
    }

    /* renamed from: c */
    public final void mo4647c(GetSocialException getSocialException) {
        ((upgqDBbsrL) m2521j()).m2580a((Throwable) getSocialException);
        ((upgqDBbsrL) m2521j()).mo4621b(Collections.emptyList());
    }

    /* renamed from: c */
    public final void mo4648c(List<ActivityPost> list) {
        m2784d((List) list);
        ((upgqDBbsrL) m2521j()).mo4631c(list);
    }

    /* renamed from: d */
    public final im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> mo4649d() {
        return this.f2616c;
    }

    /* renamed from: d */
    protected final void m2784d(List<ActivityPost> list) {
        for (ActivityPost author : list) {
            this.f2617d.m3056a(author.getAuthor());
        }
    }

    /* renamed from: d */
    protected final boolean m2785d(GetSocialException getSocialException) {
        if (getSocialException.getErrorCode() != ErrorCode.USER_IS_BANNED) {
            return false;
        }
        ((upgqDBbsrL) m2521j()).m2580a((Throwable) getSocialException);
        return true;
    }

    /* renamed from: e */
    public final String mo4650e() {
        return this.f2619f;
    }

    /* renamed from: f */
    protected abstract ActivitiesQuery mo4658f();
}
