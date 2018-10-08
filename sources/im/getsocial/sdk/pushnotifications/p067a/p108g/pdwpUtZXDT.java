package im.getsocial.sdk.pushnotifications.p067a.p108g;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.SKUqohGtGQ;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.ruWsnwUPKh;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.Notification.Key.OpenUrl;
import im.getsocial.sdk.pushnotifications.NotificationListener;
import im.getsocial.sdk.pushnotifications.p067a.p105d.upgqDBbsrL;
import java.util.Arrays;
import java.util.Collections;

/* renamed from: im.getsocial.sdk.pushnotifications.a.g.pdwpUtZXDT */
public final class pdwpUtZXDT extends jjbQypPegg {
    @XdbacJlTDQ
    /* renamed from: a */
    upgqDBbsrL f2502a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg f2503b;
    @XdbacJlTDQ
    /* renamed from: f */
    NotificationListener f2504f;
    @XdbacJlTDQ
    /* renamed from: g */
    ruWsnwUPKh f2505g;
    @XdbacJlTDQ
    /* renamed from: h */
    SKUqohGtGQ f2506h;

    /* renamed from: im.getsocial.sdk.pushnotifications.a.g.pdwpUtZXDT$jjbQypPegg */
    private class jjbQypPegg implements NotificationListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f2501a;

        private jjbQypPegg(pdwpUtZXDT pdwputzxdt) {
            this.f2501a = pdwputzxdt;
        }

        public boolean onNotificationReceived(Notification notification, boolean z) {
            if (!z || notification.getActionType() != 4) {
                return false;
            }
            this.f2501a.f2505g.mo4381a((String) notification.getActionData().get(OpenUrl.URL));
            return true;
        }
    }

    /* renamed from: a */
    static /* synthetic */ void m2457a(pdwpUtZXDT pdwputzxdt, im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ xdbacJlTDQ) {
        for (NotificationListener notificationListener : Arrays.asList(new NotificationListener[]{pdwputzxdt.f2502a.m2440c(), new jjbQypPegg(), pdwputzxdt.f2504f})) {
            if (notificationListener != null && notificationListener.onNotificationReceived(xdbacJlTDQ.m2421c(), xdbacJlTDQ.m2423d())) {
                return;
            }
        }
    }

    /* renamed from: a */
    public final void m2458a(final im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ xdbacJlTDQ) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) xdbacJlTDQ), "Notification can not be null");
        if (this.f2503b.m3698b() == null) {
            throw new RuntimeException("User is not authenticated yet.");
        } else if (!xdbacJlTDQ.m2422c(this.f2503b.m3698b().getId())) {
            throw new RuntimeException("Notification is not for current user.");
        } else if (xdbacJlTDQ.m2423d()) {
            m986a(m988c().mo4435a(Collections.singletonList(xdbacJlTDQ.m2421c().getId()), true), new CompletionCallback(this) {
                /* renamed from: b */
                final /* synthetic */ pdwpUtZXDT f2500b;

                public void onFailure(GetSocialException getSocialException) {
                    pdwpUtZXDT.m2457a(this.f2500b, xdbacJlTDQ);
                }

                public void onSuccess() {
                    pdwpUtZXDT.m2457a(this.f2500b, xdbacJlTDQ);
                }
            });
        } else {
            this.f2506h.mo4358a(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ pdwpUtZXDT f2498b;

                public void run() {
                    pdwpUtZXDT.m2457a(this.f2498b, xdbacJlTDQ);
                }
            });
        }
    }
}
