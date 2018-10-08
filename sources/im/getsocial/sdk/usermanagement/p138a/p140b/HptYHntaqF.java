package im.getsocial.sdk.usermanagement.p138a.p140b;

import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.usermanagement.AuthIdentity;

/* renamed from: im.getsocial.sdk.usermanagement.a.b.HptYHntaqF */
public class HptYHntaqF implements upgqDBbsrL<AuthIdentity, AuthIdentity> {
    HptYHntaqF() {
    }

    /* renamed from: a */
    public static HptYHntaqF m3682a() {
        return new HptYHntaqF();
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        obj = (AuthIdentity) obj;
        im.getsocial.sdk.usermanagement.upgqDBbsrL upgqdbbsrl = new im.getsocial.sdk.usermanagement.upgqDBbsrL(obj);
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj), "AuthIdentity cannot be null");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(upgqdbbsrl.m3734a()), "ProviderId cannot be null or empty");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(upgqdbbsrl.m3736c()), "AccessToken cannot be null or empty");
        if (!"facebook".equals(upgqdbbsrl.m3734a())) {
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(upgqdbbsrl.m3735b()), "User ID cannot be null or empty for provider " + upgqdbbsrl.m3734a());
        }
        return obj;
    }
}
