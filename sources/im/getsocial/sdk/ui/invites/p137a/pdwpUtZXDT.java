package im.getsocial.sdk.ui.invites.p137a;

import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.ui.invites.p137a.cjrhisSQCL.cjrhisSQCL;
import im.getsocial.sdk.ui.invites.p137a.cjrhisSQCL.jjbQypPegg;
import im.getsocial.sdk.ui.invites.p137a.cjrhisSQCL.upgqDBbsrL;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.ui.invites.a.pdwpUtZXDT */
public class pdwpUtZXDT extends upgqDBbsrL {

    /* renamed from: im.getsocial.sdk.ui.invites.a.pdwpUtZXDT$1 */
    class C12161 implements InviteCallback {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3242a;

        C12161(pdwpUtZXDT pdwputzxdt) {
            this.f3242a = pdwputzxdt;
        }

        public void onCancel() {
            ((cjrhisSQCL) this.f3242a.mo4733t()).m2541v();
        }

        public void onComplete() {
            ((cjrhisSQCL) this.f3242a.mo4733t()).m2541v();
        }

        public void onError(Throwable th) {
            ((cjrhisSQCL) this.f3242a.mo4733t()).m2541v();
            this.f3242a.m2580a(th);
        }
    }

    public pdwpUtZXDT(cjrhisSQCL cjrhissqcl, jjbQypPegg jjbqyppegg) {
        super(cjrhissqcl, jjbqyppegg);
    }

    /* renamed from: a */
    public final String mo4591a() {
        return this.c.strings().InviteFriends;
    }

    /* renamed from: a */
    public final void mo4750a(InviteChannel inviteChannel) {
        String channelId = inviteChannel.getChannelId();
        Map hashMap = new HashMap();
        hashMap.put("provider", channelId);
        m2579a("ui_invite_clicked", hashMap);
        ((cjrhisSQCL) mo4733t()).m2540u();
        ((jjbQypPegg) m2593y()).mo4751a(inviteChannel, new C12161(this));
    }

    /* renamed from: b */
    public final String mo4592b() {
        return "smartinvite";
    }

    public final void d_() {
        ((cjrhisSQCL) mo4733t()).mo4749a(((jjbQypPegg) m2593y()).mo4752b());
    }

    /* renamed from: s */
    public final void mo4696s() {
        super.mo4696s();
        ((cjrhisSQCL) mo4733t()).m2541v();
    }
}
