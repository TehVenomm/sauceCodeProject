package im.getsocial.sdk.ui.activities.p116a.p118b;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPostContent;
import im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.KSZKMmRWhZ;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.b.jjbQypPegg */
public class jjbQypPegg extends im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.jjbQypPegg {
    /* renamed from: b */
    private final String f2674b;
    /* renamed from: c */
    private final zoToeBNOjF f2675c;

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.jjbQypPegg$3 */
    class C11053 implements Callback<ActivityPost> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2686a;

        C11053(jjbQypPegg jjbqyppegg) {
            this.f2686a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            ((upgqDBbsrL) this.f2686a.m2521j()).m2684a(getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2686a.mo4649d().mo4587b(Collections.singletonList((ActivityPost) obj));
            ((upgqDBbsrL) this.f2686a.m2521j()).mo4639n();
        }
    }

    public jjbQypPegg(String str, zoToeBNOjF zotoebnojf) {
        super(str);
        this.f2675c = zotoebnojf;
        this.f2674b = str;
    }

    /* renamed from: a */
    public final void mo4653a(ActivityPostContent activityPostContent) {
        GetSocial.postActivityToFeed(this.f2674b, activityPostContent, new C11053(this));
    }

    /* renamed from: a */
    protected final boolean mo4656a(GetSocialException getSocialException) {
        return false;
    }

    /* renamed from: f */
    protected final ActivitiesQuery mo4658f() {
        return this.f2675c.mo4596a();
    }

    /* renamed from: h */
    public void mo4659h() {
        final KSZKMmRWhZ kSZKMmRWhZ = new KSZKMmRWhZ(2);
        GetSocial.getAnnouncements(this.f2674b, new Callback<List<ActivityPost>>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2679b;

            public void onFailure(GetSocialException getSocialException) {
                kSZKMmRWhZ.m3297a();
                ((upgqDBbsrL) this.f2679b.m2521j()).m2580a((Throwable) getSocialException);
            }

            public /* synthetic */ void onSuccess(Object obj) {
                final List list = (List) obj;
                kSZKMmRWhZ.m3298a(0, new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ C11011 f2677b;

                    public void run() {
                        this.f2677b.f2679b.mo4649d().mo4587b(list);
                    }
                });
            }
        });
        GetSocial.getActivities(mo4658f(), new Callback<List<ActivityPost>>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2685b;

            public void onFailure(final GetSocialException getSocialException) {
                kSZKMmRWhZ.m3298a(1, new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ C11042 f2683b;

                    public void run() {
                        ((upgqDBbsrL) this.f2683b.f2685b.m2521j()).m2708i();
                        ((upgqDBbsrL) this.f2683b.f2685b.m2521j()).mo4630b(getSocialException);
                    }
                });
            }

            public /* synthetic */ void onSuccess(Object obj) {
                final List list = (List) obj;
                kSZKMmRWhZ.m3298a(1, new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ C11042 f2681b;

                    public void run() {
                        this.f2681b.f2685b.mo4649d().mo4585a(list);
                        ((upgqDBbsrL) this.f2681b.f2685b.m2521j()).m2708i();
                    }
                });
            }
        });
    }
}
