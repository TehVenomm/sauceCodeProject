package im.getsocial.sdk.ui;

import android.app.Application;
import android.content.Context;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.internal.p091l.upgqDBbsrL;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder;
import im.getsocial.sdk.ui.activities.ActivityFeedViewBuilder;
import im.getsocial.sdk.ui.internal.jjbQypPegg;
import im.getsocial.sdk.ui.invites.InvitesViewBuilder;

public final class GetSocialUi {
    /* renamed from: a */
    private static jjbQypPegg f2535a;

    private GetSocialUi() {
    }

    /* renamed from: a */
    static upgqDBbsrL m2504a() {
        return new im.getsocial.sdk.ui.internal.p130c.jjbQypPegg();
    }

    /* renamed from: a */
    static void m2505a(Application application) {
        m2507b().m3447a(application);
    }

    /* renamed from: a */
    static boolean m2506a(Notification notification, boolean z) {
        return m2507b().m3451a(notification, z);
    }

    /* renamed from: b */
    private static jjbQypPegg m2507b() {
        if (f2535a == null) {
            f2535a = new jjbQypPegg();
        }
        return f2535a;
    }

    public static void closeView() {
        closeView(false);
    }

    public static void closeView(boolean z) {
        m2507b().m3448a(z);
    }

    public static ActivityDetailsViewBuilder createActivityDetailsView(String str) {
        m2507b();
        return jjbQypPegg.m3441b(str);
    }

    public static ActivityFeedViewBuilder createActivityFeedView(String str) {
        m2507b();
        return jjbQypPegg.m3433a(str);
    }

    public static ActivityFeedViewBuilder createGlobalActivityFeedView() {
        m2507b();
        return jjbQypPegg.m3433a(ActivitiesQuery.GLOBAL_FEED);
    }

    public static InvitesViewBuilder createInvitesView() {
        m2507b();
        return jjbQypPegg.m3438a();
    }

    public static boolean loadConfiguration(Context context, String str) {
        return m2507b().m3450a(context, str);
    }

    public static boolean loadDefaultConfiguration(Context context) {
        return m2507b().m3449a(context);
    }

    public static boolean onBackPressed() {
        return m2507b().m3454c();
    }

    public static void restoreView() {
        m2507b().m3453b();
    }

    public static boolean showView(ViewBuilder viewBuilder) {
        return m2507b().m3452a(viewBuilder);
    }
}
