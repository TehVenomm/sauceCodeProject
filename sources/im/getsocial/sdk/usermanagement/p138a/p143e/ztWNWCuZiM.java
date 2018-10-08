package im.getsocial.sdk.usermanagement.p138a.p143e;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.p138a.p139a.cjrhisSQCL;
import im.getsocial.sdk.usermanagement.p138a.p140b.HptYHntaqF;

/* renamed from: im.getsocial.sdk.usermanagement.a.e.ztWNWCuZiM */
public final class ztWNWCuZiM extends jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    upgqDBbsrL f3321a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg f3322b;
    @XdbacJlTDQ
    /* renamed from: f */
    im.getsocial.sdk.internal.p036a.p042e.jjbQypPegg f3323f;
    @XdbacJlTDQ
    /* renamed from: g */
    QhisXzMgay f3324g;
    @XdbacJlTDQ
    /* renamed from: h */
    im.getsocial.sdk.pushnotifications.p067a.p105d.jjbQypPegg f3325h;
    @XdbacJlTDQ
    /* renamed from: i */
    rFvvVpjzZH f3326i;

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.ztWNWCuZiM$1 */
    class C12241 extends im.getsocial.sdk.internal.p030e.ztWNWCuZiM {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f3317a;

        C12241(ztWNWCuZiM ztwnwcuzim) {
            this.f3317a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final void mo4491a() {
            new HptYHntaqF().m3710a();
        }
    }

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.ztWNWCuZiM$2 */
    class C12252 extends im.getsocial.sdk.internal.p030e.jjbQypPegg<Boolean> {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f3318a;

        C12252(ztWNWCuZiM ztwnwcuzim) {
            this.f3318a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4487a() {
            return Boolean.valueOf(im.getsocial.sdk.internal.p033c.p066m.XdbacJlTDQ.m1508a(this.f3318a.f3326i));
        }
    }

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.ztWNWCuZiM$3 */
    class C12263 implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL, zoToeBNOjF<im.getsocial.sdk.internal.p033c.XdbacJlTDQ, PrivateUser>> {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f3319a;

        C12263(ztWNWCuZiM ztwnwcuzim) {
            this.f3319a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4344a(Object obj) {
            im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL upgqdbbsrl = (im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL) obj;
            this.f3319a.f3322b.m3697a(upgqdbbsrl.m3677b());
            this.f3319a.f3323f.m1040a(this.f3319a.f3324g.mo4474o());
            this.f3319a.f3325h.m2431a(upgqdbbsrl.m3679d());
            return zoToeBNOjF.m1677b(this.f3319a.f3321a.m1322b(), this.f3319a.f3322b.m3698b());
        }
    }

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.ztWNWCuZiM$4 */
    class C12274 implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<PrivateUser, cjrhisSQCL> {
        /* renamed from: a */
        final /* synthetic */ ztWNWCuZiM f3320a;

        C12274(ztWNWCuZiM ztwnwcuzim) {
            this.f3320a = ztwnwcuzim;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4344a(Object obj) {
            PrivateUser privateUser = (PrivateUser) obj;
            return new cjrhisSQCL(privateUser.getId(), privateUser.getPassword());
        }
    }

    public ztWNWCuZiM() {
        im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final void m3727a(AuthIdentity authIdentity, CompletionCallback completionCallback) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) authIdentity), "Can not execute SwitchUserUseCase with null authIdentity");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Can not execute SwitchUserUseCase with null callback");
        m986a(pdwpUtZXDT.m1659a((Object) authIdentity).m1669b(HptYHntaqF.m3682a()).m1665a(new im.getsocial.sdk.usermanagement.p138a.p140b.cjrhisSQCL()).m1669b(new C12274(this)).m1665a(new im.getsocial.sdk.usermanagement.p138a.p140b.upgqDBbsrL()).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.zoToeBNOjF()).m1669b(new C12263(this)).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.ztWNWCuZiM()).m1669b(new C12252(this)).m1669b(new im.getsocial.sdk.pushnotifications.p067a.p104c.jjbQypPegg()).m1669b(new C12241(this)), completionCallback);
    }
}
