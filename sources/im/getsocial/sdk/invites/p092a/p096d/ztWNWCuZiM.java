package im.getsocial.sdk.invites.p092a.p096d;

import im.getsocial.sdk.internal.p030e.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.fOrCGNYyfk;
import im.getsocial.sdk.internal.p033c.p041b.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.qZypgoeblR;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.ReferralData;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.d.ztWNWCuZiM */
public final class ztWNWCuZiM extends jjbQypPegg<pdwpUtZXDT<ReferralData>> {
    /* renamed from: e */
    private static final cjrhisSQCL f2369e = upgqDBbsrL.m1274a(ztWNWCuZiM.class);
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg f2370a;
    @XdbacJlTDQ
    /* renamed from: b */
    fOrCGNYyfk f2371b;
    @XdbacJlTDQ
    /* renamed from: c */
    bpiSwUyLit f2372c;
    @XdbacJlTDQ
    @KSZKMmRWhZ(a = "device_info")
    @qZypgoeblR
    /* renamed from: d */
    Map<String, String> f2373d;
    /* renamed from: f */
    private final boolean f2374f;
    /* renamed from: g */
    private final boolean f2375g;
    /* renamed from: h */
    private final String f2376h;

    /* renamed from: im.getsocial.sdk.invites.a.d.ztWNWCuZiM$1 */
    class C10411 implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<Throwable, pdwpUtZXDT<? extends ReferralData>> {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f2367a;

        C10411(ztWNWCuZiM ztwnwcuzim) {
            this.f2367a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4344a(Object obj) {
            Throwable th = (Throwable) obj;
            if (im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th).getErrorCode() != 102) {
                return pdwpUtZXDT.m1660a(th);
            }
            ztWNWCuZiM.f2369e.mo4387a("No referral data was found.");
            ztWNWCuZiM.m2325a(this.f2367a);
            return pdwpUtZXDT.m1659a(null);
        }
    }

    /* renamed from: im.getsocial.sdk.invites.a.d.ztWNWCuZiM$2 */
    class C10422 implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<ReferralData, ReferralData> {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f2368a;

        C10422(ztWNWCuZiM ztwnwcuzim) {
            this.f2368a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4344a(Object obj) {
            ztWNWCuZiM.f2369e.mo4388a("Referral data found: %s", (ReferralData) obj);
            ztWNWCuZiM.m2325a(this.f2368a);
            return (ReferralData) obj;
        }
    }

    public ztWNWCuZiM(String str, boolean z, boolean z2) {
        im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
        this.f2376h = str;
        this.f2375g = z;
        this.f2374f = z2;
    }

    /* renamed from: a */
    static /* synthetic */ void m2325a(ztWNWCuZiM ztwnwcuzim) {
        ztwnwcuzim.m2326a("deep_link_referrer");
        ztwnwcuzim.m2326a("facebook_referrer");
        ztwnwcuzim.m2326a("google_referrer");
    }

    /* renamed from: a */
    private void m2326a(String str) {
        if (this.f2372c.mo4361a(str)) {
            this.f2372c.mo4365e(str);
        }
    }

    /* renamed from: a */
    private void m2327a(String str, im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT pdwputzxdt, Map<im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT, Map<String, String>> map) {
        Map map2 = (Map) map.get(pdwputzxdt);
        if (map2 == null) {
            map2 = new HashMap();
            map.put(pdwputzxdt, map2);
        }
        if (this.f2372c.mo4361a(str)) {
            map2.putAll(ztWNWCuZiM.m2329b(this.f2372c.mo4362b(str)));
        }
    }

    /* renamed from: b */
    private static Map<String, String> m2329b(String str) {
        Map<String, String> hashMap = new HashMap();
        try {
            Object a = new im.getsocial.p015a.p016a.p017a.cjrhisSQCL().m721a(str, null);
            if (a instanceof im.getsocial.p015a.p016a.pdwpUtZXDT) {
                hashMap.putAll((im.getsocial.p015a.p016a.pdwpUtZXDT) a);
            }
        } catch (im.getsocial.p015a.p016a.p017a.pdwpUtZXDT e) {
            f2369e.mo4388a("Could not parse persisted referrel parameters.", e);
        }
        return hashMap;
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4487a() {
        Map hashMap = new HashMap();
        m2327a("facebook_referrer", im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.FACEBOOK, hashMap);
        m2327a("google_referrer", im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.GOOGLE_PLAY, hashMap);
        if (this.f2376h == null) {
            m2327a("deep_link_referrer", im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.DEEP_LINK, hashMap);
        } else {
            Map hashMap2 = new HashMap();
            hashMap2.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a, this.f2376h);
            hashMap.put(im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT.DEEP_LINK, hashMap2);
        }
        im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg jjbqyppegg = this.f2370a;
        fOrCGNYyfk forcgnyyfk = this.f2371b;
        boolean z = this.f2374f && this.f2375g;
        return jjbqyppegg.mo4418a(forcgnyyfk, z, hashMap, this.f2373d).m1669b(new C10422(this)).m1670c(new C10411(this));
    }
}
