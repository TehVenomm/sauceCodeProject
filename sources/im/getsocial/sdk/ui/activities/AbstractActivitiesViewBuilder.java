package im.getsocial.sdk.ui.activities;

import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p047b.upgqDBbsrL;
import im.getsocial.sdk.ui.AvatarClickListener;
import im.getsocial.sdk.ui.MentionClickListener;
import im.getsocial.sdk.ui.TagClickListener;
import im.getsocial.sdk.ui.ViewBuilder;
import im.getsocial.sdk.ui.activities.p116a.p120i.cjrhisSQCL;
import im.getsocial.sdk.ui.activities.p116a.p120i.pdwpUtZXDT;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg;

public abstract class AbstractActivitiesViewBuilder<B extends AbstractActivitiesViewBuilder> extends ViewBuilder<B> {
    /* renamed from: d */
    protected boolean f2543d;
    @XdbacJlTDQ
    /* renamed from: e */
    upgqDBbsrL f2544e;
    /* renamed from: f */
    private ActionButtonListener f2545f;
    /* renamed from: g */
    private AvatarClickListener f2546g;
    /* renamed from: h */
    private MentionClickListener f2547h;
    /* renamed from: i */
    private TagClickListener f2548i;

    /* renamed from: a */
    protected final void mo4579a(jjbQypPegg.upgqDBbsrL upgqdbbsrl) {
        if (upgqdbbsrl instanceof im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg) {
            ((im.getsocial.sdk.ui.activities.p116a.p120i.jjbQypPegg) upgqdbbsrl).mo4619a(m2516e());
        }
        if (upgqdbbsrl instanceof im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL) {
            ((im.getsocial.sdk.ui.activities.p116a.p120i.upgqDBbsrL) upgqdbbsrl).mo4616a(m2515d());
        }
        if (upgqdbbsrl instanceof cjrhisSQCL) {
            ((cjrhisSQCL) upgqdbbsrl).mo4617a(m2517f());
        }
        if (upgqdbbsrl instanceof pdwpUtZXDT) {
            ((pdwpUtZXDT) upgqdbbsrl).mo4618a(m2518g());
        }
    }

    /* renamed from: d */
    protected final AvatarClickListener m2515d() {
        return (AvatarClickListener) this.f2544e.m1060a(AvatarClickListener.class, this.f2546g);
    }

    /* renamed from: e */
    protected final ActionButtonListener m2516e() {
        return (ActionButtonListener) this.f2544e.m1060a(ActionButtonListener.class, this.f2545f);
    }

    /* renamed from: f */
    protected final MentionClickListener m2517f() {
        return (MentionClickListener) this.f2544e.m1060a(MentionClickListener.class, this.f2547h);
    }

    /* renamed from: g */
    protected final TagClickListener m2518g() {
        return (TagClickListener) this.f2544e.m1060a(TagClickListener.class, this.f2548i);
    }

    public B setAvatarClickListener(AvatarClickListener avatarClickListener) {
        this.f2546g = avatarClickListener;
        return this;
    }

    public B setButtonActionListener(ActionButtonListener actionButtonListener) {
        this.f2545f = actionButtonListener;
        return this;
    }

    public B setMentionClickListener(MentionClickListener mentionClickListener) {
        this.f2547h = mentionClickListener;
        return this;
    }

    public B setReadOnly(boolean z) {
        this.f2543d = z;
        return this;
    }

    public B setTagClickListener(TagClickListener tagClickListener) {
        this.f2548i = tagClickListener;
        return this;
    }
}
