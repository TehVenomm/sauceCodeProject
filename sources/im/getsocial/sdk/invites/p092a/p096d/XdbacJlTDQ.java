package im.getsocial.sdk.invites.p092a.p096d;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.activities.MentionTypes;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.p092a.p094b.cjrhisSQCL;
import im.getsocial.sdk.invites.p092a.p094b.zoToeBNOjF;
import im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.d.XdbacJlTDQ */
public class XdbacJlTDQ implements upgqDBbsrL<cjrhisSQCL, pdwpUtZXDT<Void>> {
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2341a;
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg f2342b;
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: c */
    im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f2343c;
    /* renamed from: d */
    private final InviteChannel f2344d;

    public XdbacJlTDQ(InviteChannel inviteChannel) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f2344d = inviteChannel;
    }

    /* renamed from: a */
    static /* synthetic */ void m2300a(XdbacJlTDQ xdbacJlTDQ, String str, String str2, String str3, Map map) {
        Map hashMap = new HashMap();
        hashMap.put("provider", str2);
        hashMap.put("token", str3);
        hashMap.put("source", MentionTypes.USER);
        if (map != null) {
            hashMap.putAll(map);
        }
        xdbacJlTDQ.f2343c.m1053a(str, hashMap);
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        final cjrhisSQCL cjrhissqcl = (cjrhisSQCL) obj;
        final im.getsocial.sdk.invites.p092a.pdwpUtZXDT a = this.f2342b.m2348a(this.f2344d.getChannelId());
        return !a.mo4571a(this.f2344d) ? pdwpUtZXDT.m1660a(new GetSocialException(ErrorCode.ILLEGAL_ARGUMENT, "Invite channel [" + this.f2344d.getChannelId() + "] is not available.")) : pdwpUtZXDT.m1658a(new KSZKMmRWhZ<im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM<? super Void>>(this) {
            /* renamed from: c */
            final /* synthetic */ XdbacJlTDQ f2340c;

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                final im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM ztwnwcuzim = (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj;
                zoToeBNOjF a = im.getsocial.sdk.invites.upgqDBbsrL.m2409a(this.f2340c.f2344d);
                String d = a == null ? "" : a.m2292d();
                String displayName = this.f2340c.f2341a.m3698b().getDisplayName();
                String str = cjrhissqcl.f2304a;
                final String str2 = cjrhissqcl.f2305b;
                String str3 = "";
                if (cjrhissqcl.f2306c.m2279d() != null) {
                    str3 = im.getsocial.sdk.invites.p092a.p097j.XdbacJlTDQ.m2382a(cjrhissqcl.f2306c.m2279d().getLocalisedString(), d, displayName, str, false);
                }
                a.mo4570a(this.f2340c.f2344d, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.m2276a().m2271b(cjrhissqcl.f2306c.m2280e() != null ? im.getsocial.sdk.invites.p092a.p097j.XdbacJlTDQ.m2382a(cjrhissqcl.f2306c.m2280e().getLocalisedString(), d, displayName, str, false) : "").m2267a(str3).m2268a(cjrhissqcl.f2306c.m2282g()).m2273c(cjrhissqcl.f2306c.m2281f()).m2275e(cjrhissqcl.f2306c.m2284i()).m2274d(cjrhissqcl.f2306c.m2283h()).m2272b(cjrhissqcl.f2306c.m2285j()).m2269a(), displayName, str, new im.getsocial.sdk.invites.p092a.upgqDBbsrL(this) {
                    /* renamed from: c */
                    final /* synthetic */ C10361 f2337c;

                    /* renamed from: a */
                    public final void mo4562a(Map<String, String> map) {
                        XdbacJlTDQ.m2300a(this.f2337c.f2340c, "invite_sent", this.f2337c.f2340c.f2344d.getChannelId(), str2, map);
                        ztwnwcuzim.mo4489a(null);
                    }

                    public void onCancel() {
                        XdbacJlTDQ.m2300a(this.f2337c.f2340c, "invite_canceled", this.f2337c.f2340c.f2344d.getChannelId(), str2, null);
                        ztwnwcuzim.mo4490a(new im.getsocial.sdk.invites.p092a.p095c.jjbQypPegg());
                        ztwnwcuzim.mo4489a(null);
                    }

                    public void onComplete() {
                        XdbacJlTDQ.m2300a(this.f2337c.f2340c, "invite_sent", this.f2337c.f2340c.f2344d.getChannelId(), str2, null);
                        ztwnwcuzim.mo4489a(null);
                    }

                    public void onError(Throwable th) {
                        XdbacJlTDQ.m2300a(this.f2337c.f2340c, "invite_failed", this.f2337c.f2340c.f2344d.getChannelId(), str2, null);
                        ztwnwcuzim.mo4490a(th);
                    }
                });
            }
        });
    }
}
