package im.getsocial.sdk.usermanagement.p138a.p140b;

import com.facebook.AccessToken;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.usermanagement.PrivateUser;

/* renamed from: im.getsocial.sdk.usermanagement.a.b.ztWNWCuZiM */
public class ztWNWCuZiM extends KSZKMmRWhZ<zoToeBNOjF<XdbacJlTDQ, PrivateUser>> {
    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    /* renamed from: a */
    bpiSwUyLit f3303a;

    public ztWNWCuZiM() {
        im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: b */
    public final /* synthetic */ void mo4412b(Object obj) {
        obj = (zoToeBNOjF) obj;
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj), "Can not persist null appId and private user");
        Object obj2 = (XdbacJlTDQ) obj.mo4497a();
        Object obj3 = (PrivateUser) obj.mo4498b();
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj3), "Can not persist null private user");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj2), "Can not persist null app id");
        this.f3303a.mo4360a(AccessToken.USER_ID_KEY, obj3.getId());
        this.f3303a.mo4360a("user_password", obj3.getPassword());
        this.f3303a.mo4360a("app_id", obj2.m1132a());
    }
}
