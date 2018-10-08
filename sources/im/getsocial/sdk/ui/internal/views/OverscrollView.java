package im.getsocial.sdk.ui.internal.views;

import android.annotation.SuppressLint;
import android.content.Context;
import android.widget.LinearLayout;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.views.OverscrollingListView.OnOverscrollActionDoneListener;
import im.getsocial.sdk.ui.internal.views.OverscrollingListView.OnOverscrollListener;

@SuppressLint({"ViewConstructor"})
public class OverscrollView extends LinearLayout implements OnOverscrollListener {
    /* renamed from: a */
    private final String f2653a;
    /* renamed from: b */
    protected MultiTextView f2654b;
    /* renamed from: c */
    protected LoadingIndicator f2655c;
    /* renamed from: d */
    private final String f2656d;
    /* renamed from: e */
    private final Runnable f2657e;
    /* renamed from: f */
    private final int f2658f = upgqDBbsrL.m3237a().m3244a(64);
    /* renamed from: g */
    private OnOverscrollActionDoneListener f2659g;

    public OverscrollView(Context context, String str, String str2, Runnable runnable) {
        super(context);
        this.f2653a = str;
        this.f2656d = str2;
        this.f2657e = runnable;
        inflate(getContext(), C1067R.layout.overscroll_view, this);
        this.f2654b = (MultiTextView) findViewById(C1067R.id.text_view_loading);
        this.f2655c = (LoadingIndicator) findViewById(C1067R.id.progress_bar_loading);
        this.f2654b.m3555a(17);
        this.f2655c.m3519a(0, 1.0f);
    }

    /* renamed from: a */
    public final int mo4684a(int i, OnOverscrollActionDoneListener onOverscrollActionDoneListener) {
        if (i < this.f2658f) {
            return 0;
        }
        this.f2654b.setVisibility(8);
        this.f2655c.setVisibility(0);
        this.f2657e.run();
        this.f2659g = onOverscrollActionDoneListener;
        return this.f2658f;
    }

    /* renamed from: a */
    public final void mo4685a() {
        if (this.f2659g != null) {
            this.f2659g.mo4746a();
            this.f2659g = null;
        }
        this.f2655c.setVisibility(8);
    }

    /* renamed from: a */
    public final void mo4686a(int i) {
        this.f2654b.setVisibility(0);
        this.f2655c.setVisibility(8);
        KluUZYuxme a = upgqDBbsrL.m3237a().m3248a(upgqDBbsrL.m3237a().m3255b().m3212c().m3129m().m3209a());
        this.f2654b.m3554a();
        if (i < this.f2658f) {
            this.f2654b.m3556a(this.f2653a, a);
        } else {
            this.f2654b.m3556a(this.f2656d, a);
        }
    }
}
