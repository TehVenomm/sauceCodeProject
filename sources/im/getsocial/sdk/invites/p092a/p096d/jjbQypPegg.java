package im.getsocial.sdk.invites.p092a.p096d;

import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.d.jjbQypPegg */
public final class jjbQypPegg extends im.getsocial.sdk.internal.p030e.jjbQypPegg<pdwpUtZXDT<Void>> {
    /* renamed from: c */
    private static final cjrhisSQCL f2349c = upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.invites.p092a.p097j.cjrhisSQCL f2350a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.invites.p092a.p100g.jjbQypPegg f2351b;
    /* renamed from: d */
    private final boolean f2352d;
    /* renamed from: e */
    private final boolean f2353e;

    /* renamed from: im.getsocial.sdk.invites.a.d.jjbQypPegg$1 */
    class C10371 extends KSZKMmRWhZ<ztWNWCuZiM<? super Boolean>> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2346a;

        C10371(jjbQypPegg jjbqyppegg) {
            this.f2346a = jjbqyppegg;
        }

        /* renamed from: b */
        public final /* synthetic */ void mo4412b(Object obj) {
            ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
            if (this.f2346a.f2352d && this.f2346a.f2353e) {
                jjbQypPegg.f2349c.mo4387a("It's a first app launch, let's check FB referral data");
                this.f2346a.f2350a.mo4572a(new im.getsocial.sdk.invites.p092a.p097j.cjrhisSQCL.jjbQypPegg(this.f2346a, ztwnwcuzim) {
                    /* renamed from: b */
                    final /* synthetic */ jjbQypPegg f2348b;

                    /* renamed from: a */
                    public final void mo4566a(String str) {
                        jjbQypPegg.f2349c.mo4388a("Got a response from fetchDeferredAppLinkData: %s", str);
                        if (!im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM.m1521a(str)) {
                            Map hashMap = new HashMap();
                            hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a, str);
                            this.f2348b.f2351b.m2355a(im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.FACEBOOK, hashMap);
                        }
                        r3.mo4489a(null);
                    }
                });
                return;
            }
            ztwnwcuzim.mo4489a(null);
        }
    }

    public jjbQypPegg(boolean z, boolean z2) {
        im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
        this.f2353e = z;
        this.f2352d = z2;
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4487a() {
        f2349c.mo4388a("CheckFacebookReferrerFunc call, is first open: %s, is new install: %s", Boolean.valueOf(this.f2353e), Boolean.valueOf(this.f2352d));
        return pdwpUtZXDT.m1658a(new C10371(this));
    }
}
