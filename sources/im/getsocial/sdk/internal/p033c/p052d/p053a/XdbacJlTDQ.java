package im.getsocial.sdk.internal.p033c.p052d.p053a;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg.jjbQypPegg;
import im.getsocial.sdk.invites.FetchReferralDataCallback;

/* renamed from: im.getsocial.sdk.internal.c.d.a.XdbacJlTDQ */
public class XdbacJlTDQ implements jjbQypPegg {
    /* renamed from: a */
    private final FetchReferralDataCallback f1258a;

    XdbacJlTDQ(FetchReferralDataCallback fetchReferralDataCallback) {
        this.f1258a = fetchReferralDataCallback;
    }

    /* renamed from: a */
    public final void mo4382a(GetSocialException getSocialException) {
        if (this.f1258a != null) {
            this.f1258a.onFailure(getSocialException);
        }
    }
}
