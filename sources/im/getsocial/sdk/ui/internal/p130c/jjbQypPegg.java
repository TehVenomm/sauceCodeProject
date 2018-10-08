package im.getsocial.sdk.ui.internal.p130c;

import android.app.Application;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.internal.p033c.p041b.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p041b.pdwpUtZXDT;
import im.getsocial.sdk.internal.p091l.upgqDBbsrL;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationListener;
import im.getsocial.sdk.sharedl10n.Localization;
import im.getsocial.sdk.sharedl10n.Localization.UserLanguageCodeProvider;

/* renamed from: im.getsocial.sdk.ui.internal.c.jjbQypPegg */
public final class jjbQypPegg implements upgqDBbsrL {

    /* renamed from: im.getsocial.sdk.ui.internal.c.jjbQypPegg$1 */
    class C11351 implements cjrhisSQCL<im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2780a;

        C11351(jjbQypPegg jjbqyppegg) {
            this.f2780a = jjbqyppegg;
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4357a(pdwpUtZXDT pdwputzxdt) {
            return im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.c.jjbQypPegg$2 */
    class C11372 implements cjrhisSQCL<Localization> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2782a;

        /* renamed from: im.getsocial.sdk.ui.internal.c.jjbQypPegg$2$1 */
        class C11361 implements UserLanguageCodeProvider {
            /* renamed from: a */
            final /* synthetic */ C11372 f2781a;

            C11361(C11372 c11372) {
                this.f2781a = c11372;
            }

            public String getCurrentLanguageCode() {
                return GetSocial.getLanguage();
            }
        }

        C11372(jjbQypPegg jjbqyppegg) {
            this.f2782a = jjbqyppegg;
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4357a(pdwpUtZXDT pdwputzxdt) {
            return new Localization(new C11361(this));
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.c.jjbQypPegg$3 */
    class C11383 implements NotificationListener {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f2783a;

        C11383(jjbQypPegg jjbqyppegg) {
            this.f2783a = jjbqyppegg;
        }

        public boolean onNotificationReceived(Notification notification, boolean z) {
            return im.getsocial.sdk.ui.jjbQypPegg.m3624a(notification, z);
        }
    }

    /* renamed from: a */
    public final void mo4720a(Application application) {
        im.getsocial.sdk.ui.jjbQypPegg.m3623a(application);
    }

    /* renamed from: a */
    public final void mo4721a(pdwpUtZXDT pdwputzxdt) {
        pdwputzxdt.m1208a(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.class, new C11351(this));
        pdwputzxdt.m1208a(Localization.class, new C11372(this));
        pdwputzxdt.m1209a(im.getsocial.sdk.ui.internal.cjrhisSQCL.class, im.getsocial.sdk.ui.internal.cjrhisSQCL.class);
        pdwputzxdt.m1214b(NotificationListener.class, new C11383(this));
    }
}
