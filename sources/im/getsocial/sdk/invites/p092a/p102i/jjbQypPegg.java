package im.getsocial.sdk.invites.p092a.p102i;

import im.getsocial.sdk.internal.p033c.p034l.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.invites.InviteChannel;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.invites.a.i.jjbQypPegg */
public final class jjbQypPegg implements upgqDBbsrL {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg f2413a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.invites.p092a.p099f.upgqDBbsrL f2414b;

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final List<InviteChannel> m2370a() {
        List<InviteChannel> b = this.f2414b.m2354b().m2288b();
        im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg jjbqyppegg = this.f2413a;
        List<InviteChannel> arrayList = new ArrayList();
        for (InviteChannel inviteChannel : b) {
            if (inviteChannel.isEnabled() && jjbqyppegg.m2348a(inviteChannel.getChannelId()).mo4571a(inviteChannel)) {
                arrayList.add(inviteChannel);
            }
        }
        Collections.sort(arrayList, InviteChannel.INVITE_CHANNELS_COMPARATOR_BASED_ON_DISPLAY_ORDER);
        return arrayList;
    }
}
