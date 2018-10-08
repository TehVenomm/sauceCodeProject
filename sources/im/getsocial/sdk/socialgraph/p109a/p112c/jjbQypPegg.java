package im.getsocial.sdk.socialgraph.p109a.p112c;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.socialgraph.p109a.p110a.cjrhisSQCL;
import im.getsocial.sdk.usermanagement.PrivateUser;

/* renamed from: im.getsocial.sdk.socialgraph.a.c.jjbQypPegg */
public final class jjbQypPegg extends im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg f2528a;

    /* renamed from: a */
    public final void m2494a(String str, Callback<Integer> callback) {
        boolean z = false;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) callback), "Can not execute AddFriendUseCase with null callback");
        PrivateUser b = this.f2528a.m3698b();
        boolean z2 = b != null && str.equals(b.getId());
        if (!z2) {
            z = true;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "You can not add yourself to your friend list.");
        cjrhisSQCL.m2482a(str);
        m985a(m988c().mo4445c(str), (Callback) callback);
    }
}
