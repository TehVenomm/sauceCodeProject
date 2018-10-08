package im.getsocial.sdk.ui.activities.p116a.p124c;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AbsListView.OnScrollListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p124c.cjrhisSQCL.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.activities.a.c.XdbacJlTDQ */
class XdbacJlTDQ extends cjrhisSQCL {
    /* renamed from: a */
    private final KluUZYuxme f2705a = KluUZYuxme.m3299a(m2526p());
    /* renamed from: d */
    private final List<PublicUser> f2706d = new ArrayList();
    /* renamed from: e */
    private ListView f2707e;
    /* renamed from: f */
    private jjbQypPegg f2708f;

    /* renamed from: im.getsocial.sdk.ui.activities.a.c.XdbacJlTDQ$1 */
    class C11111 implements OnScrollListener {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2702a;
        /* renamed from: b */
        private int f2703b = 0;

        C11111(XdbacJlTDQ xdbacJlTDQ) {
            this.f2702a = xdbacJlTDQ;
        }

        public void onScroll(AbsListView absListView, int i, int i2, int i3) {
            Object obj = 1;
            Object obj2 = i > this.f2703b ? 1 : null;
            if (i3 == 0 || i + i2 < i3) {
                obj = null;
            }
            if (!(obj2 == null || r0 == null || this.f2702a.m2542w())) {
                ((upgqDBbsrL) this.f2702a.m2524n()).mo4704g();
            }
            this.f2703b = i;
        }

        public void onScrollStateChanged(AbsListView absListView, int i) {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.activities.a.c.XdbacJlTDQ$2 */
    class C11122 implements OnItemClickListener {
        /* renamed from: a */
        final /* synthetic */ XdbacJlTDQ f2704a;

        C11122(XdbacJlTDQ xdbacJlTDQ) {
            this.f2704a = xdbacJlTDQ;
        }

        public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
            ((upgqDBbsrL) this.f2704a.m2524n()).mo4700a((PublicUser) this.f2704a.f2706d.get(i));
        }
    }

    XdbacJlTDQ() {
    }

    /* renamed from: a */
    public final View mo4580a(ViewGroup viewGroup) {
        return LayoutInflater.from(m2526p()).inflate(C1067R.layout.contentview_activity_likers, viewGroup);
    }

    /* renamed from: a */
    protected final void mo4581a() {
        this.f2707e = (ListView) m2529a(C1067R.id.list_view_activity_likers, ListView.class);
    }

    /* renamed from: a */
    public final void mo4697a(List<PublicUser> list) {
        this.f2706d.clear();
        this.f2706d.addAll(list);
        this.f2708f.notifyDataSetChanged();
    }

    protected final void a_() {
        super.a_();
        this.f2708f = new jjbQypPegg(m2526p(), this.f2706d, this.f2705a);
        this.f2707e.setAdapter(this.f2708f);
        this.f2707e.setOnScrollListener(new C11111(this));
        this.f2707e.setOnItemClickListener(new C11122(this));
    }
}
