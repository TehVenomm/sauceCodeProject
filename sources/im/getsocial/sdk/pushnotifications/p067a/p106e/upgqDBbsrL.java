package im.getsocial.sdk.pushnotifications.p067a.p106e;

import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import com.google.android.gms.drive.DriveFile;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg;
import java.util.List;

/* renamed from: im.getsocial.sdk.pushnotifications.a.e.upgqDBbsrL */
class upgqDBbsrL extends cjrhisSQCL {
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2495a;
    @XdbacJlTDQ
    /* renamed from: b */
    bpiSwUyLit f2496b;

    upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final void mo4577a(Context context, Intent intent) {
        im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ a = im.getsocial.sdk.pushnotifications.p067a.jjbQypPegg.m2463a(intent);
        if (a != null) {
            List queryIntentActivities = context.getPackageManager().queryIntentActivities(new Intent("getsocial.intent.action.NOTIFICATION_RECEIVE").setPackage(context.getPackageName()), 65536);
            Intent className = queryIntentActivities.isEmpty() ? null : new Intent().setClassName(context, ((ResolveInfo) queryIntentActivities.get(0)).activityInfo.name);
            if (className == null) {
                className = context.getPackageManager().getLaunchIntentForPackage(context.getPackageName());
            }
            if (className != null) {
                im.getsocial.sdk.pushnotifications.p067a.jjbQypPegg.m2467a(this.f2496b, a);
                className.addFlags(DriveFile.MODE_READ_ONLY);
                context.startActivity(className);
            }
            this.f2495a.m1053a("push_notification_clicked", a.m2420b());
        }
    }
}
