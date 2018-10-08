package im.getsocial.sdk.ui.activities.p116a.p119a;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.ui.UiAction;
import im.getsocial.sdk.ui.UiAction.Pending;
import im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.jjbQypPegg;
import im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.upgqDBbsrL;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.pdwpUtZXDT;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView.OnActivityEventListener;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.a.cjrhisSQCL */
class cjrhisSQCL extends upgqDBbsrL {
    /* renamed from: a */
    private String f2606a;
    /* renamed from: f */
    private boolean f2607f = false;

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.cjrhisSQCL$jjbQypPegg */
    private final class jjbQypPegg extends im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.upgqDBbsrL {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2597a;

        private jjbQypPegg(cjrhisSQCL cjrhissqcl) {
            this.f2597a = cjrhissqcl;
        }

        /* renamed from: a */
        public final void mo4600a(final ActivityPost activityPost) {
            this.f2597a.b.onUiAction(UiAction.LIKE_COMMENT, new Pending(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f2596b;

                public void proceed() {
                    ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.jjbQypPegg) this.f2596b.f2597a.m2593y()).m2772a(activityPost, !activityPost.isLikedByMe());
                }
            });
        }

        /* renamed from: a */
        public final void mo4601a(PublicUser publicUser) {
            this.f2597a.m2693a(publicUser);
        }

        /* renamed from: a */
        public final void mo4602a(String str) {
            this.f2597a.m2694a(str);
        }

        /* renamed from: b */
        public final void mo4603b(ActivityPost activityPost) {
            this.f2597a.m2685a(activityPost);
        }

        /* renamed from: b */
        public final void mo4604b(String str) {
            this.f2597a.m2697b(str);
        }

        /* renamed from: d */
        public final void mo4605d(ActivityPost activityPost) {
            this.f2597a.m2700d(activityPost);
        }

        /* renamed from: f */
        public final void mo4606f(ActivityPost activityPost) {
            this.f2597a.m2696b(activityPost);
        }

        /* renamed from: g */
        public final void mo4607g(ActivityPost activityPost) {
            this.f2597a.m2702e(activityPost);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.cjrhisSQCL$upgqDBbsrL */
    private final class upgqDBbsrL extends OnActivityEventListener {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2599a;

        /* renamed from: im.getsocial.sdk.ui.activities.a.a.cjrhisSQCL$upgqDBbsrL$1 */
        class C10771 implements Pending {
            /* renamed from: a */
            final /* synthetic */ upgqDBbsrL f2598a;

            C10771(upgqDBbsrL upgqdbbsrl) {
                this.f2598a = upgqdbbsrl;
            }

            public void proceed() {
                ((jjbQypPegg) this.f2598a.f2599a.m2593y()).mo4655a(!this.f2598a.f2599a.m2712E().isLikedByMe());
            }
        }

        private upgqDBbsrL(cjrhisSQCL cjrhissqcl) {
            this.f2599a = cjrhissqcl;
        }

        /* renamed from: a */
        public final void mo4608a() {
            this.f2599a.b.onUiAction(UiAction.LIKE_ACTIVITY, new C10771(this));
        }

        /* renamed from: a */
        public final void mo4609a(String str) {
            this.f2599a.m2694a(str);
        }

        /* renamed from: b */
        public final void mo4610b() {
            this.f2599a.m2685a(this.f2599a.m2712E());
        }

        /* renamed from: b */
        public final void mo4611b(String str) {
            this.f2599a.m2697b(str);
        }

        /* renamed from: d */
        public final void mo4612d() {
            this.f2599a.m2699c(this.f2599a.m2712E());
        }

        /* renamed from: f */
        public final void mo4613f() {
            this.f2599a.m2693a(this.f2599a.m2712E().getAuthor());
        }

        /* renamed from: g */
        public final void mo4614g() {
            this.f2599a.m2685a(this.f2599a.m2712E());
        }

        /* renamed from: h */
        public final void mo4615h() {
            this.f2599a.m2702e(this.f2599a.m2712E());
        }
    }

    cjrhisSQCL(im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL cjrhissqcl, jjbQypPegg jjbqyppegg, String str) {
        super(cjrhissqcl, jjbqyppegg);
        this.f2606a = str;
    }

    /* renamed from: E */
    private ActivityPost m2712E() {
        return ((jjbQypPegg) m2593y()).mo4651i();
    }

    /* renamed from: a */
    static /* synthetic */ void m2714a(cjrhisSQCL cjrhissqcl, ActivityPost activityPost) {
        if (activityPost == cjrhissqcl.m2712E()) {
            ((jjbQypPegg) cjrhissqcl.m2593y()).mo4657b(activityPost);
        } else {
            ((jjbQypPegg) cjrhissqcl.m2593y()).mo4652a(activityPost);
        }
    }

    /* renamed from: a */
    public final String mo4591a() {
        return this.c.strings().CommentsTitle;
    }

    /* renamed from: a */
    protected final void mo4628a(Pending pending) {
        this.b.onUiAction(UiAction.POST_COMMENT, pending);
    }

    public final void a_(ActivityPost activityPost) {
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4664a(activityPost);
    }

    /* renamed from: b */
    public final String mo4592b() {
        return "comments";
    }

    /* renamed from: b */
    public final void mo4630b(GetSocialException getSocialException) {
        m2580a((Throwable) getSocialException);
    }

    /* renamed from: c */
    public final void mo4631c(List<ActivityPost> list) {
        List arrayList = new ArrayList(list);
        Collections.reverse(arrayList);
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4670a(arrayList);
        if (this.f2606a != null) {
            ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4668a(this.f2606a);
            this.f2606a = null;
        }
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4674b(arrayList.size() < m2712E().getCommentsCount());
    }

    public final void c_() {
        if (!this.f2607f) {
            this.f2607f = true;
            ((jjbQypPegg) m2593y()).m2776b();
        }
    }

    /* renamed from: d */
    public final void mo4633d() {
        m2591w();
    }

    public final void d_() {
        super.d_();
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4664a(m2712E());
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4667a(new upgqDBbsrL());
    }

    /* renamed from: f */
    protected final void mo4634f(final ActivityPost activityPost) {
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4665a(new pdwpUtZXDT(this) {
            /* renamed from: b */
            final /* synthetic */ cjrhisSQCL f2592b;

            /* renamed from: a */
            public final void mo4597a(ReportingReason reportingReason) {
                ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) this.f2592b.mo4733t()).m2540u();
                ((jjbQypPegg) this.f2592b.m2593y()).m2771a(activityPost, reportingReason);
            }
        });
    }

    /* renamed from: g */
    protected final void mo4635g(final ActivityPost activityPost) {
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4669a(activityPost == m2712E() ? this.c.strings().DeleteActivity : this.c.strings().DeleteComment, new im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.cjrhisSQCL(this) {
            /* renamed from: b */
            final /* synthetic */ cjrhisSQCL f2594b;

            /* renamed from: a */
            public final void mo4598a() {
                cjrhisSQCL.m2714a(this.f2594b, activityPost);
            }
        });
    }

    /* renamed from: k */
    protected final im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.upgqDBbsrL mo4636k() {
        return new jjbQypPegg();
    }

    /* renamed from: l */
    public final void mo4637l() {
        this.f2607f = false;
    }

    /* renamed from: m */
    public final void mo4638m() {
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4663c();
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4683k();
    }

    /* renamed from: n */
    public final void mo4639n() {
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).m2541v();
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4672b();
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).m2544y();
        ((im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL) mo4733t()).mo4683k();
    }

    /* renamed from: o */
    public final void mo4640o() {
        ((jjbQypPegg) m2593y()).m2780c();
    }
}
