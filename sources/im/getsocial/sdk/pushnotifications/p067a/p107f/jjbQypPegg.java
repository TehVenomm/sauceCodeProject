package im.getsocial.sdk.pushnotifications.p067a.p107f;

import im.getsocial.sdk.internal.p070f.p071a.DvmrLquonW;
import im.getsocial.sdk.internal.p070f.p071a.JWvbLzaedN;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery.Filter;
import im.getsocial.sdk.pushnotifications.p067a.p103b.upgqDBbsrL;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.pushnotifications.a.f.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static DvmrLquonW m2449a(NotificationsCountQuery notificationsCountQuery) {
        return jjbQypPegg.m2451a(im.getsocial.sdk.pushnotifications.jjbQypPegg.m2469a(notificationsCountQuery));
    }

    /* renamed from: a */
    public static DvmrLquonW m2450a(NotificationsQuery notificationsQuery) {
        return jjbQypPegg.m2451a(im.getsocial.sdk.pushnotifications.jjbQypPegg.m2470a(notificationsQuery));
    }

    /* renamed from: a */
    private static DvmrLquonW m2451a(upgqDBbsrL upgqdbbsrl) {
        String str = null;
        DvmrLquonW dvmrLquonW = new DvmrLquonW();
        dvmrLquonW.f1579e = upgqdbbsrl.f2469c;
        dvmrLquonW.f1576b = upgqdbbsrl.f2470d == Filter.NEWER ? upgqdbbsrl.f2471e : null;
        if (upgqdbbsrl.f2470d == Filter.OLDER) {
            str = upgqdbbsrl.f2471e;
        }
        dvmrLquonW.f1577c = str;
        dvmrLquonW.f1575a = Integer.valueOf(upgqdbbsrl.f2467a);
        int[] iArr = upgqdbbsrl.f2468b;
        List arrayList = new ArrayList();
        for (int valueOf : iArr) {
            arrayList.add(Integer.valueOf(valueOf));
        }
        dvmrLquonW.f1578d = arrayList;
        return dvmrLquonW;
    }

    /* renamed from: a */
    public static Notification m2452a(JWvbLzaedN jWvbLzaedN) {
        return new Notification(jWvbLzaedN.f1599a, jWvbLzaedN.f1602d.booleanValue(), jWvbLzaedN.f1601c.intValue(), (long) jWvbLzaedN.f1600b.intValue(), jWvbLzaedN.f1606h, jWvbLzaedN.f1605g, jWvbLzaedN.f1603e.intValue(), jWvbLzaedN.f1604f);
    }
}
