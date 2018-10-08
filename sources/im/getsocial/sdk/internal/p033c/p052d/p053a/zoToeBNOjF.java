package im.getsocial.sdk.internal.p033c.p052d.p053a;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.invites.InviteCallback;

/* renamed from: im.getsocial.sdk.internal.c.d.a.zoToeBNOjF */
class zoToeBNOjF implements jjbQypPegg {
    /* renamed from: a */
    private final InviteCallback f1262a;

    zoToeBNOjF(InviteCallback inviteCallback) {
        this.f1262a = inviteCallback;
    }

    /* renamed from: a */
    public final void mo4382a(GetSocialException getSocialException) {
        if (this.f1262a != null) {
            this.f1262a.onError(getSocialException);
        }
    }
}
