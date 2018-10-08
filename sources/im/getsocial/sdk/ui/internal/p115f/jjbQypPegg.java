package im.getsocial.sdk.ui.internal.p115f;

import android.app.Activity;
import android.content.Context;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.sharedl10n.Localization;
import im.getsocial.sdk.ui.internal.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.ui.internal.f.jjbQypPegg */
public class jjbQypPegg {

    /* renamed from: im.getsocial.sdk.ui.internal.f.jjbQypPegg$upgqDBbsrL */
    public static class upgqDBbsrL<P> {
        /* renamed from: a */
        private P f2551a;
        @XdbacJlTDQ
        /* renamed from: b */
        protected Localization f2552b;
        @XdbacJlTDQ
        /* renamed from: c */
        cjrhisSQCL f2553c;

        protected upgqDBbsrL() {
            ztWNWCuZiM.m1221a((Object) this);
        }

        /* renamed from: a */
        public void mo4736a(P p) {
            this.f2551a = p;
        }

        /* renamed from: a */
        protected final void m2523a(Runnable runnable) {
            Activity a = this.f2553c.m3099a();
            if (a != null && !a.isFinishing()) {
                a.runOnUiThread(runnable);
            }
        }

        /* renamed from: n */
        protected final P m2524n() {
            return this.f2551a;
        }

        /* renamed from: o */
        protected final cjrhisSQCL m2525o() {
            return this.f2553c;
        }

        /* renamed from: p */
        protected final Context m2526p() {
            return this.f2553c.m3099a();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.f.jjbQypPegg$jjbQypPegg */
    public static class jjbQypPegg<V extends upgqDBbsrL> {
        /* renamed from: a */
        private final V f2565a;

        protected jjbQypPegg(V v) {
            this.f2565a = v;
            this.f2565a.mo4736a((Object) this);
        }

        /* renamed from: t */
        protected V mo4733t() {
            return this.f2565a;
        }
    }
}
