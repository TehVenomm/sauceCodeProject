package im.getsocial.sdk.invites.p092a.p099f;

import im.getsocial.sdk.internal.p033c.p041b.KluUZYuxme;
import im.getsocial.sdk.internal.p033c.p041b.jMsobIMeui;
import im.getsocial.sdk.invites.InviteChannelIds;
import im.getsocial.sdk.invites.ReferralData;
import im.getsocial.sdk.invites.p092a.pdwpUtZXDT;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.f.jjbQypPegg */
public class jjbQypPegg implements KluUZYuxme {
    /* renamed from: a */
    private final Map<String, pdwpUtZXDT> f2396a = new HashMap();
    /* renamed from: b */
    private ReferralData f2397b;

    /* renamed from: a */
    public final jMsobIMeui mo4351a() {
        return jMsobIMeui.APP;
    }

    /* renamed from: a */
    public final pdwpUtZXDT m2348a(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Missing required parameter: inviteChannelId");
        return this.f2396a.containsKey(str) ? (pdwpUtZXDT) this.f2396a.get(str) : (pdwpUtZXDT) this.f2396a.get(InviteChannelIds.GENERIC);
    }

    /* renamed from: a */
    public final void m2349a(ReferralData referralData) {
        this.f2397b = referralData;
    }

    /* renamed from: a */
    public final void m2350a(String str, pdwpUtZXDT pdwputzxdt) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Missing required parameter: inviteChannelId");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) pdwputzxdt), "Missing required parameter: sharedInviteChannelPlugin");
        this.f2396a.put(str, pdwputzxdt);
    }

    /* renamed from: b */
    public final ReferralData m2351b() {
        return this.f2397b;
    }
}
