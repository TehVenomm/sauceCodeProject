package jp.colopl.drapro;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.app.NotificationCompat.Builder;
import jp.colopl.util.Util;

public class LocalNotificationAlarmReceiver extends BroadcastReceiver {
    public static final String EXTRA_ACTIVITY = "activity";
    public static final String EXTRA_BODY = "body";
    public static final String EXTRA_ID = "id";
    public static final String EXTRA_LARGEICON = "largeIcon";
    public static final String EXTRA_SMALLICON = "smallIcon";
    public static final String EXTRA_TITLE = "title";

    public void onReceive(Context context, Intent intent) {
        Util.dLog("Unity", "SuccessReceive");
        int intExtra = intent.getIntExtra("id", 0);
        CharSequence stringExtra = intent.getStringExtra("title");
        CharSequence stringExtra2 = intent.getStringExtra(EXTRA_BODY);
        int intExtra2 = intent.getIntExtra(EXTRA_SMALLICON, 0);
        Class cls = (Class) intent.getSerializableExtra(EXTRA_ACTIVITY);
        PendingIntent activity = PendingIntent.getActivity(context, 0, new Intent(context, StartActivity.class), 0);
        Bitmap decodeResource = BitmapFactory.decodeResource(context.getResources(), context.getResources().getIdentifier("app_icon", "drawable", context.getPackageName()));
        int dimensionPixelSize = context.getResources().getDimensionPixelSize(17104901);
        int dimensionPixelSize2 = context.getResources().getDimensionPixelSize(17104902);
        if (decodeResource.getWidth() > dimensionPixelSize || decodeResource.getHeight() > dimensionPixelSize2) {
            decodeResource = Bitmap.createScaledBitmap(decodeResource, dimensionPixelSize, dimensionPixelSize2, true);
        }
        ((NotificationManager) context.getSystemService("notification")).notify(intExtra, new Builder(context).setTicker(stringExtra).setContentTitle(stringExtra).setContentText(stringExtra2).setSmallIcon(intExtra2).setLargeIcon(decodeResource).setContentIntent(activity).setDefaults(1).setAutoCancel(true).build());
    }
}
