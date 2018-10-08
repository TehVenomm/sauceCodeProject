package im.getsocial.sdk.internal.p033c.p052d.p053a;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.d.a.cjrhisSQCL */
class cjrhisSQCL implements jjbQypPegg {
    /* renamed from: a */
    private final Callback f1259a;

    cjrhisSQCL(Callback callback) {
        this.f1259a = callback;
    }

    /* renamed from: a */
    public final void mo4382a(GetSocialException getSocialException) {
        if (this.f1259a != null) {
            this.f1259a.onFailure(getSocialException);
        }
    }
}
