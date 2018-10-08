package im.getsocial.sdk.ui;

import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg;

public abstract class ViewBuilder<V extends ViewBuilder> {
    /* renamed from: a */
    protected static final cjrhisSQCL f2536a = upgqDBbsrL.m1274a(ViewBuilder.class);
    /* renamed from: b */
    protected String f2537b;
    @XdbacJlTDQ
    /* renamed from: c */
    im.getsocial.sdk.internal.p047b.upgqDBbsrL f2538c;
    /* renamed from: d */
    private ViewStateListener f2539d;
    /* renamed from: e */
    private UiActionListener f2540e;
    /* renamed from: f */
    private boolean f2541f = false;
    /* renamed from: g */
    private boolean f2542g;

    protected ViewBuilder() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    final ViewStateListener m2508a() {
        return this.f2539d == null ? null : (ViewStateListener) this.f2538c.m1060a(ViewStateListener.class, this.f2539d);
    }

    /* renamed from: a */
    protected abstract void mo4579a(jjbQypPegg.upgqDBbsrL upgqdbbsrl);

    /* renamed from: a */
    final void m2510a(boolean z) {
        this.f2541f = z;
    }

    /* renamed from: b */
    final jjbQypPegg.upgqDBbsrL m2511b() {
        jjbQypPegg.upgqDBbsrL c = mo4594c();
        c.m2584c(this.f2537b);
        c.m2581a(this.f2541f);
        c.m2583b(this.f2542g);
        UiActionListener uiActionListener = this.f2540e;
        c.m2577a(uiActionListener == null ? UiActionListener.PROCEED_ALL : (UiActionListener) this.f2538c.m1060a(UiActionListener.class, new im.getsocial.sdk.ui.internal.p129b.jjbQypPegg(uiActionListener)));
        mo4579a(c);
        return c;
    }

    /* renamed from: b */
    final void m2512b(boolean z) {
        this.f2542g = z;
    }

    /* renamed from: c */
    protected abstract jjbQypPegg.upgqDBbsrL mo4594c();

    public V setUiActionListener(UiActionListener uiActionListener) {
        this.f2540e = uiActionListener;
        return this;
    }

    public V setViewStateListener(ViewStateListener viewStateListener) {
        this.f2539d = viewStateListener;
        return this;
    }

    public V setWindowTitle(String str) {
        this.f2537b = str;
        return this;
    }

    public boolean show() {
        return GetSocialUi.showView(this);
    }
}
