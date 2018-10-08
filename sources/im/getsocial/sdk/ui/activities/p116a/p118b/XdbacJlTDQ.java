package im.getsocial.sdk.ui.activities.p116a.p118b;

import android.app.AlertDialog.Builder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.sharedl10n.LocalizationAdapter;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p121d.jjbQypPegg;
import im.getsocial.sdk.ui.activities.p116a.p123g.pdwpUtZXDT;
import im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.views.ActivityContainerView;
import im.getsocial.sdk.ui.internal.views.InputContainer;
import im.getsocial.sdk.ui.internal.views.InputContainer.Listener;
import im.getsocial.sdk.ui.internal.views.LoadingIndicator;
import im.getsocial.sdk.ui.internal.views.OverscrollView;
import im.getsocial.sdk.ui.internal.views.OverscrollingListView;
import im.getsocial.sdk.ui.internal.views.PlaceholderView;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ */
public class XdbacJlTDQ extends cjrhisSQCL {
    /* renamed from: a */
    private final List<ActivityPost> f2663a = new ArrayList();
    /* renamed from: d */
    private final boolean f2664d;
    /* renamed from: e */
    private final boolean f2665e;
    /* renamed from: f */
    private final List<String> f2666f;
    /* renamed from: g */
    private FrameLayout f2667g;
    /* renamed from: h */
    private InputContainer f2668h;
    /* renamed from: i */
    private im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL f2669i;
    /* renamed from: j */
    private OverscrollingListView f2670j;
    /* renamed from: k */
    private im.getsocial.sdk.ui.activities.p116a.cjrhisSQCL f2671k;
    /* renamed from: l */
    private upgqDBbsrL f2672l;

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$1 */
    class C10901 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2643a;

