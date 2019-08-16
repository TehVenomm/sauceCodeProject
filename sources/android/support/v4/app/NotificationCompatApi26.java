package android.support.p000v4.app;

import android.app.Notification;
import android.app.PendingIntent;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.p000v4.app.NotificationCompatBase.Action;
import android.widget.RemoteViews;
import java.util.ArrayList;
import java.util.Iterator;

@RequiresApi(26)
/* renamed from: android.support.v4.app.NotificationCompatApi26 */
class NotificationCompatApi26 {

    /* renamed from: android.support.v4.app.NotificationCompatApi26$Builder */
    public static class Builder implements NotificationBuilderWithBuilderAccessor, NotificationBuilderWithActions {

        /* renamed from: mB */
        private android.app.Notification.Builder f14mB;

        Builder(Context context, Notification notification, CharSequence charSequence, CharSequence charSequence2, CharSequence charSequence3, RemoteViews remoteViews, int i, PendingIntent pendingIntent, PendingIntent pendingIntent2, Bitmap bitmap, int i2, int i3, boolean z, boolean z2, boolean z3, int i4, CharSequence charSequence4, boolean z4, String str, ArrayList<String> arrayList, Bundle bundle, int i5, int i6, Notification notification2, String str2, boolean z5, String str3, CharSequence[] charSequenceArr, RemoteViews remoteViews2, RemoteViews remoteViews3, RemoteViews remoteViews4, String str4, int i7, String str5, long j, boolean z6, boolean z7, int i8) {
            this.f14mB = new android.app.Notification.Builder(context, str4).setWhen(notification.when).setShowWhen(z2).setSmallIcon(notification.icon, notification.iconLevel).setContent(notification.contentView).setTicker(notification.tickerText, remoteViews).setSound(notification.sound, notification.audioStreamType).setVibrate(notification.vibrate).setLights(notification.ledARGB, notification.ledOnMS, notification.ledOffMS).setOngoing((notification.flags & 2) != 0).setOnlyAlertOnce((notification.flags & 8) != 0).setAutoCancel((notification.flags & 16) != 0).setDefaults(notification.defaults).setContentTitle(charSequence).setContentText(charSequence2).setSubText(charSequence4).setContentInfo(charSequence3).setContentIntent(pendingIntent).setDeleteIntent(notification.deleteIntent).setFullScreenIntent(pendingIntent2, (notification.flags & 128) != 0).setLargeIcon(bitmap).setNumber(i).setUsesChronometer(z3).setPriority(i4).setProgress(i2, i3, z).setLocalOnly(z4).setExtras(bundle).setGroup(str2).setGroupSummary(z5).setSortKey(str3).setCategory(str).setColor(i5).setVisibility(i6).setPublicVersion(notification2).setRemoteInputHistory(charSequenceArr).setChannelId(str4).setBadgeIconType(i7).setShortcutId(str5).setTimeoutAfter(j).setGroupAlertBehavior(i8);
            if (z7) {
                this.f14mB.setColorized(z6);
            }
            if (remoteViews2 != null) {
                this.f14mB.setCustomContentView(remoteViews2);
            }
            if (remoteViews3 != null) {
                this.f14mB.setCustomBigContentView(remoteViews3);
            }
            if (remoteViews4 != null) {
                this.f14mB.setCustomHeadsUpContentView(remoteViews4);
            }
            Iterator it = arrayList.iterator();
            while (it.hasNext()) {
                this.f14mB.addPerson((String) it.next());
            }
        }

        public void addAction(Action action) {
            NotificationCompatApi24.addAction(this.f14mB, action);
        }

        public Notification build() {
            return this.f14mB.build();
        }

        public android.app.Notification.Builder getBuilder() {
            return this.f14mB;
        }
    }

    NotificationCompatApi26() {
    }
}
