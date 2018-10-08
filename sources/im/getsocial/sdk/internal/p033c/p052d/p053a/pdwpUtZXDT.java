package im.getsocial.sdk.internal.p033c.p052d.p053a;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.d.a.pdwpUtZXDT */
class pdwpUtZXDT implements jjbQypPegg {
    /* renamed from: a */
    private final CompletionCallback f1260a;

    pdwpUtZXDT(CompletionCallback completionCallback) {
        this.f1260a = completionCallback;
    }

    /* renamed from: a */
    public final void mo4382a(GetSocialException getSocialException) {
        if (this.f1260a != null) {
            this.f1260a.onFailure(getSocialException);
        }
    }
}
