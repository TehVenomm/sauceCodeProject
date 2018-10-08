package im.getsocial.sdk.ui.invites.p137a;

import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteContent;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.ui.invites.InviteUiCallback;
import im.getsocial.sdk.ui.invites.p137a.cjrhisSQCL.jjbQypPegg;
import java.util.List;

/* renamed from: im.getsocial.sdk.ui.invites.a.upgqDBbsrL */
public class upgqDBbsrL extends jjbQypPegg {
    /* renamed from: a */
    private final InviteContent f3246a;
    /* renamed from: b */
    private final LinkParams f3247b;
    /* renamed from: c */
    private final InviteUiCallback f3248c;

    public upgqDBbsrL(InviteContent inviteContent, LinkParams linkParams, InviteUiCallback inviteUiCallback) {
        this.f3246a = inviteContent;
        this.f3247b = linkParams;
        this.f3248c = inviteUiCallback;
    }

    /* renamed from: a */
    public final void mo4751a(final InviteChannel inviteChannel, final InviteCallback inviteCallback) {
        GetSocial.sendInvite(inviteChannel.getChannelId(), this.f3246a, this.f3247b, new InviteCallback(this) {
            /* renamed from: c */
            final /* synthetic */ upgqDBbsrL f3245c;

            public void onCancel() {
                inviteCallback.onCancel();
                if (this.f3245c.f3248c != null) {
                    this.f3245c.f3248c.onCancel(inviteChannel.getChannelId());
                }
            }

            public void onComplete() {
                inviteCallback.onComplete();
                if (this.f3245c.f3248c != null) {
                    this.f3245c.f3248c.onComplete(inviteChannel.getChannelId());
                }
            }

            public void onError(Throwable th) {
                inviteCallback.onError(th);
                if (this.f3245c.f3248c != null) {
                    this.f3245c.f3248c.onError(inviteChannel.getChannelId(), th);
                }
            }
        });
    }

    /* renamed from: b */
    public final List<InviteChannel> mo4752b() {
        return GetSocial.getInviteChannels();
    }
}
