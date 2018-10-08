package im.getsocial.sdk.pushnotifications.p067a.p106e;

import android.app.Notification;
import android.app.Notification.BigTextStyle;
import android.app.Notification.Builder;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Build.VERSION;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg;
import java.util.HashSet;
import java.util.Set;

/* renamed from: im.getsocial.sdk.pushnotifications.a.e.pdwpUtZXDT */
class pdwpUtZXDT extends cjrhisSQCL {
    /* renamed from: f */
    private static final cjrhisSQCL f2488f = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    /* renamed from: g */
    private static final Set<String> f2489g = new HashSet();
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2490a;
    @XdbacJlTDQ
    /* renamed from: b */
    rFvvVpjzZH f2491b;
    @XdbacJlTDQ
    /* renamed from: c */
    im.getsocial.sdk.internal.p089m.XdbacJlTDQ f2492c;
    @XdbacJlTDQ
    /* renamed from: d */
    im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg f2493d;
    @XdbacJlTDQ
    /* renamed from: e */
    im.getsocial.sdk.internal.upgqDBbsrL f2494e;

    pdwpUtZXDT() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    private static im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg m2445a(Intent intent) {
        try {
            return intent.hasExtra("gs_data") ? im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg.m2414a(intent.getStringExtra("gs_data")) : im.getsocial.sdk.pushnotifications.p067a.jjbQypPegg.m2468b(intent);
        } catch (Exception e) {
            return null;
        }
    }

    /* renamed from: a */
    private boolean m2446a() {
        return this.f2491b.mo4368a("im.getsocial.sdk.ShowNotificationInForeground", false);
    }

    /* renamed from: a */
    public final void mo4577a(Context context, Intent intent) {
        im.getsocial.sdk.pushnotifications.p067a.p103b.jjbQypPegg a = pdwpUtZXDT.m2445a(intent);
        if (a != null && !a.mo4576a()) {
            final im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ xdbacJlTDQ = (im.getsocial.sdk.pushnotifications.p067a.p103b.XdbacJlTDQ) a;
            if (f2489g.add(xdbacJlTDQ.m2420b().get("g_nid"))) {
                f2488f.mo4388a("Received notification:\n%s", xdbacJlTDQ.m2424e());
                this.f2490a.m1053a("push_notification_received", xdbacJlTDQ.m2420b());
                int i = (this.f2492c.m2113b() == im.getsocial.sdk.internal.p089m.cjrhisSQCL.PAUSED || this.f2492c.m2113b() == im.getsocial.sdk.internal.p089m.cjrhisSQCL.NOT_STARTED) ? true : 0;
                if (i != 0 || m2446a()) {
                    Notification notification;
                    Intent data = new Intent("im.getsocial.sdk.intent.RECEIVE").setData(Uri.parse("getSocialNotificationId://" + ((String) xdbacJlTDQ.m2420b().get("g_nid"))));
                    im.getsocial.sdk.pushnotifications.p067a.jjbQypPegg.m2466a(data, xdbacJlTDQ);
                    int currentTimeMillis = (int) (System.currentTimeMillis() % 2147483647L);
                    i = this.f2491b.mo4366a("im.getsocial.sdk.NotificationIcon", 0);
                    if (i == 0) {
                        i = context.getResources().getIdentifier("getsocial_notification_icon", "drawable", context.getApplicationInfo().packageName);
                    }
                    if (i == 0) {
                        i = context.getApplicationInfo().icon;
                    }
                    int a2 = this.f2491b.mo4366a("im.getsocial.sdk.LargeNotificationIcon", 0);
                    Bitmap decodeResource = a2 == 0 ? null : BitmapFactory.decodeResource(context.getResources(), a2);
                    PendingIntent broadcast = PendingIntent.getBroadcast(context, currentTimeMillis, data, 134217728);
                    im.getsocial.sdk.pushnotifications.Notification c = xdbacJlTDQ.m2421c();
                    CharSequence string = c.getTitle() == null ? context.getString(context.getApplicationInfo().labelRes) : c.getTitle();
                    CharSequence text = c.getText();
                    Builder contentIntent = new Builder(context).setSmallIcon(i).setLargeIcon(decodeResource).setTicker(text).setWhen(System.currentTimeMillis()).setSound(RingtoneManager.getDefaultUri(2)).setAutoCancel(true).setContentTitle(string).setContentText(text).setContentIntent(broadcast);
                    if (VERSION.SDK_INT < 16) {
                        notification = contentIntent.getNotification();
                    } else {
                        if (m2446a()) {
                            contentIntent.setPriority(1);
                        }
                        contentIntent.setStyle(new BigTextStyle().bigText(text).setBigContentTitle(string));
                        notification = contentIntent.build();
                    }
                    ((NotificationManager) context.getSystemService("notification")).notify(currentTimeMillis, notification);
                    return;
                }
                this.f2493d.m1244a(new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f2487b;

                    public void run() {
                        im.getsocial.sdk.internal.cjrhisSQCL.m1572a(this.f2487b.f2494e, xdbacJlTDQ);
                    }
                });
            }
        }
    }
}
