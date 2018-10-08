package im.getsocial.sdk.ui.activities.p116a.p118b;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.ui.UiAction;
import im.getsocial.sdk.ui.UiAction.Pending;
import im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.jjbQypPegg;
import im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.upgqDBbsrL;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.List;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.ui.activities.a.b.pdwpUtZXDT */
public class pdwpUtZXDT extends upgqDBbsrL {
    /* renamed from: a */
    private final ScheduledExecutorService f2697a = Executors.newSingleThreadScheduledExecutor();
    /* renamed from: f */
    private ScheduledFuture<?> f2698f;
    /* renamed from: g */
    private final boolean f2699g;

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.pdwpUtZXDT$4 */
    class C11094 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2693a;

        C11094(pdwpUtZXDT pdwputzxdt) {
            this.f2693a = pdwputzxdt;
        }

        public void run() {
            ((jjbQypPegg) this.f2693a.m2593y()).m2780c();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.pdwpUtZXDT$jjbQypPegg */
    private final class jjbQypPegg extends cjrhisSQCL.upgqDBbsrL {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2696a;

        private jjbQypPegg(pdwpUtZXDT pdwputzxdt) {
            this.f2696a = pdwputzxdt;
        }

        /* renamed from: a */
        public final void mo4600a(final ActivityPost activityPost) {
            this.f2696a.b.onUiAction(UiAction.LIKE_ACTIVITY, new Pending(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f2695b;

                public void proceed() {
                    ((im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.jjbQypPegg) this.f2695b.f2696a.m2593y()).m2772a(activityPost, !activityPost.isLikedByMe());
                }
            });
        }

        /* renamed from: a */
        public final void mo4601a(PublicUser publicUser) {
            this.f2696a.m2693a(publicUser);
        }

        /* renamed from: a */
        public final void mo4602a(String str) {
            this.f2696a.m2694a(str);
        }

        /* renamed from: b */
        public final void mo4603b(ActivityPost activityPost) {
            this.f2696a.m2685a(activityPost);
        }

        /* renamed from: b */
        public final void mo4604b(String str) {
            this.f2696a.m2697b(str);
        }

        /* renamed from: c */
        public final void mo4690c(ActivityPost activityPost) {
            pdwpUtZXDT.m2954b(this.f2696a, activityPost);
        }

        /* renamed from: d */
        public final void mo4605d(ActivityPost activityPost) {
            this.f2696a.m2699c(activityPost);
        }

        /* renamed from: e */
        public final void mo4691e(ActivityPost activityPost) {
            pdwpUtZXDT.m2954b(this.f2696a, activityPost);
        }

        /* renamed from: f */
        public final void mo4606f(ActivityPost activityPost) {
            this.f2696a.m2696b(activityPost);
        }

        /* renamed from: g */
        public final void mo4607g(ActivityPost activityPost) {
            this.f2696a.m2702e(activityPost);
        }
    }

    public pdwpUtZXDT(upgqDBbsrL.cjrhisSQCL cjrhissqcl, jjbQypPegg jjbqyppegg, boolean z) {
        super(cjrhissqcl, jjbqyppegg);
        this.f2699g = z;
    }

    /* renamed from: E */
    private void m2947E() {
        this.f2698f = this.f2697a.scheduleAtFixedRate(new C11094(this), 30, 30, TimeUnit.SECONDS);
    }

    /* renamed from: F */
    private void m2948F() {
        if (this.f2698f != null) {
            this.f2698f.cancel(true);
            this.f2698f = null;
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m2954b(pdwpUtZXDT pdwputzxdt, final ActivityPost activityPost) {
        if (((upgqDBbsrL.cjrhisSQCL) pdwputzxdt.mo4733t()).m2528A()) {
            pdwputzxdt.b.onUiAction(UiAction.OPEN_COMMENTS, new Pending(pdwputzxdt) {
                /* renamed from: b */
                final /* synthetic */ pdwpUtZXDT f2692b;

                public void proceed() {
                    im.getsocial.sdk.ui.internal.upgqDBbsrL.m3461a();
                    im.getsocial.sdk.ui.internal.upgqDBbsrL.m3463a(this.f2692b, activityPost, (im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.jjbQypPegg) this.f2692b.m2593y(), this.f2692b.f2699g);
                }
            });
        }
    }

    /* renamed from: a */
    public final String mo4591a() {
        return this.c.strings().ActivityTitle;
    }

    /* renamed from: a */
    protected final void mo4628a(Pending pending) {
        this.b.onUiAction(UiAction.POST_ACTIVITY, pending);
    }

    /* renamed from: b */
    public final String mo4592b() {
        return "activities";
    }

    /* renamed from: b */
    public final void mo4630b(GetSocialException getSocialException) {
        m2580a((Throwable) getSocialException);
        if (((jjbQypPegg) m2593y()).mo4649d().mo4582a().isEmpty()) {
            ((upgqDBbsrL.cjrhisSQCL) mo4733t()).m2534a(this.c.strings().ConnectionLostTitle, this.c.strings().ConnectionLostMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3142z().m3173a());
        }
    }

    /* renamed from: c */
    public final void mo4692c() {
        ((jjbQypPegg) m2593y()).m2780c();
    }

    /* renamed from: c */
    public final void mo4631c(List<ActivityPost> list) {
        if (list.isEmpty()) {
            ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4688l();
        } else {
            ((upgqDBbsrL.cjrhisSQCL) mo4733t()).m2543x();
            ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4689m();
        }
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4670a((List) list);
    }

    /* renamed from: f */
    protected final void mo4634f(final ActivityPost activityPost) {
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4665a(new im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.pdwpUtZXDT(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2688b;

            /* renamed from: a */
            public final void mo4597a(ReportingReason reportingReason) {
                ((upgqDBbsrL.cjrhisSQCL) this.f2688b.mo4733t()).m2540u();
                ((jjbQypPegg) this.f2688b.m2593y()).m2771a(activityPost, reportingReason);
            }
        });
    }

    /* renamed from: g */
    protected final void mo4635g(final ActivityPost activityPost) {
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4669a(this.c.strings().DeleteActivity, new cjrhisSQCL.cjrhisSQCL(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2690b;

            /* renamed from: a */
            public final void mo4598a() {
                ((jjbQypPegg) this.f2690b.m2593y()).mo4652a(activityPost);
            }
        });
    }

    /* renamed from: k */
    protected final cjrhisSQCL.upgqDBbsrL mo4636k() {
        return new jjbQypPegg();
    }

    /* renamed from: l */
    public final void mo4637l() {
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4663c();
    }

    /* renamed from: m */
    public final void mo4638m() {
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4687k();
    }

    /* renamed from: n */
    public final void mo4639n() {
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).m2541v();
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4672b();
        ((upgqDBbsrL.cjrhisSQCL) mo4733t()).m2544y();
    }

    /* renamed from: o */
    public final void mo4640o() {
        ((jjbQypPegg) m2593y()).m2776b();
    }

    /* renamed from: p */
    protected final void mo4693p() {
        m2947E();
    }

    /* renamed from: q */
    protected final void mo4694q() {
        m2948F();
    }

    /* renamed from: r */
    public final void mo4695r() {
        m2948F();
    }

    /* renamed from: s */
    public final void mo4696s() {
        m2947E();
    }
}
