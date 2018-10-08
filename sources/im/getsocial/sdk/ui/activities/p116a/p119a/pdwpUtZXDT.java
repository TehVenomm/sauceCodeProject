package im.getsocial.sdk.ui.activities.p116a.p119a;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.activities.p116a.p119a.upgqDBbsrL.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView.OnActivityEventListener;
import im.getsocial.sdk.ui.internal.views.AssetButton;
import im.getsocial.sdk.ui.internal.views.InputContainer;
import im.getsocial.sdk.ui.internal.views.InputContainer.Listener;
import im.getsocial.sdk.ui.internal.views.LoadingIndicator;
import im.getsocial.sdk.ui.internal.views.OverscrollingListView;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT */
class pdwpUtZXDT extends cjrhisSQCL {
    /* renamed from: a */
    private final List<ActivityPost> f2634a = new ArrayList();
    /* renamed from: d */
    private final boolean f2635d;
    /* renamed from: e */
    private OverscrollingListView f2636e;
    /* renamed from: f */
    private ArrayAdapter<ActivityPost> f2637f;
    /* renamed from: g */
    private InputContainer f2638g;
    /* renamed from: h */
    private ActivityContainerView f2639h;
    /* renamed from: i */
    private upgqDBbsrL f2640i;
    /* renamed from: j */
    private View f2641j;
    /* renamed from: k */
    private im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL f2642k;

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT$1 */
    class C10821 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2623a;

        C10821(pdwpUtZXDT pdwputzxdt) {
            this.f2623a = pdwputzxdt;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT$3 */
    class C10843 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2626a;

        C10843(pdwpUtZXDT pdwputzxdt) {
            this.f2626a = pdwputzxdt;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT$5 */
    class C10865 implements im.getsocial.sdk.ui.activities.p116a.p123g.pdwpUtZXDT<jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2629a;

        C10865(pdwpUtZXDT pdwputzxdt) {
            this.f2629a = pdwputzxdt;
        }

        /* renamed from: a */
        public final im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg<jjbQypPegg> mo4660a() {
            return new im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL(this.f2629a.m2526p());
        }

        /* renamed from: a */
        public final /* synthetic */ void mo4661a(Object obj) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2629a.m2524n()).m2691a((jjbQypPegg) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT$6 */
    class C10876 implements im.getsocial.sdk.ui.activities.p116a.p123g.pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2630a;

        C10876(pdwpUtZXDT pdwputzxdt) {
            this.f2630a = pdwputzxdt;
        }

        /* renamed from: a */
        public final im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> mo4660a() {
            return new im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT(this.f2630a.m2526p());
        }

        /* renamed from: a */
        public final /* synthetic */ void mo4661a(Object obj) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2630a.m2524n()).m2692a((im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.a.pdwpUtZXDT$8 */
    class C10898 implements View.OnClickListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2633a;

        C10898(pdwpUtZXDT pdwputzxdt) {
            this.f2633a = pdwputzxdt;
        }

        public void onClick(View view) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2633a.m2524n()).c_();
        }
    }

    pdwpUtZXDT(boolean z) {
        this.f2635d = z;
    }

    /* renamed from: l */
    private im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL m2849l() {
        if (this.f2642k == null) {
            this.f2642k = new im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL(m2526p());
            ((ViewGroup) m2536q()).addView(this.f2642k);
            KluUZYuxme.m3299a(m2526p()).m3311a(this.f2642k, false);
        }
        return this.f2642k;
    }

    /* renamed from: a */
    public final View mo4580a(ViewGroup viewGroup) {
        View inflate = LayoutInflater.from(m2526p()).inflate(C1067R.layout.contentview_comments, viewGroup);
        this.f2636e = new OverscrollingListView(m2526p(), null, null);
        ((FrameLayout) inflate.findViewById(C1067R.id.list_view_comments)).addView(this.f2636e);
        return inflate;
    }

    /* renamed from: a */
    public final void mo4581a() {
        this.f2638g = (InputContainer) m2529a(C1067R.id.post_comment_container, InputContainer.class);
        this.f2638g.m3510a((Listener) m2524n());
        if (this.f2635d) {
            this.f2638g.setVisibility(8);
        }
        this.f2638g.m3511a(this.b.strings().CommentsPostPlaceholder);
        this.f2638g.setBackgroundColor(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3134r().m3167a().m3215a());
        KluUZYuxme.m3299a(m2526p()).m3305a(this.f2638g);
        this.f2637f = new im.getsocial.sdk.ui.activities.p116a.cjrhisSQCL(this, m2526p(), this.f2634a, this.f2640i) {
            /* renamed from: a */
            final /* synthetic */ pdwpUtZXDT f2632a;

            /* renamed from: a */
            protected final void mo4662a(ActivityContainerView activityContainerView) {
                activityContainerView.setBackgroundColor(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3138v().m3167a().m3215a());
                activityContainerView.m3480b();
                activityContainerView.m3477a(100);
                if (this.f2632a.f2635d) {
                    activityContainerView.m3481c();
                }
            }
        };
        OverscrollingListView overscrollingListView = this.f2636e;
        View view = (LinearLayout) LayoutInflater.from(m2526p()).inflate(C1067R.layout.header_comments_view, null);
        this.f2639h = (ActivityContainerView) view.findViewById(C1067R.id.header_activity_post_view);
        if (this.f2635d) {
            this.f2639h.m3481c();
        }
        this.f2639h.m3476a();
        this.f2639h.m3477a(100);
        AssetButton assetButton = (AssetButton) view.findViewById(C1067R.id.load_more_button);
        assetButton.m3503a(this.b.strings().CommentsMoreCommentsButton);
        KluUZYuxme.m3299a(m2526p()).m3313a(assetButton, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3139w());
        assetButton.setOnClickListener(new C10898(this));
        this.f2641j = view.findViewById(C1067R.id.load_more_button_container);
        this.f2641j.setBackgroundColor(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3138v().m3167a().m3215a());
        overscrollingListView.m3588b(view);
        this.f2636e.m3586a(this.f2637f);
        this.f2636e.m3584a(null);
        mo4674b(false);
    }

