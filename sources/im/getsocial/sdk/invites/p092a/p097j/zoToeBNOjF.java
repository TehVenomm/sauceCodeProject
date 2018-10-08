package im.getsocial.sdk.invites.p092a.p097j;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.FetchReferralDataCallback;
import im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT;
import im.getsocial.sdk.invites.p092a.p100g.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;
import java.util.Stack;

/* renamed from: im.getsocial.sdk.invites.a.j.zoToeBNOjF */
public class zoToeBNOjF {
    /* renamed from: a */
    private static final cjrhisSQCL f2436a = upgqDBbsrL.m1274a(zoToeBNOjF.class);
    /* renamed from: b */
    private final jjbQypPegg f2437b;
    /* renamed from: c */
    private final im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg f2438c;
    /* renamed from: d */
    private boolean f2439d = false;
    /* renamed from: e */
    private boolean f2440e = false;
    /* renamed from: f */
    private final Stack<FetchReferralDataCallback> f2441f = new Stack();

    /* renamed from: im.getsocial.sdk.invites.a.j.zoToeBNOjF$1 */
    class C10581 implements CompletionCallback {
        /* renamed from: a */
        final /* synthetic */ zoToeBNOjF f2435a;

        C10581(zoToeBNOjF zotoebnojf) {
            this.f2435a = zotoebnojf;
        }

        public void onFailure(GetSocialException getSocialException) {
            this.f2435a.f2439d = false;
            this.f2435a.m2396b();
        }

        public void onSuccess() {
            this.f2435a.f2439d = false;
            this.f2435a.m2396b();
        }
    }

    @XdbacJlTDQ
    protected zoToeBNOjF(jjbQypPegg jjbqyppegg, im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg jjbqyppegg2) {
        this.f2437b = jjbqyppegg;
        this.f2438c = jjbqyppegg2;
    }

    /* renamed from: b */
    private void m2396b() {
        this.f2440e = true;
        if (!this.f2439d) {
            synchronized (this.f2441f) {
                while (!this.f2441f.isEmpty()) {
                    ((FetchReferralDataCallback) this.f2441f.pop()).onSuccess(this.f2438c.m2351b());
                }
            }
        }
    }

    /* renamed from: a */
    public final void m2397a() {
        this.f2440e = false;
    }

    /* renamed from: a */
    public final void m2398a(FetchReferralDataCallback fetchReferralDataCallback) {
        if (!this.f2440e || this.f2439d) {
            synchronized (this.f2441f) {
                this.f2441f.push(fetchReferralDataCallback);
            }
            return;
        }
        fetchReferralDataCallback.onSuccess(this.f2438c.m2351b());
    }

    /* renamed from: a */
    public final void m2399a(String str) {
        if (im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1517c(str)) {
            m2396b();
            return;
        }
        f2436a.mo4388a("GetSocial deep link detected: %s", str);
        this.f2439d = true;
        Map hashMap = new HashMap();
        hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a, str);
        this.f2437b.m2356a(pdwpUtZXDT.DEEP_LINK, hashMap, new C10581(this));
    }
}
