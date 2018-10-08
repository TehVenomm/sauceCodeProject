package im.getsocial.sdk.internal.p036a.p046i;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.a.i.jjbQypPegg */
public final class jjbQypPegg extends im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p036a.p040d.jjbQypPegg f1204a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg f1205b;

    /* renamed from: a */
    static /* synthetic */ int m1054a(jjbQypPegg jjbqyppegg) {
        upgqDBbsrL b = jjbqyppegg.f1205b.m1378b();
        if (b == null || b.m1325e() == null) {
            return 0;
        }
        Map c = b.m1325e().m1571c();
        return c.containsKey("retry_count") ? Integer.parseInt((String) c.get("retry_count")) : 0;
    }

    /* renamed from: a */
    public final void m1055a() {
        final List a = this.f1204a.mo4347a();
        if (!a.isEmpty()) {
            m987b(m1056b(), new CompletionCallback(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f1203b;

                public void onFailure(GetSocialException getSocialException) {
                    if (getSocialException.getErrorCode() == ErrorCode.CONNECTION_TIMEOUT) {
                        for (im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg jjbqyppegg : a) {
                            if (jjbqyppegg.m1021e() <= ((long) jjbQypPegg.m1054a(this.f1203b))) {
                                jjbqyppegg.m1022f();
                                this.f1203b.f1204a.mo4348a(jjbqyppegg);
                            }
                        }
                        return;
                    }
                    this.f1203b.f1204a.mo4349a(a);
                }

                public void onSuccess() {
                }
            });
        }
    }

    /* renamed from: b */
    public final pdwpUtZXDT m1056b() {
        Object a = this.f1204a.mo4347a();
        if (a.isEmpty()) {
            return pdwpUtZXDT.m1656a();
        }
        this.f1204a.mo4350b();
        return pdwpUtZXDT.m1659a(a).m1665a(new im.getsocial.sdk.internal.p036a.p039c.jjbQypPegg());
    }
}