    /* renamed from: a */
    public final void mo4664a(ActivityPost activityPost) {
        this.f2639h.m3478a(activityPost);
    }

    /* renamed from: a */
    public final void mo4665a(final im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.pdwpUtZXDT pdwputzxdt) {
        Builder z = m2545z();
        String str = this.b.strings().ReportAsSpam;
        String str2 = this.b.strings().ReportAsInappropriate;
        CharSequence[] charSequenceArr = new CharSequence[]{str, str2};
        z.setItems(charSequenceArr, new OnClickListener(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2625b;

            public void onClick(DialogInterface dialogInterface, int i) {
                switch (i) {
                    case 0:
                        pdwputzxdt.mo4597a(ReportingReason.SPAM);
                        break;
                    case 1:
                        pdwputzxdt.mo4597a(ReportingReason.INAPPROPRIATE_CONTENT);
                        break;
                }
                dialogInterface.dismiss();
            }
        }).setNegativeButton(this.b.strings().CancelButton, new C10821(this)).show();
    }

    /* renamed from: a */
    public final void mo4666a(upgqDBbsrL upgqdbbsrl) {
        this.f2640i = upgqdbbsrl;
    }

    /* renamed from: a */
    public final void mo4667a(OnActivityEventListener onActivityEventListener) {
        this.f2639h.m3479a(onActivityEventListener);
    }

    /* renamed from: a */
    public final void mo4668a(String str) {
        for (int i = 0; i < this.f2634a.size(); i++) {
            if (str.equals(((ActivityPost) this.f2634a.get(i)).getId())) {
                this.f2636e.m3583a(i + 1);
                return;
            }
        }
    }

    /* renamed from: a */
    public final void mo4669a(String str, final im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.cjrhisSQCL cjrhissqcl) {
        CharSequence[] charSequenceArr = new CharSequence[]{str};
        m2545z().setItems(charSequenceArr, new OnClickListener(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f2628b;

            public void onClick(DialogInterface dialogInterface, int i) {
                if (i == 0) {
                    cjrhissqcl.mo4598a();
                }
                dialogInterface.dismiss();
            }
        }).setNegativeButton(this.b.strings().CancelButton, new C10843(this)).show();
    }

    /* renamed from: a */
    public final void mo4670a(List<ActivityPost> list) {
        this.f2634a.clear();
        this.f2634a.addAll(list);
        this.f2637f.notifyDataSetChanged();
    }

    /* renamed from: a */
    public final void mo4671a(boolean z) {
        this.f2638g.m3512a(z);
    }

    /* renamed from: b */
    public final void mo4672b() {
        this.f2638g.m3509a();
    }

    /* renamed from: b */
    public final void mo4673b(List<jjbQypPegg> list) {
        m2849l().m3069a(im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL.class, new C10865(this), list);
    }

    /* renamed from: b */
    public final void mo4674b(boolean z) {
        this.f2641j.setVisibility(z ? 0 : 8);
    }

    /* renamed from: c */
    public final void mo4663c() {
        this.f2636e.m3582a();
    }

    /* renamed from: c */
    public final void mo4675c(List<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> list) {
        m2849l().m3069a(im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT.class, new C10876(this), list);
    }

    /* renamed from: d */
    public final void mo4676d() {
        if (this.f2642k != null && this.f2642k.m3070a(im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL.class)) {
            mo4678f();
        }
    }

    /* renamed from: e */
    public final void mo4677e() {
        if (this.f2642k != null && this.f2642k.m3070a(im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT.class)) {
            mo4678f();
        }
    }

    /* renamed from: f */
    public final void mo4678f() {
        ((ViewGroup) m2536q()).removeView(this.f2642k);
        this.f2642k = null;
    }

    /* renamed from: g */
    public final im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<jjbQypPegg> mo4679g() {
        return this.f2638g.m3513b();
    }

    /* renamed from: h */
    public final im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> mo4680h() {
        return this.f2638g.m3514c();
    }

    /* renamed from: i */
    public final void mo4681i() {
        if (this.f2642k != null) {
            LoadingIndicator.m3515a(this.f2642k);
        }
    }

    /* renamed from: j */
    public final void mo4682j() {
        if (this.f2642k != null) {
            LoadingIndicator.m3518b(this.f2642k);
        }
    }

    /* renamed from: k */
    public final void mo4683k() {
        this.f2636e.m3583a(this.f2634a.size());
    }
}
