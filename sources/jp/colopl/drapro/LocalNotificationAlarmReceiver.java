package jp.colopl.drapro;

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
import jp.colopl.util.Util;

public class LocalNotificationAlarmReceiver extends BroadcastReceiver {
    public static final String EXTRA_ACTIVITY = "activity";
    public static final String EXTRA_BODY = "body";
    public static final String EXTRA_ID = "id";
    public static final String EXTRA_LARGEICON = "largeIcon";
    public static final String EXTRA_SMALLICON = "smallIcon";
    public static final String EXTRA_TITLE = "title";
    public static final String PRIMARY_CHANNEL = "default";
    public static final String nnt_final = "nnt_final";
    public static String nnt_normal = "nnt_normal";

    public void onReceive(Context context, Intent intent) {
        int i;
        CharSequence charSequence;
        CharSequence charSequence2;
        Builder builder;
        Util.dLog("Unity", "SuccessReceive");
        Bundle extras = intent.getExtras();
        int intExtra;
        String stringExtra;
        String stringExtra2;
        if (extras == null || extras.isEmpty()) {
            intExtra = intent.getIntExtra("id", 0);
            stringExtra = intent.getStringExtra("title");
            stringExtra2 = intent.getStringExtra(EXTRA_BODY);
            Util.dLog("Unity", "case 3 ");
            Object obj = stringExtra2;
            i = intExtra;
            Object obj2 = stringExtra;
        } else {
            intExtra = extras.getInt("id");
            stringExtra = extras.getString("title");
            stringExtra2 = extras.getString(EXTRA_BODY);
            Util.dLog("Unity", "case 1 ");
            charSequence = stringExtra2;
            i = intExtra;
            charSequence2 = stringExtra;
        }
        int identifier = context.getResources().getIdentifier("push_icon", "drawable", context.getPackageName());
        Intent intent2 = new Intent(context, StartActivity.class);
        PendingIntent activity = PendingIntent.getActivity(context, 0, intent2, 0);
        Util.dLog("Unity", "newIntent= " + intent2);
        Util.dLog("Unity", "contentIntent= " + activity);
        Bitmap decodeResource = BitmapFactory.decodeResource(context.getResources(), context.getResources().getIdentifier("app_icon", "drawable", context.getPackageName()));
        int dimensionPixelSize = context.getResources().getDimensionPixelSize(17104901);
        int dimensionPixelSize2 = context.getResources().getDimensionPixelSize(17104902);
        Bitmap createScaledBitmap = (decodeResource.getWidth() > dimensionPixelSize || decodeResource.getHeight() > dimensionPixelSize2) ? Bitmap.createScaledBitmap(decodeResource, dimensionPixelSize, dimensionPixelSize2, true) : decodeResource;
        Util.dLog("Unity", "onReceive 111 " + i + " ; " + charSequence2 + " ; " + charSequence + " ; " + identifier + " ; " + dimensionPixelSize + " ; " + dimensionPixelSize2);
        NotificationManager notificationManager = (NotificationManager) context.getSystemService("notification");
        Util.dLog("Unity", "notifyManager= " + notificationManager);
        Util.dLog("Unity", "SDK_INT= " + VERSION.SDK_INT);
        Util.dLog("Unity", "VERSION_CODES= 26");
        if (VERSION.SDK_INT >= 26) {
            NotificationChannel notificationChannel = new NotificationChannel("default", "Primary Channel", 3);
            notificationChannel.setLockscreenVisibility(0);
            notificationManager.createNotificationChannel(notificationChannel);
            builder = new Builder(context, "default");
            Util.dLog("Unity", "nb 1111 = " + builder);
        } else {
            builder = new Builder(context);
            Util.dLog("Unity", "nb 2222 = " + builder);
        }
        builder.setTicker(charSequence2).setContentTitle(charSequence2).setContentText(charSequence).setSmallIcon(identifier).setLargeIcon(createScaledBitmap).setContentIntent(activity).setDefaults(1).setAutoCancel(true);
        notificationManager.notify(i, builder.build());
    }
}
