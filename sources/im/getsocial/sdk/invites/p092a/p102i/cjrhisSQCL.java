package im.getsocial.sdk.invites.p092a.p102i;

import im.getsocial.sdk.internal.p033c.p034l.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.invites.InviteChannelIds;
import im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg;
import im.getsocial.sdk.invites.p092a.pdwpUtZXDT;

/* renamed from: im.getsocial.sdk.invites.a.i.cjrhisSQCL */
public final class cjrhisSQCL implements upgqDBbsrL {
    /* renamed from: b */
    private static final String[] f2411b = new String[]{InviteChannelIds.GENERIC, "email", "facebook", InviteChannelIds.HANGOUTS, "instagram", InviteChannelIds.KAKAO, InviteChannelIds.KIK, InviteChannelIds.LINE, InviteChannelIds.FACEBOOK_MESSENGER, InviteChannelIds.NATIVE_SHARE, InviteChannelIds.SNAPCHAT, InviteChannelIds.SMS, "twitter", InviteChannelIds.TELEGRAM, InviteChannelIds.VIBER, InviteChannelIds.VK, InviteChannelIds.WHATSAPP, "googleplus"};
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2412a;

    public cjrhisSQCL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final void m2369a(String str, pdwpUtZXDT pdwputzxdt) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Channel ID should not be null");
        String[] strArr = f2411b;
        for (int i = 0; i < 18; i++) {
            if (strArr[i].equalsIgnoreCase(str)) {
                this.f2412a.m2350a(str, pdwputzxdt);
                return;
            }
        }
        throw new IllegalArgumentException("Channel ID " + str + " is not valid. Check all valid Channel IDs.");
    }
}