        C10901(XdbacJlTDQ xdbacJlTDQ) {
            this.f2643a = xdbacJlTDQ;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$3 */
    class C10923 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2646a;

        C10923(XdbacJlTDQ xdbacJlTDQ) {
            this.f2646a = xdbacJlTDQ;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$5 */
    class C10945 implements pdwpUtZXDT<jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2649a;

        C10945(XdbacJlTDQ xdbacJlTDQ) {
            this.f2649a = xdbacJlTDQ;
        }

        /* renamed from: a */
        public final im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg<jjbQypPegg> mo4660a() {
            return new im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL(this.f2649a.m2526p());
        }

        /* renamed from: a */
        public final /* synthetic */ void mo4661a(Object obj) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2649a.m2524n()).m2691a((jjbQypPegg) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$6 */
    class C10956 implements pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2650a;

        C10956(XdbacJlTDQ xdbacJlTDQ) {
            this.f2650a = xdbacJlTDQ;
        }

        /* renamed from: a */
        public final im.getsocial.sdk.ui.activities.p116a.p123g.jjbQypPegg<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> mo4660a() {
            return new im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT(this.f2650a.m2526p());
        }

        /* renamed from: a */
        public final /* synthetic */ void mo4661a(Object obj) {
            ((upgqDBbsrL.upgqDBbsrL) this.f2650a.m2524n()).m2692a((im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$jjbQypPegg */
    private class jjbQypPegg extends OverscrollView {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2660a;

        /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$jjbQypPegg$1 */
        class C10971 implements Runnable {
            /* renamed from: a */
            final /* synthetic */ XdbacJlTDQ f2652a;

            C10971(XdbacJlTDQ xdbacJlTDQ) {
                this.f2652a = xdbacJlTDQ;
            }

            public void run() {
                ((upgqDBbsrL.upgqDBbsrL) this.f2652a.m2524n()).mo4640o();
            }
        }

        public jjbQypPegg(XdbacJlTDQ xdbacJlTDQ, Context context) {
            this.f2660a = xdbacJlTDQ;
            super(context, xdbacJlTDQ.b.strings().PullUpToLoadMore, xdbacJlTDQ.b.strings().ReleaseToLoadMore, new C10971(xdbacJlTDQ));
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$upgqDBbsrL */
    private class upgqDBbsrL extends OverscrollView {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2662a;

        /* renamed from: im.getsocial.sdk.ui.activities.a.b.XdbacJlTDQ$upgqDBbsrL$1 */
        class C10981 implements Runnable {
            /* renamed from: a */
            final /* synthetic */ XdbacJlTDQ f2661a;

            C10981(XdbacJlTDQ xdbacJlTDQ) {
                this.f2661a = xdbacJlTDQ;
            }

            public void run() {
                ((im.getsocial.sdk.ui.activities.p116a.p118b.upgqDBbsrL.upgqDBbsrL) this.f2661a.m2524n()).mo4692c();
            }
        }

        public upgqDBbsrL(XdbacJlTDQ xdbacJlTDQ, Context context) {
            this.f2662a = xdbacJlTDQ;
            super(context, xdbacJlTDQ.b.strings().PullDownToRefresh, xdbacJlTDQ.b.strings().ReleaseToRefresh, new C10981(xdbacJlTDQ));
            setGravity(80);
        }
    }

    public XdbacJlTDQ(boolean z, List<String> list) {
        this.f2664d = z;
        this.f2665e = !list.isEmpty();
        this.f2666f = new ArrayList(list);
    }

    /* renamed from: B */
    private im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL m2887B() {
        if (this.f2669i == null) {
            this.f2669i = new im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL(m2526p());
            this.f2667g.addView(this.f2669i);
            KluUZYuxme.m3299a(m2526p()).m3311a(this.f2669i, true);
        }
        return this.f2669i;
    }

    /* renamed from: a */
    public final View mo4580a(ViewGroup viewGroup) {
        View inflate = LayoutInflater.from(m2526p()).inflate(C1067R.layout.contentview_activities, viewGroup);
        this.f2668h = new InputContainer(m2526p());
        this.f2670j = new OverscrollingListView(m2526p(), new upgqDBbsrL(this, m2526p()), new jjbQypPegg(this, m2526p()));
        if (!this.f2664d) {
            this.f2670j.m3585a(this.f2668h);
        }
        this.f2667g = (FrameLayout) inflate.findViewById(C1067R.id.container_activities);
        this.f2667g.addView(this.f2670j);
        return inflate;
    }

    /* renamed from: a */
    public final void mo4581a() {
        this.f2668h.m3510a((Listener) m2524n());
        this.f2668h.setBackgroundColor(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3133q().m3167a().m3215a());
        this.f2668h.m3511a(this.b.strings().ActivityPostPlaceholder);
        this.f2671k = new im.getsocial.sdk.ui.activities.p116a.cjrhisSQCL(this, m2526p(), this.f2663a, this.f2672l) {
            /* renamed from: a */
            final /* synthetic */ XdbacJlTDQ f2651a;

            /* renamed from: a */
            protected final void mo4662a(ActivityContainerView activityContainerView) {
                activityContainerView.m3477a(3);
                if (this.f2651a.f2664d) {
                    activityContainerView.m3481c();
                }
            }
        };
        this.f2670j.m3586a(this.f2671k);
        this.f2670j.m3584a(null);
    }

    /* renamed from: a */
    public final void mo4665a(final im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.pdwpUtZXDT pdwputzxdt) {
        Builder z = m2545z();
        String str = this.b.strings().ReportAsSpam;
        String str2 = this.b.strings().ReportAsInappropriate;
        CharSequence[] charSequenceArr = new CharSequence[]{str, str2};
        z.setItems(charSequenceArr, new OnClickListener(this) {
            /* renamed from: b */
            final /* synthetic */ XdbacJlTDQ f2645b;

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
        }).setNegativeButton(this.b.strings().CancelButton, new C10901(this)).show();
    }

    /* renamed from: a */
    public final void mo4666a(upgqDBbsrL upgqdbbsrl) {
        this.f2672l = upgqdbbsrl;
    }

    /* renamed from: a */
    public final void mo4669a(String str, final im.getsocial.sdk.ui.activities.p116a.pdwpUtZXDT.cjrhisSQCL.cjrhisSQCL cjrhissqcl) {
        CharSequence[] charSequenceArr = new CharSequence[]{str};
        m2545z().setItems(charSequenceArr, new OnClickListener(this) {
            /* renamed from: b */
            final /* synthetic */ XdbacJlTDQ f2648b;

            public void onClick(DialogInterface dialogInterface, int i) {
                if (i == 0) {
                    cjrhissqcl.mo4598a();
                }
                dialogInterface.dismiss();
            }
        }).setNegativeButton(this.b.strings().CancelButton, new C10923(this)).show();
    }

    /* renamed from: a */
    public final void mo4670a(List<ActivityPost> list) {
        this.f2663a.clear();
        this.f2663a.addAll(list);
        this.f2671k.notifyDataSetChanged();
    }

    /* renamed from: a */
    public final void mo4671a(boolean z) {
        this.f2668h.m3512a(z);
    }

    /* renamed from: b */
    public final void mo4672b() {
        this.f2668h.m3509a();
    }

    /* renamed from: b */
    public final void mo4673b(List<jjbQypPegg> list) {
        m2887B().m3069a(im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL.class, new C10945(this), list);
    }

    /* renamed from: c */
    public final void mo4663c() {
        this.f2670j.m3582a();
    }

    /* renamed from: c */
    public final void mo4675c(List<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> list) {
        m2887B().m3069a(im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT.class, new C10956(this), list);
    }

    /* renamed from: d */
    public final void mo4676d() {
        if (this.f2669i != null && this.f2669i.m3070a(im.getsocial.sdk.ui.activities.p116a.p121d.upgqDBbsrL.class)) {
            mo4678f();
        }
    }

    /* renamed from: e */
    public final void mo4677e() {
        if (this.f2669i != null && this.f2669i.m3070a(im.getsocial.sdk.ui.activities.p116a.p122h.pdwpUtZXDT.class)) {
            mo4678f();
        }
    }

    /* renamed from: f */
    public final void mo4678f() {
        this.f2667g.removeView(this.f2669i);
        this.f2669i = null;
    }

    /* renamed from: g */
    public final im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<jjbQypPegg> mo4679g() {
        return this.f2668h.m3513b();
    }

    /* renamed from: h */
    public final im.getsocial.sdk.ui.internal.p126e.pdwpUtZXDT<im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg> mo4680h() {
        return this.f2668h.m3514c();
    }

    /* renamed from: i */
    public final void mo4681i() {
        if (this.f2669i != null) {
            LoadingIndicator.m3515a(this.f2669i);
        }
    }

    /* renamed from: j */
    public final void mo4682j() {
        if (this.f2669i != null) {
            LoadingIndicator.m3518b(this.f2669i);
        }
    }

    /* renamed from: k */
    public final void mo4687k() {
        this.f2670j.m3587b();
    }

    /* renamed from: l */
    public final void mo4688l() {
        if (this.f2665e) {
            StringBuilder append = new StringBuilder("#").append((String) this.f2666f.get(0));
            for (int i = 1; i < this.f2666f.size(); i++) {
                append.append(", #").append((String) this.f2666f.get(i));
            }
            PlaceholderView.m3589a(this.f2667g, LocalizationAdapter.noResults(this.b, append.toString()), this.b.strings().ActivityAllNoActivitiesPlaceholderMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3113B().m3173a());
        } else {
            PlaceholderView.m3589a(this.f2667g, this.b.strings().ActivityAllNoActivitiesPlaceholderTitle, this.b.strings().ActivityAllNoActivitiesPlaceholderMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3112A().m3173a());
        }
        this.f2667g.bringChildToFront(this.f2670j);
    }

    /* renamed from: m */
    public final void mo4689m() {
        PlaceholderView.m3592a(this.f2667g);
    }
}
