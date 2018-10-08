package im.getsocial.sdk.invites.p092a.p102i;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p086j.p088b.cjrhisSQCL;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.p092a.p096d.zoToeBNOjF;
import im.getsocial.sdk.invites.p092a.p099f.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.invites.a.i.pdwpUtZXDT */
public final class pdwpUtZXDT extends jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    upgqDBbsrL f2426a;

    /* renamed from: a */
    static /* synthetic */ void m2376a(LinkParams linkParams, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg jjbqyppegg, cjrhisSQCL cjrhissqcl) {
        if (linkParams != null && cjrhissqcl != null) {
            linkParams.put(LinkParams.KEY_CUSTOM_IMAGE, cjrhissqcl.m2004a());
            if (linkParams.containsKey("INVITE_AND_LANDINGPAGE_IMAGES_ARE_EQUAL")) {
                pdwpUtZXDT.m2378b(jjbqyppegg, cjrhissqcl);
            }
        }
    }

    /* renamed from: b */
    private static void m2378b(im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg jjbqyppegg, cjrhisSQCL cjrhissqcl) {
        if (cjrhissqcl != null) {
            if (cjrhissqcl.m2004a() != null) {
                jjbqyppegg.m2273c(cjrhissqcl.m2004a());
            }
            if (cjrhissqcl.m2005b() != null) {
                jjbqyppegg.m2275e(cjrhissqcl.m2005b());
            }
            if (cjrhissqcl.m2006c() != null) {
                jjbqyppegg.m2274d(cjrhissqcl.m2006c());
            }
        }
    }

    /* renamed from: a */
    public final void m2379a(String str, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT pdwputzxdt, final LinkParams linkParams, final InviteCallback inviteCallback) {
        byte[] bArr;
        final im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg a;
        byte[] j;
        im.getsocial.sdk.internal.p030e.upgqDBbsrL c10521;
        byte[] bArr2 = null;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Can not call execute with null channelId");
        im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL b = this.f2426a.m2354b();
        InviteChannel a2 = b.m2286a(str);
        final im.getsocial.sdk.invites.p092a.p094b.cjrhisSQCL cjrhissqcl = new im.getsocial.sdk.invites.p092a.p094b.cjrhisSQCL();
        if (linkParams != null) {
            Object obj = linkParams.get(LinkParams.KEY_CUSTOM_IMAGE);
            if (obj instanceof byte[]) {
                bArr = (byte[]) obj;
                a = im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.m2276a();
                if (pdwputzxdt != null) {
                    a.m2266a(pdwputzxdt.m2279d());
                    a.m2270b(pdwputzxdt.m2280e());
                    a.m2273c(pdwputzxdt.m2281f());
                    a.m2275e(pdwputzxdt.m2284i());
                    a.m2274d(pdwputzxdt.m2283h());
                }
                j = pdwputzxdt != null ? null : pdwputzxdt.m2285j();
                if (pdwputzxdt != null) {
                    bArr2 = pdwputzxdt.m2282g();
                }
                c10521 = new KSZKMmRWhZ<cjrhisSQCL>(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f2416b;

                    /* renamed from: b */
                    public final /* synthetic */ void mo4412b(Object obj) {
                        pdwpUtZXDT.m2378b(a, (cjrhisSQCL) obj);
                    }
                };
                m986a(im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1656a().m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(bArr2), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_INVITE_IMAGE)).m1669b(c10521).m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2008b(j), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_INVITE_IMAGE)).m1669b(c10521).m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(bArr), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_LANDING_PAGE_IMAGE)).m1669b(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<cjrhisSQCL, im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT>(this) {
                    /* renamed from: c */
                    final /* synthetic */ pdwpUtZXDT f2423c;

                    /* renamed from: a */
                    public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                        pdwpUtZXDT.m2376a(linkParams, a, (cjrhisSQCL) obj);
                        return a.m2269a();
                    }
                }).m1669b(new zoToeBNOjF(b.m2287a(), im.getsocial.sdk.invites.upgqDBbsrL.m2410b(a2))).m1669b(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT, Void>(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f2420b;

                    /* renamed from: a */
                    public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                        cjrhissqcl.f2306c = (im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT) obj;
                        return null;
                    }
                }).m1665a(new im.getsocial.sdk.invites.p092a.p096d.pdwpUtZXDT(str, linkParams)).m1669b(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ, im.getsocial.sdk.invites.p092a.p094b.cjrhisSQCL>(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f2418b;

                    /* renamed from: a */
                    public final /* synthetic */ Object mo4344a(Object obj) {
                        im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ xdbacJlTDQ = (im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ) obj;
                        cjrhissqcl.f2304a = xdbacJlTDQ.m2264a();
                        cjrhissqcl.f2305b = xdbacJlTDQ.m2265b();
                        return cjrhissqcl;
                    }
                }).m1665a(new im.getsocial.sdk.invites.p092a.p096d.XdbacJlTDQ(a2)), new CompletionCallback(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f2425b;

                    public void onFailure(GetSocialException getSocialException) {
                        if (getSocialException instanceof im.getsocial.sdk.invites.p092a.p095c.jjbQypPegg) {
                            pdwpUtZXDT.c.mo4387a("Invite cancelled by user.");
                            inviteCallback.onCancel();
                            return;
                        }
                        inviteCallback.onError(getSocialException);
                    }

                    public void onSuccess() {
                        inviteCallback.onComplete();
                    }
                });
            }
        }
        bArr = null;
        a = im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.m2276a();
        if (pdwputzxdt != null) {
            a.m2266a(pdwputzxdt.m2279d());
            a.m2270b(pdwputzxdt.m2280e());
            a.m2273c(pdwputzxdt.m2281f());
            a.m2275e(pdwputzxdt.m2284i());
            a.m2274d(pdwputzxdt.m2283h());
        }
        if (pdwputzxdt != null) {
        }
        if (pdwputzxdt != null) {
            bArr2 = pdwputzxdt.m2282g();
        }
        c10521 = /* anonymous class already generated */;
        m986a(im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1656a().m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(bArr2), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_INVITE_IMAGE)).m1669b(c10521).m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2008b(j), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_INVITE_IMAGE)).m1669b(c10521).m1665a(new im.getsocial.sdk.internal.p086j.p087a.jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg.m2007a(bArr), im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT.CUSTOM_LANDING_PAGE_IMAGE)).m1669b(/* anonymous class already generated */).m1669b(new zoToeBNOjF(b.m2287a(), im.getsocial.sdk.invites.upgqDBbsrL.m2410b(a2))).m1669b(/* anonymous class already generated */).m1665a(new im.getsocial.sdk.invites.p092a.p096d.pdwpUtZXDT(str, linkParams)).m1669b(/* anonymous class already generated */).m1665a(new im.getsocial.sdk.invites.p092a.p096d.XdbacJlTDQ(a2)), /* anonymous class already generated */);
    }
}
