package im.getsocial.sdk.invites.p092a.p096d;

import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.p065a.zoToeBNOjF;
import im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.qZypgoeblR;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.rWfbqYooCV;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.d.upgqDBbsrL */
public final class upgqDBbsrL extends jjbQypPegg<pdwpUtZXDT<Void>> {
    /* renamed from: c */
    private static final cjrhisSQCL f2360c = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    @XdbacJlTDQ
    @qZypgoeblR
    /* renamed from: a */
    rWfbqYooCV f2361a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.invites.p092a.p100g.jjbQypPegg f2362b;
    /* renamed from: d */
    private final boolean f2363d;
    /* renamed from: e */
    private final boolean f2364e;

    /* renamed from: im.getsocial.sdk.invites.a.d.upgqDBbsrL$1 */
    class C10391 extends KSZKMmRWhZ<ztWNWCuZiM<? super Void>> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2357a;

        C10391(upgqDBbsrL upgqdbbsrl) {
            this.f2357a = upgqdbbsrl;
        }

        /* renamed from: b */
        public final /* synthetic */ void mo4412b(Object obj) {
            ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
            if (this.f2357a.f2363d && this.f2357a.f2364e) {
                upgqDBbsrL.f2360c.mo4387a("It's a first app launch, let's check Google Play referral data");
                upgqDBbsrL.m2314a(this.f2357a, ztwnwcuzim);
                return;
            }
            ztwnwcuzim.mo4489a(null);
        }
    }

    public upgqDBbsrL(boolean z, boolean z2) {
        im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
        this.f2364e = z;
        this.f2363d = z2;
    }

    /* renamed from: a */
    static /* synthetic */ void m2314a(upgqDBbsrL upgqdbbsrl, final ztWNWCuZiM ztwnwcuzim) {
        if (upgqdbbsrl.f2361a == null) {
            ztwnwcuzim.mo4489a(null);
        } else {
            upgqdbbsrl.f2361a.mo4575a(new rWfbqYooCV.jjbQypPegg(upgqdbbsrl) {
                /* renamed from: b */
                final /* synthetic */ upgqDBbsrL f2359b;

                /* renamed from: a */
                public final void mo4567a(Map<String, String> map) {
                    upgqDBbsrL.f2360c.mo4388a("Got a response from fetchReferrer: %s", map);
                    if (map != null) {
                        this.f2359b.f2362b.m2355a(im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.GOOGLE_PLAY, map);
                    }
                    ztwnwcuzim.mo4489a(null);
                }
            });
        }
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4487a() {
        f2360c.mo4388a("CheckGooglePlayReferrerFunc call, is first open: %s, is new install: %s", Boolean.valueOf(this.f2364e), Boolean.valueOf(this.f2363d));
        return pdwpUtZXDT.m1658a(new C10391(this)).m1664a(zoToeBNOjF.m1674a());
    }
}
