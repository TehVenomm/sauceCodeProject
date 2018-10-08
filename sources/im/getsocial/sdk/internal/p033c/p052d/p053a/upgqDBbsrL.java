package im.getsocial.sdk.internal.p033c.p052d.p053a;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.usermanagement.AddAuthIdentityCallback;

/* renamed from: im.getsocial.sdk.internal.c.d.a.upgqDBbsrL */
public class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private final AddAuthIdentityCallback f1261a;

    upgqDBbsrL(AddAuthIdentityCallback addAuthIdentityCallback) {
        this.f1261a = addAuthIdentityCallback;
    }

    /* renamed from: a */
    public final void mo4382a(GetSocialException getSocialException) {
        if (this.f1261a != null) {
            this.f1261a.onFailure(getSocialException);
        }
    }
}
