package im.getsocial.sdk.invites.p092a.p094b;

import im.getsocial.sdk.invites.InviteChannel;
import java.util.List;

/* renamed from: im.getsocial.sdk.invites.a.b.upgqDBbsrL */
public class upgqDBbsrL {
    /* renamed from: a */
    pdwpUtZXDT f2321a;
    /* renamed from: b */
    List<InviteChannel> f2322b;

    public upgqDBbsrL(pdwpUtZXDT pdwputzxdt, List<InviteChannel> list) {
        this.f2321a = pdwputzxdt;
        this.f2322b = list;
    }

    /* renamed from: a */
    public final InviteChannel m2286a(String str) {
        for (InviteChannel inviteChannel : this.f2322b) {
            if (inviteChannel.getChannelId().equals(str)) {
                return inviteChannel;
            }
        }
        throw new IllegalArgumentException("Channel with id '" + str + "' not found");
    }

    /* renamed from: a */
    public final pdwpUtZXDT m2287a() {
        return this.f2321a;
    }

    /* renamed from: b */
    public final List<InviteChannel> m2288b() {
        return this.f2322b;
    }
}
