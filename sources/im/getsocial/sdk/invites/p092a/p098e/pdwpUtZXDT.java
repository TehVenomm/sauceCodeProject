package im.getsocial.sdk.invites.p092a.p098e;

import android.graphics.Bitmap;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;
import im.getsocial.sdk.invites.InvitePackage.Builder;
import im.getsocial.sdk.invites.p092a.p097j.jjbQypPegg;

/* renamed from: im.getsocial.sdk.invites.a.e.pdwpUtZXDT */
public class pdwpUtZXDT implements im.getsocial.sdk.invites.p092a.pdwpUtZXDT {
    /* renamed from: b */
    private static final cjrhisSQCL f2393b = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2394a;
    /* renamed from: c */
    private final InviteChannelPlugin f2395c;

    public pdwpUtZXDT(InviteChannelPlugin inviteChannelPlugin) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) inviteChannelPlugin), "Can not create SharedInviteProviderPluginAdapter with null plugin");
        this.f2395c = inviteChannelPlugin;
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private void m2342a(InviteChannel inviteChannel, InvitePackage invitePackage, im.getsocial.sdk.invites.p092a.upgqDBbsrL upgqdbbsrl) {
        this.f2395c.presentChannelInterface(inviteChannel, invitePackage, upgqdbbsrl);
    }

    /* renamed from: a */
    public final void mo4570a(final InviteChannel inviteChannel, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT pdwputzxdt, String str, String str2, final im.getsocial.sdk.invites.p092a.upgqDBbsrL upgqdbbsrl) {
        int i = 1;
        if (!mo4571a(inviteChannel)) {
            upgqdbbsrl.onError(new IllegalStateException("Invite Channel `" + inviteChannel.getChannelId() + "' is not available on device."));
        } else if (this.f2394a.mo4573a()) {
            Builder withReferralUrl = new Builder().withUsername(str).withReferralUrl(str2);
            if (pdwputzxdt.m2279d() != null) {
                withReferralUrl.withSubject(pdwputzxdt.m2279d().getLocalisedString());
            }
            if (pdwputzxdt.m2280e() != null) {
                withReferralUrl.withText(pdwputzxdt.m2280e().getLocalisedString());
            }
            if (pdwputzxdt.m2281f() != null) {
                withReferralUrl.withImageUrl(pdwputzxdt.m2281f());
            }
            if (pdwputzxdt.m2284i() != null) {
                withReferralUrl.withGifUrl(pdwputzxdt.m2284i());
            }
            if (pdwputzxdt.m2283h() != null) {
                withReferralUrl.withVideoUrl(pdwputzxdt.m2283h());
            }
            if (!pdwputzxdt.m2277b() || pdwputzxdt.m2278c()) {
                i = 0;
            }
            if (i != 0) {
                String f = pdwputzxdt.m2281f();
                final Builder withImageUrl = withReferralUrl.withImageUrl(pdwputzxdt.m2281f());
                im.getsocial.sdk.internal.p072g.jjbQypPegg.m1910a(f).m1934a(new Callback<Bitmap>(this) {
                    /* renamed from: d */
                    final /* synthetic */ pdwpUtZXDT f2392d;

                    public void onFailure(GetSocialException getSocialException) {
                        pdwpUtZXDT.f2393b.mo4391b("Failed to download Smart Invite image: ", getSocialException.getMessage());
                        this.f2392d.m2342a(inviteChannel, withImageUrl.build(), upgqdbbsrl);
                    }

                    public /* synthetic */ void onSuccess(Object obj) {
                        this.f2392d.m2342a(inviteChannel, withImageUrl.withImage((Bitmap) obj).build(), upgqdbbsrl);
                    }
                });
                return;
            }
            m2342a(inviteChannel, withReferralUrl.build(), upgqdbbsrl);
        } else {
            upgqdbbsrl.onError(new IllegalStateException(String.format("%s is missing in the AndroidManifest. Follow integration guide to fix it: https://docs.getsocial.im/knowledge-base/manual-integration/android/#smart-invites", new Object[]{jjbQypPegg.f2428a})));
        }
    }

    /* renamed from: a */
    public final boolean mo4571a(InviteChannel inviteChannel) {
        return this.f2395c.isAvailableForDevice(inviteChannel);
    }
}
