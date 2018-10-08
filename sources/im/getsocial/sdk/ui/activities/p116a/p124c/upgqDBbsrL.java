package im.getsocial.sdk.ui.activities.p116a.p124c;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.jjbQypPegg;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.upgqDBbsrL */
class upgqDBbsrL extends jjbQypPegg {
    /* renamed from: a */
    private final List<PublicUser> f2716a = new ArrayList();
    /* renamed from: b */
    private final ActivityPost f2717b;
    /* renamed from: c */
    private final im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> f2718c;

    /* renamed from: im.getsocial.sdk.ui.activities.a.c.upgqDBbsrL$1 */
    class C11141 implements Callback<List<PublicUser>> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2714a;

        C11141(upgqDBbsrL upgqdbbsrl) {
            this.f2714a = upgqdbbsrl;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (getSocialException.getErrorCode() == ErrorCode.NOT_FOUND) {
                this.f2714a.f2718c.mo4588c(this.f2714a.f2717b);
                ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2714a.m2521j()).mo4703e();
                return;
            }
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2714a.m2521j()).mo4699a(getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2714a.f2716a.addAll((List) obj);
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2714a.m2521j()).mo4701a(this.f2714a.f2716a);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.c.upgqDBbsrL$2 */
    class C11152 implements Callback<List<PublicUser>> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2715a;

        C11152(upgqDBbsrL upgqdbbsrl) {
            this.f2715a = upgqdbbsrl;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (getSocialException.getErrorCode() == ErrorCode.NOT_FOUND) {
                this.f2715a.f2718c.mo4588c(this.f2715a.f2717b);
                ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2715a.m2521j()).mo4703e();
                return;
            }
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2715a.m2521j()).mo4702d();
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2715a.m2521j()).m2580a((Throwable) getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2715a.f2716a.addAll((List) obj);
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2715a.m2521j()).mo4701a(this.f2715a.f2716a);
            ((im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL) this.f2715a.m2521j()).mo4702d();
        }
    }

    upgqDBbsrL(ActivityPost activityPost, im.getsocial.sdk.ui.activities.p116a.p117f.jjbQypPegg<String, ActivityPost> jjbqyppegg) {
        this.f2717b = activityPost;
        this.f2718c = jjbqyppegg;
    }

    /* renamed from: b */
    public final void mo4705b() {
        GetSocial.getActivityLikers(this.f2717b.getId(), 0, 10, new C11141(this));
    }

    /* renamed from: c */
    public final void mo4706c() {
        GetSocial.getActivityLikers(this.f2717b.getId(), this.f2716a.size(), 10, new C11152(this));
    }
}
