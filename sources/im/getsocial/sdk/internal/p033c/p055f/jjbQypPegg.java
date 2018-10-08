package im.getsocial.sdk.internal.p033c.p055f;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.KCGqEGAizh;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;

/* renamed from: im.getsocial.sdk.internal.c.f.jjbQypPegg */
public class jjbQypPegg implements KCGqEGAizh {
    /* renamed from: a */
    private static final cjrhisSQCL f1281a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private final im.getsocial.sdk.internal.p033c.p056i.cjrhisSQCL f1282b;
    /* renamed from: c */
    private jjbQypPegg f1283c = null;

    /* renamed from: im.getsocial.sdk.internal.c.f.jjbQypPegg$jjbQypPegg */
    private final class jjbQypPegg implements im.getsocial.sdk.internal.p033c.p056i.cjrhisSQCL.upgqDBbsrL {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1279a;
        /* renamed from: b */
        private final im.getsocial.sdk.internal.p033c.KCGqEGAizh.jjbQypPegg f1280b;

        /* renamed from: im.getsocial.sdk.internal.c.f.jjbQypPegg$jjbQypPegg$1 */
        class C09401 implements CompletionCallback {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f1278a;

            C09401(jjbQypPegg jjbqyppegg) {
                this.f1278a = jjbqyppegg;
            }

            public void onFailure(GetSocialException getSocialException) {
                jjbQypPegg.f1281a.mo4396d("GetSocial initialization failed with exception:\n" + getSocialException);
            }

            public void onSuccess() {
                jjbQypPegg.f1281a.mo4387a("GetSocial initialization successful");
                this.f1278a.f1279a.f1282b.m1301b(this.f1278a);
                this.f1278a.f1279a.f1283c = null;
            }
        }

        jjbQypPegg(jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p033c.KCGqEGAizh.jjbQypPegg jjbqyppegg2) {
            this.f1279a = jjbqyppegg;
            this.f1280b = jjbqyppegg2;
        }

        /* renamed from: a */
        public final void mo4384a(boolean z) {
            if (z && !GetSocial.isInitialized()) {
                this.f1280b.mo4561a(new C09401(this));
            }
        }
    }

    @XdbacJlTDQ
    jjbQypPegg(im.getsocial.sdk.internal.p033c.p056i.cjrhisSQCL cjrhissqcl) {
        this.f1282b = cjrhissqcl;
    }

    /* renamed from: a */
    public final void mo4385a(im.getsocial.sdk.internal.p033c.KCGqEGAizh.jjbQypPegg jjbqyppegg) {
        if (!GetSocial.isInitialized()) {
            this.f1283c = new jjbQypPegg(this, jjbqyppegg);
            if (this.f1282b.m1302b()) {
                this.f1283c.mo4384a(true);
                return;
            }
            this.f1282b.m1300a(this.f1283c);
            this.f1282b.m1299a();
        }
    }
}
