package im.getsocial.sdk.ui.activities.p116a;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView.OnActivityEventListener;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.cjrhisSQCL */
public class cjrhisSQCL extends ArrayAdapter<ActivityPost> {
    /* renamed from: a */
    private final jjbQypPegg f2631a;

    /* renamed from: im.getsocial.sdk.ui.activities.a.cjrhisSQCL$jjbQypPegg */
    public static class jjbQypPegg {
        protected jjbQypPegg() {
        }

        /* renamed from: a */
        public void mo4600a(ActivityPost activityPost) {
        }

        /* renamed from: a */
        public void mo4601a(PublicUser publicUser) {
        }

        /* renamed from: a */
        public void mo4602a(String str) {
        }

        /* renamed from: b */
        public void mo4603b(ActivityPost activityPost) {
        }

        /* renamed from: b */
        public void mo4604b(String str) {
        }

        /* renamed from: c */
        public void mo4690c(ActivityPost activityPost) {
        }

        /* renamed from: d */
        public void mo4605d(ActivityPost activityPost) {
        }

        /* renamed from: e */
        public void mo4691e(ActivityPost activityPost) {
        }

        /* renamed from: f */
        public void mo4606f(ActivityPost activityPost) {
        }

        /* renamed from: g */
        public void mo4607g(ActivityPost activityPost) {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.cjrhisSQCL$upgqDBbsrL */
    private class upgqDBbsrL extends OnActivityEventListener {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f2721a;
        /* renamed from: b */
        private final ActivityPost f2722b;
        /* renamed from: c */
        private final int f2723c;

        upgqDBbsrL(cjrhisSQCL cjrhissqcl, int i, ActivityPost activityPost) {
            this.f2721a = cjrhissqcl;
            this.f2723c = i;
            this.f2722b = activityPost;
        }

        /* renamed from: a */
        public final void mo4608a() {
            this.f2721a.f2631a.mo4600a(this.f2722b);
        }

        /* renamed from: a */
        public final void mo4609a(String str) {
            this.f2721a.f2631a.mo4602a(str);
        }

        /* renamed from: b */
        public final void mo4610b() {
            this.f2721a.f2631a.mo4603b(this.f2722b);
        }

        /* renamed from: b */
        public final void mo4611b(String str) {
            this.f2721a.f2631a.mo4604b(str);
        }

        /* renamed from: c */
        public final void mo4707c() {
            this.f2721a.f2631a.mo4690c(this.f2722b);
        }

        /* renamed from: d */
        public final void mo4612d() {
            this.f2721a.f2631a.mo4605d(this.f2722b);
        }

        /* renamed from: e */
        public final void mo4708e() {
            this.f2721a.f2631a.mo4691e(this.f2722b);
        }

        /* renamed from: f */
        public final void mo4613f() {
            this.f2721a.f2631a.mo4601a(this.f2722b.getAuthor());
        }

        /* renamed from: g */
        public final void mo4614g() {
            if (this.f2722b.hasButton()) {
                this.f2721a.f2631a.mo4603b(this.f2722b);
            } else {
                this.f2721a.f2631a.mo4691e(this.f2722b);
            }
        }

        /* renamed from: h */
        public final void mo4615h() {
            this.f2721a.f2631a.mo4607g(this.f2722b);
        }

        /* renamed from: i */
        public final void mo4709i() {
            this.f2721a.f2631a.mo4606f(this.f2722b);
        }
    }

    public cjrhisSQCL(Context context, List<ActivityPost> list, jjbQypPegg jjbqyppegg) {
        super(context, 0, list);
        this.f2631a = jjbqyppegg;
    }

    /* renamed from: a */
    protected void mo4662a(ActivityContainerView activityContainerView) {
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        ActivityContainerView activityContainerView = view == null ? new ActivityContainerView(getContext()) : (ActivityContainerView) view;
        ActivityPost activityPost = (ActivityPost) getItem(i);
        mo4662a(activityContainerView);
        activityContainerView.m3478a(activityPost);
        activityContainerView.m3479a(new upgqDBbsrL(this, i, activityPost));
        return activityContainerView;
    }
}
