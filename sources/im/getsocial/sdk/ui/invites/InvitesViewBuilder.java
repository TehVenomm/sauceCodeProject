package im.getsocial.sdk.ui.invites;

import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p047b.upgqDBbsrL;
import im.getsocial.sdk.invites.CustomReferralData;
import im.getsocial.sdk.invites.InviteContent;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.ui.ViewBuilder;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg;
import im.getsocial.sdk.ui.invites.p137a.pdwpUtZXDT;

public class InvitesViewBuilder extends ViewBuilder<InvitesViewBuilder> {
    @XdbacJlTDQ
    /* renamed from: d */
    upgqDBbsrL f3228d;
    /* renamed from: e */
    private LinkParams f3229e;
    /* renamed from: f */
    private InviteContent f3230f;
    /* renamed from: g */
    private InviteUiCallback f3231g;

    /* renamed from: a */
    protected final void mo4579a(jjbQypPegg.upgqDBbsrL upgqdbbsrl) {
    }

    /* renamed from: c */
    protected final jjbQypPegg.upgqDBbsrL mo4594c() {
        return new pdwpUtZXDT(new im.getsocial.sdk.ui.invites.p137a.XdbacJlTDQ(), new im.getsocial.sdk.ui.invites.p137a.upgqDBbsrL(this.f3230f, this.f3229e, this.f3231g == null ? null : (InviteUiCallback) this.f3228d.m1060a(InviteUiCallback.class, this.f3231g)));
    }

    public InvitesViewBuilder setCustomInviteContent(InviteContent inviteContent) {
        this.f3230f = inviteContent;
        return this;
    }

    @Deprecated
    public InvitesViewBuilder setCustomReferralData(CustomReferralData customReferralData) {
        LinkParams linkParams = new LinkParams();
        if (customReferralData != null) {
            linkParams.putAll(customReferralData);
        }
        this.f3229e = linkParams;
        return this;
    }

    public InvitesViewBuilder setInviteCallback(InviteUiCallback inviteUiCallback) {
        this.f3231g = inviteUiCallback;
        return this;
    }

    public InvitesViewBuilder setLinkParams(LinkParams linkParams) {
        this.f3229e = linkParams;
        return this;
    }
}
