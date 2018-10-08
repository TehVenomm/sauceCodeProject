package im.getsocial.sdk.invites;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;

public class MultipleInstallReferrerReceiver extends BroadcastReceiver {
    /* renamed from: a */
    private static final cjrhisSQCL f2285a = upgqDBbsrL.m1274a(MultipleInstallReferrerReceiver.class);
    /* renamed from: b */
    private static boolean f2286b;

    /* renamed from: a */
    private static void m2255a(Context context, Intent intent) {
        for (ResolveInfo resolveInfo : context.getPackageManager().queryBroadcastReceivers(new Intent("com.android.vending.INSTALL_REFERRER"), 0)) {
            String str = resolveInfo.activityInfo.name;
            boolean equals = MultipleInstallReferrerReceiver.class.getName().equals(str);
            boolean equals2 = InstallReferrerReceiver.class.getName().equals(str);
            if (!(!context.getPackageName().equals(resolveInfo.activityInfo.packageName) || equals || equals2)) {
                f2285a.mo4387a("Invoke onReceive on class: " + str);
                try {
                    ((BroadcastReceiver) Class.forName(str).newInstance()).onReceive(context, intent);
                } catch (Throwable th) {
                    f2285a.mo4394c("Failed to invoke BroadcastReceiver: %s", str);
                    f2285a.mo4395c(th);
                }
            }
        }
    }

    public void onReceive(Context context, Intent intent) {
        f2285a.mo4387a("MultipleInstallReferrerReceiver invoked.");
        if (intent == null) {
            return;
        }
        if (f2286b) {
            f2285a.mo4387a("Received duplicate intent. Ignoring.");
        } else if ("com.android.vending.INSTALL_REFERRER".equals(intent.getAction())) {
            f2286b = true;
            InstallReferrerReceiver.onIntentReceived(context, intent);
            m2255a(context, intent);
        }
    }
}
