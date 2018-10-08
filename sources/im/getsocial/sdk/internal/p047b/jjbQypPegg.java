package im.getsocial.sdk.internal.p047b;

import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.GlobalErrorListener;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;

/* renamed from: im.getsocial.sdk.internal.b.jjbQypPegg */
public final class jjbQypPegg implements GlobalErrorListener {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg f1210a;
    /* renamed from: b */
    private final GlobalErrorListener f1211b;

    public jjbQypPegg(GlobalErrorListener globalErrorListener) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f1211b = globalErrorListener;
    }

    public final void onError(final GetSocialException getSocialException) {
        this.f1210a.m1245b(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1209b;

            public void run() {
                this.f1209b.f1211b.onError(getSocialException);
            }
        });
    }
}
