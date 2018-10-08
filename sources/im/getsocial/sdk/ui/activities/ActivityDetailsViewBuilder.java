package im.getsocial.sdk.ui.activities;

import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;
import im.getsocial.sdk.ui.GetSocialUi;
import im.getsocial.sdk.ui.ViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p119a.XdbacJlTDQ;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.cjrhisSQCL;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.upgqDBbsrL;
import java.util.Collections;

public class ActivityDetailsViewBuilder extends AbstractActivitiesViewBuilder<ActivityDetailsViewBuilder> {
    /* renamed from: f */
    private final String f2578f;
    /* renamed from: g */
    private String f2579g;
    /* renamed from: h */
    private boolean f2580h = true;
    /* renamed from: i */
    private boolean f2581i;

    /* renamed from: im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder$1 */
    class C10691 extends im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.jjbQypPegg {
        /* renamed from: a */
        final /* synthetic */ ActivityDetailsViewBuilder f2550a;

        C10691(ActivityDetailsViewBuilder activityDetailsViewBuilder) {
            this.f2550a = activityDetailsViewBuilder;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder$2 */
    class C10702 extends cjrhisSQCL {
        /* renamed from: a */
        final /* synthetic */ ActivityDetailsViewBuilder f2558a;

        C10702(ActivityDetailsViewBuilder activityDetailsViewBuilder) {
            this.f2558a = activityDetailsViewBuilder;
        }

        /* renamed from: a */
        protected final View mo4580a(ViewGroup viewGroup) {
            return new FrameLayout(m2526p());
        }

        /* renamed from: a */
        protected final void mo4581a() {
        }
    }

    private class jjbQypPegg extends upgqDBbsrL<cjrhisSQCL, im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ ActivityDetailsViewBuilder f2577a;

        /* renamed from: im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder$jjbQypPegg$1 */
        class C10721 implements Callback<ActivityPost> {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f2564a;

            /* renamed from: im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder$jjbQypPegg$1$1 */
            class C10711 extends im.getsocial.sdk.ui.activities.p116a.p117f.upgqDBbsrL<String, ActivityPost> {
                /* renamed from: a */
                final /* synthetic */ C10721 f2563a;

                C10711(C10721 c10721) {
                    this.f2563a = c10721;
                }

                /* renamed from: a */
                protected final /* synthetic */ Object mo4590a(Object obj) {
                    return ((ActivityPost) obj).getId();
                }
            }

            C10721(jjbQypPegg jjbqyppegg) {
                this.f2564a = jjbqyppegg;
            }

            /* renamed from: a */
            private <T extends AbstractActivitiesViewBuilder<T>> void m2568a(AbstractActivitiesViewBuilder<T> abstractActivitiesViewBuilder) {
                ((AbstractActivitiesViewBuilder) abstractActivitiesViewBuilder.setReadOnly(this.f2564a.f2577a.f2581i).setUiActionListener(this.f2564a.m2589u())).setMentionClickListener(this.f2564a.f2577a.m2517f()).setTagClickListener(this.f2564a.f2577a.m2518g()).setButtonActionListener(this.f2564a.f2577a.m2516e()).setAvatarClickListener(this.f2564a.f2577a.m2515d()).setWindowTitle(this.f2564a.f2577a.b);
            }

            public void onFailure(GetSocialException getSocialException) {
                ((cjrhisSQCL) this.f2564a.mo4733t()).m2541v();
                if (getSocialException.getErrorCode() == ErrorCode.NOT_FOUND) {
                    this.f2564a.m2580a((Throwable) getSocialException);
                } else {
                    GetSocialUi.closeView();
                }
            }

            public /* synthetic */ void onSuccess(Object obj) {
                ActivityPost activityPost = (ActivityPost) obj;
                ((cjrhisSQCL) this.f2564a.mo4733t()).m2541v();
                boolean z = this.f2564a.f2577a.f2580h && !ztWNWCuZiM.m1521a(activityPost.getFeedId());
                if (z) {
                    ViewBuilder create = ActivityFeedViewBuilder.create(activityPost.getFeedId());
                    m2568a(create);
                    im.getsocial.sdk.ui.upgqDBbsrL.m3626a(create, true);
                    im.getsocial.sdk.ui.upgqDBbsrL.m3628b(create, true);
                    create.show();
                }
                im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg c10711 = new C10711(this);
                c10711.mo4587b(Collections.singletonList(activityPost));
                ViewBuilder a = XdbacJlTDQ.m2612a(ztWNWCuZiM.m1521a(activityPost.getFeedId()) ? ActivitiesQuery.GLOBAL_FEED : activityPost.getFeedId(), activityPost, c10711).m2613a(this.f2564a.f2577a.f2579g);
                m2568a(a);
                im.getsocial.sdk.ui.upgqDBbsrL.m3626a(a, !z);
                a.show();
            }
        }

        public jjbQypPegg(ActivityDetailsViewBuilder activityDetailsViewBuilder, cjrhisSQCL cjrhissqcl, im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.jjbQypPegg jjbqyppegg) {
            this.f2577a = activityDetailsViewBuilder;
            super(cjrhissqcl, jjbqyppegg);
        }

        /* renamed from: a */
        protected final String mo4591a() {
            return this.c.strings().ActivityTitle;
        }

        /* renamed from: b */
        public final String mo4592b() {
            return "activities";
        }

        public final void d_() {
            ((cjrhisSQCL) mo4733t()).m2540u();
            GetSocial.getActivity(this.f2577a.f2578f, new C10721(this));
        }
    }

    private ActivityDetailsViewBuilder(String str) {
        this.f2578f = str;
    }

    public static ActivityDetailsViewBuilder create(String str) {
        return new ActivityDetailsViewBuilder(str);
    }

    /* renamed from: c */
    protected final upgqDBbsrL mo4594c() {
        return new jjbQypPegg(this, new C10702(this), new C10691(this));
    }

    public ActivityDetailsViewBuilder setCommentId(String str) {
        this.f2579g = str;
        return this;
    }

    public ActivityDetailsViewBuilder setReadOnly(boolean z) {
        this.f2581i = z;
        return this;
    }

    public ActivityDetailsViewBuilder setShowActivityFeedView(boolean z) {
        this.f2580h = z;
        return this;
    }
}
