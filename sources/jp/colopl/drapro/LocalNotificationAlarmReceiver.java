package p018jp.colopl.drapro;

import android.app.Notification.Builder;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Build.VERSION;
import android.os.Bundle;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.LocalNotificationAlarmReceiver */
public class LocalNotificationAlarmReceiver extends BroadcastReceiver {
    public static final String EXTRA_ACTIVITY = "activity";
    public static final String EXTRA_BODY = "body";
    public static final String EXTRA_ID = "id";
    public static final String EXTRA_LARGEICON = "largeIcon";
    public static final String EXTRA_SMALLICON = "smallIcon";
    public static final String EXTRA_TITLE = "title";
    public static final String PRIMARY_CHANNEL = "default";

    public void onReceive(Context context, Intent intent) {
        int intExtra;
        String stringExtra;
        String str;
        Builder builder;
        Util.dLog("Unity", "SuccessReceive");
        Bundle extras = intent.getExtras();
        if (extras == null || extras.isEmpty()) {
            intExtra = intent.getIntExtra("id", 0);
            String stringExtra2 = intent.getStringExtra("title");
            stringExtra = intent.getStringExtra(EXTRA_BODY);
            str = stringExtra2;
        } else {
            intExtra = extras.getInt("id");
            String string = extras.getString("title");
            stringExtra = extras.getString(EXTRA_BODY);
            str = string;
        }
        int identifier = context.getResources().getIdentifier("push_icon", "drawable", context.getPackageName());
        PendingIntent activity = PendingIntent.getActivity(context, 0, new Intent(context, StartActivity.class), 0);
        Bitmap decodeResource = BitmapFactory.decodeResource(context.getResources(), context.getResources().getIdentifier("app_icon", "drawable", context.getPackageName()));
        int dimensionPixelSize = context.getResources().getDimensionPixelSize(17104901);
        int dimensionPixelSize2 = context.getResources().getDimensionPixelSize(17104902);
        Bitmap createScaledBitmap = (decodeResource.getWidth() > dimensionPixelSize || decodeResource.getHeight() > dimensionPixelSize2) ? Bitmap.createScaledBitmap(decodeResource, dimensionPixelSize, dimensionPixelSize2, true) : decodeResource;
        NotificationManager notificationManager = (NotificationManager) context.getSystemService("notification");
        if (VERSION.SDK_INT >= 26) {
            NotificationChannel notificationChannel = new NotificationChannel("default", "Primary Channel", 3);
            notificationChannel.setLockscreenVisibility(0);
            notificationManager.createNotificationChannel(notificationChannel);
            builder = new Builder(context, "default");
        } else {
            builder = new Builder(context);
        }
        builder.setTicker(str).setContentTitle(str).setContentText(stringExtra).setSmallIcon(identifier).setLargeIcon(createScaledBitmap).setContentIntent(activity).setDefaults(1).setAutoCancel(true);
        notificationManager.notify(intExtra, builder.build());
    }
}
