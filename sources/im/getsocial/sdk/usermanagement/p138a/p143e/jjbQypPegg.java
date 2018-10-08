package im.getsocial.sdk.usermanagement.p138a.p143e;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.usermanagement.AddAuthIdentityCallback;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.ConflictUser;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.p138a.p140b.HptYHntaqF;
import im.getsocial.sdk.usermanagement.p138a.p140b.XdbacJlTDQ;
import im.getsocial.sdk.usermanagement.p138a.p140b.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.usermanagement.a.e.jjbQypPegg */
public final class jjbQypPegg extends im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg {

    /* renamed from: im.getsocial.sdk.usermanagement.a.e.jjbQypPegg$2 */
    class C12222 implements upgqDBbsrL<PrivateUser, ConflictUser> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f3313a;

        C12222(jjbQypPegg jjbqyppegg) {
            this.f3313a = jjbqyppegg;
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.usermanagement.jjbQypPegg.m3733a((PrivateUser) obj);
        }
    }

    /* renamed from: a */
    public final void m3718a(final AuthIdentity authIdentity, final AddAuthIdentityCallback addAuthIdentityCallback) {
        m986a(pdwpUtZXDT.m1659a((Object) authIdentity).m1669b(HptYHntaqF.m3682a()).m1665a(new im.getsocial.sdk.usermanagement.p138a.p140b.jjbQypPegg()).m1669b(new XdbacJlTDQ()), new CompletionCallback(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f3312c;

            public void onFailure(GetSocialException getSocialException) {
                if (getSocialException instanceof im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg) {
                    im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg jjbqyppegg = (im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg) getSocialException;
                    if (jjbqyppegg.m1135a(101)) {
                        jjbQypPegg.c.mo4387a("AddUserIdentityUseCase let's fetch conflict user");
                        this.f3312c.m985a(pdwpUtZXDT.m1659a((Object) authIdentity).m1665a(new cjrhisSQCL()).m1669b(new C12222(this.f3312c)), new Callback<ConflictUser>(this.f3312c, addAuthIdentityCallback) {
                            /* renamed from: b */
                            final /* synthetic */ jjbQypPegg f3315b;

                            public void onFailure(GetSocialException getSocialException) {
                                if (getSocialException.getErrorCode() == ErrorCode.ILLEGAL_ARGUMENT) {
                                    r4.onFailure(new GetSocialException(ErrorCode.USERID_TOKEN_MISMATCH, "UserId and token do not match"));
                                } else {
                                    r4.onFailure(getSocialException);
                                }
                            }

                            public /* synthetic */ void onSuccess(Object obj) {
                                r4.onConflict((ConflictUser) obj);
                            }
                        });
                        return;
                    }
                    addAuthIdentityCallback.onFailure(jjbqyppegg);
                } else if (getSocialException.getErrorCode() == 101) {
                    jjbQypPegg.c.mo4387a("AddUserIdentityUseCase let's fetch conflict user");
                    this.f3312c.m985a(pdwpUtZXDT.m1659a((Object) authIdentity).m1665a(new cjrhisSQCL()).m1669b(new C12222(this.f3312c)), /* anonymous class already generated */);
                } else {
                    addAuthIdentityCallback.onFailure(getSocialException);
                }
            }

            public void onSuccess() {
                addAuthIdentityCallback.onComplete();
            }
        });
    }
}
