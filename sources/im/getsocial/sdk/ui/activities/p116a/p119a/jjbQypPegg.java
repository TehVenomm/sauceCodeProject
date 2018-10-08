package im.getsocial.sdk.ui.activities.p116a.p119a;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPostContent;
import im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg.upgqDBbsrL;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.a.jjbQypPegg */
class jjbQypPegg extends im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.jjbQypPegg implements upgqDBbsrL<ActivityPost> {
    /* renamed from: b */
    private final im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> f2620b;
    /* renamed from: c */
    private ActivityPost f2621c;
    /* renamed from: d */
    private boolean f2622d;

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.jjbQypPegg$1 */
    class C10781 implements Callback<List<ActivityPost>> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2608a;

        C10781(jjbQypPegg jjbqyppegg) {
            this.f2608a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (!this.f2608a.m2799e(getSocialException)) {
                ((upgqDBbsrL.upgqDBbsrL) this.f2608a.m2521j()).m2708i();
                ((upgqDBbsrL.upgqDBbsrL) this.f2608a.m2521j()).mo4630b(getSocialException);
            }
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2608a.mo4649d().mo4585a((List) obj);
            ((upgqDBbsrL.upgqDBbsrL) this.f2608a.m2521j()).m2708i();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.jjbQypPegg$2 */
    class C10792 implements Callback<ActivityPost> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2609a;

        C10792(jjbQypPegg jjbqyppegg) {
            this.f2609a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2609a.m2521j()).m2684a(getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            ActivityPost activityPost = (ActivityPost) obj;
            this.f2609a.m2803k();
            this.f2609a.mo4649d().mo4587b(Collections.singletonList(activityPost));
            ((upgqDBbsrL.upgqDBbsrL) this.f2609a.m2521j()).mo4639n();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.jjbQypPegg$4 */
    class C10814 implements Callback<ActivityPost> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2613a;

        C10814(jjbQypPegg jjbqyppegg) {
            this.f2613a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            this.f2613a.m2799e(getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2613a.f2620b.mo4586b((ActivityPost) obj);
        }
    }

    jjbQypPegg(String str, ActivityPost activityPost, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        super(str);
        this.f2621c = activityPost;
        this.f2620b = jjbqyppegg;
        this.f2620b.mo4584a(activityPost, this);
        m2803k();
        m2784d(this.f2620b.mo4582a());
    }

    /* renamed from: e */
    private boolean m2799e(GetSocialException getSocialException) {
        if (getSocialException.getErrorCode() != ErrorCode.NOT_FOUND) {
            return false;
        }
        this.f2620b.mo4588c(this.f2621c);
        ((upgqDBbsrL.upgqDBbsrL) m2521j()).m2580a((Throwable) getSocialException);
        return true;
    }

    /* renamed from: k */
    private void m2803k() {
        GetSocial.getActivity(this.f2621c.getId(), new C10814(this));
    }

    /* renamed from: a */
    public final void mo4652a(ActivityPost activityPost) {
        this.f2620b.mo4586b(im.getsocial.sdk.ui.activities.p116a.p127e.jjbQypPegg.m3066b(this.f2621c));
        super.mo4652a(activityPost);
    }

    /* renamed from: a */
    public final void mo4653a(ActivityPostContent activityPostContent) {
        GetSocial.postCommentToActivity(this.f2621c.getId(), activityPostContent, new C10792(this));
    }

    /* renamed from: a */
    public final /* synthetic */ void mo4654a(Object obj, Object obj2) {
        ActivityPost activityPost = (ActivityPost) obj2;
        if (activityPost != null) {
            this.f2621c = activityPost;
            ((upgqDBbsrL.upgqDBbsrL) m2521j()).a_(this.f2621c);
        } else if (this.f2622d) {
            ((upgqDBbsrL.upgqDBbsrL) m2521j()).mo4633d();
        }
    }

    /* renamed from: a */
    public final void mo4655a(boolean z) {
        final ActivityPost activityPost = this.f2621c;
        final Object a = im.getsocial.sdk.ui.activities.p116a.p127e.jjbQypPegg.m3065a(activityPost);
        this.f2620b.mo4586b(a);
        GetSocial.likeActivity(activityPost.getId(), z, new Callback<ActivityPost>(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f2612c;

            public void onFailure(GetSocialException getSocialException) {
                if (a == this.f2612c.mo4651i()) {
                    this.f2612c.f2620b.mo4586b(activityPost);
                }
                if (!this.f2612c.m2799e(getSocialException) && !this.f2612c.m2785d(getSocialException)) {
                    ((upgqDBbsrL.upgqDBbsrL) this.f2612c.m2521j()).m2580a((Throwable) getSocialException);
                }
            }

            public /* synthetic */ void onSuccess(Object obj) {
                obj = (ActivityPost) obj;
                if (this.f2612c.mo4651i() == a) {
                    this.f2612c.f2620b.mo4586b(obj);
                }
            }
        });
    }

    /* renamed from: a */
    protected final boolean mo4656a(GetSocialException getSocialException) {
        return m2799e(getSocialException);
    }

    /* renamed from: b */
    public final void mo4657b(ActivityPost activityPost) {
        super.mo4652a(activityPost);
        this.f2622d = true;
        this.f2620b.mo4588c(activityPost);
    }

    /* renamed from: f */
    protected final ActivitiesQuery mo4658f() {
        return ActivitiesQuery.commentsToPost(this.f2621c.getId()).withLimit(15);
    }

    /* renamed from: h */
    public final void mo4659h() {
        GetSocial.getActivities(mo4658f(), new C10781(this));
    }

    /* renamed from: i */
    public final ActivityPost mo4651i() {
        return this.f2621c;
    }
}
