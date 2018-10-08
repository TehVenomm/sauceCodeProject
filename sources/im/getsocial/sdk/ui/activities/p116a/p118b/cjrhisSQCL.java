package im.getsocial.sdk.ui.activities.p116a.p118b;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.upgqDBbsrL;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.b.cjrhisSQCL */
public class cjrhisSQCL extends jjbQypPegg {

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.cjrhisSQCL$1 */
    class C10991 implements Callback<List<ActivityPost>> {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2673a;

        C10991(cjrhisSQCL cjrhissqcl) {
            this.f2673a = cjrhissqcl;
        }

        public void onFailure(GetSocialException getSocialException) {
            ((upgqDBbsrL) this.f2673a.m2521j()).m2708i();
            ((upgqDBbsrL) this.f2673a.m2521j()).mo4630b(getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            this.f2673a.mo4649d().mo4587b((List) obj);
            ((upgqDBbsrL) this.f2673a.m2521j()).m2708i();
        }
    }

    public cjrhisSQCL(String str, zoToeBNOjF zotoebnojf) {
        super(str, zotoebnojf);
    }

    /* renamed from: h */
    public final void mo4659h() {
        GetSocial.getActivities(mo4658f(), new C10991(this));
    }
}
