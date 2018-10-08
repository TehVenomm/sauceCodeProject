package im.getsocial.sdk.invites.p092a.p100g;

import com.google.firebase.analytics.FirebaseAnalytics.Param;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.wWemqSpYTx;
import im.getsocial.sdk.internal.p036a.p044g.cjrhisSQCL;
import im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.g.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: a */
    private final wWemqSpYTx f2401a;
    /* renamed from: b */
    private final im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f2402b;
    /* renamed from: c */
    private final bpiSwUyLit f2403c;
    /* renamed from: d */
    private final cjrhisSQCL f2404d;

    /* renamed from: im.getsocial.sdk.invites.a.g.jjbQypPegg$1 */
    class C10481 implements CompletionCallback {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2399a;

        C10481(jjbQypPegg jjbqyppegg) {
            this.f2399a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
        }

        public void onSuccess() {
        }
    }

    @XdbacJlTDQ
    jjbQypPegg(wWemqSpYTx wwemqspytx, im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg jjbqyppegg, bpiSwUyLit bpiswuylit, cjrhisSQCL cjrhissqcl) {
        this.f2401a = wwemqspytx;
        this.f2402b = jjbqyppegg;
        this.f2403c = bpiswuylit;
        this.f2404d = cjrhissqcl;
    }

    /* renamed from: a */
    public final void m2355a(pdwpUtZXDT pdwputzxdt, Map<String, String> map) {
        m2356a(pdwputzxdt, map, new C10481(this));
    }

    /* renamed from: a */
    public final void m2356a(pdwpUtZXDT pdwputzxdt, Map<String, String> map, CompletionCallback completionCallback) {
        Object obj;
        boolean a = this.f2401a.mo4553a();
        if (a && pdwputzxdt == pdwpUtZXDT.DEEP_LINK) {
            obj = 1;
        } else {
            String str;
            if (a) {
                str = (String) map.get(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a);
                Map hashMap = new HashMap();
                hashMap.put("source", pdwputzxdt.name());
                hashMap.put(Param.CONTENT, str);
                hashMap.put("duration", String.valueOf(this.f2404d.mo4353b() - this.f2403c.mo4363c("application_did_become_active_event_timestamp")));
                this.f2402b.m1053a("install_referrer_received_after_init", hashMap);
            } else {
                bpiSwUyLit bpiswuylit = this.f2403c;
                switch (pdwputzxdt) {
                    case FACEBOOK:
                        str = "facebook_referrer";
                        break;
                    case GOOGLE_PLAY:
                        str = "google_referrer";
                        break;
                    case DEEP_LINK:
                        str = "deep_link_referrer";
                        break;
                    default:
                        throw new IllegalArgumentException();
                }
                bpiswuylit.mo4360a(str, im.getsocial.p015a.p016a.pdwpUtZXDT.m725a(new im.getsocial.p015a.p016a.pdwpUtZXDT(map)));
            }
            obj = null;
        }
        if (obj != null) {
            new im.getsocial.sdk.invites.p092a.p102i.XdbacJlTDQ().m2368a((String) map.get(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a), completionCallback);
        } else {
            completionCallback.onSuccess();
        }
    }
}
