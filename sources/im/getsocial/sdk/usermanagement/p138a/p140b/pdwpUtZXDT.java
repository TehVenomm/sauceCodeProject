package im.getsocial.sdk.usermanagement.p138a.p140b;

import com.facebook.AccessToken;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.usermanagement.p138a.p139a.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.usermanagement.a.b.pdwpUtZXDT */
public class pdwpUtZXDT implements upgqDBbsrL<XdbacJlTDQ, cjrhisSQCL> {
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: a */
    bpiSwUyLit f3297a;

    public pdwpUtZXDT() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        obj = (XdbacJlTDQ) obj;
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj), "Can not load credentials for null AppId");
        Object obj2 = (this.f3297a.mo4361a(AccessToken.USER_ID_KEY) && this.f3297a.mo4361a("user_password")) ? 1 : null;
        return (obj2 == null || !obj.m1132a().equals(this.f3297a.mo4362b("app_id"))) ? null : new cjrhisSQCL(this.f3297a.mo4362b(AccessToken.USER_ID_KEY), this.f3297a.mo4362b("user_password"));
    }
}
