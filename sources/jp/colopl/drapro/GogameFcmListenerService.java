package p018jp.colopl.drapro;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.Build.VERSION;
import android.support.p000v4.app.NotificationCompat.BigPictureStyle;
import android.support.p000v4.app.NotificationCompat.Builder;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.RemoteViews;
import com.facebook.internal.NativeProtocol;
import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.Date;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.GogameFcmListenerService */
public class GogameFcmListenerService extends FirebaseMessagingService {
    public static final String NOTIFICATION_ID = "notifyid";
    public static final String NOTIFICATION_TAG = "notifytag";
    private static final String TAG = "GogameFcmListenerService";

    private Bitmap convertBigPicture(Bitmap bitmap) {
        if (bitmap == null) {
            return null;
        }
        int height = bitmap.getHeight();
        int width = bitmap.getWidth();
        Config config = bitmap.getConfig();
        int[] iArr = new int[(width * height)];
        bitmap.getPixels(iArr, 0, width, 0, 0, width, height);
        if (((float) width) > ((float) height) * 1.8f) {
            Bitmap createBitmap = Bitmap.createBitmap(width, (int) (((float) width) / 1.8f), config);
            createBitmap.setPixels(iArr, 0, width, 0, (createBitmap.getHeight() - height) / 2, width, height);
            return createBitmap;
        }
        Bitmap createBitmap2 = Bitmap.createBitmap((int) (((float) height) * 1.8f), height, config);
        createBitmap2.setPixels(iArr, 0, width, (createBitmap2.getWidth() - width) / 2, 0, width, height);
        return createBitmap2;
    }

    private Notification createNotification(String str, String str2, String str3, int i, Bitmap bitmap, Bitmap bitmap2, PendingIntent pendingIntent) {
        Builder contentIntent = new Builder(this).setContentTitle(Html.fromHtml(str)).setContentText(Html.fromHtml(str2)).setTicker(Html.fromHtml(str3)).setSmallIcon(i).setLargeIcon(bitmap).setAutoCancel(true).setContentIntent(pendingIntent);
        return bitmap2 == null ? contentIntent.build() : new BigPictureStyle(contentIntent).bigPicture(bitmap2).setSummaryText(Html.fromHtml(str2)).build();
    }

    private PendingIntent createPendingIntent(String str, String str2) {
        if (!str.contains("?openurl=true")) {
            str = str.replaceFirst("http://", "gogamedrapro://");
        }
        Intent intent = new Intent("android.intent.action.VIEW", Uri.parse(str + (str.contains("?") ? "&" : "?") + "rt=" + System.currentTimeMillis()));
        intent.putExtra("notifyid", getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", getPackageName()));
        intent.putExtra("notifytag", str2);
        return PendingIntent.getActivity(this, (int) (System.currentTimeMillis() & 268435455), intent, 134217728);
    }

    private RemoteViews getContentView(Context context, JSONObject jSONObject) {
        Bitmap bitmap;
        int identifier = getIdentifier(context, "notification_type" + jSONObject.optInt("layout", -1), "layout");
        if (identifier == 0) {
            return null;
        }
        RemoteViews remoteViews = new RemoteViews(getPackageName(), identifier);
        View inflate = ((LayoutInflater) getSystemService("layout_inflater")).inflate(identifier, null);
        int identifier2 = getResources().getIdentifier("img", "id", getPackageName());
        if (inflate.findViewById(identifier2) != null) {
            String optString = jSONObject.optString("img", "");
            if (optString.equals("")) {
                return null;
            }
            try {
                HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(optString).openConnection();
                httpURLConnection.setDoInput(true);
                httpURLConnection.connect();
                bitmap = BitmapFactory.decodeStream(httpURLConnection.getInputStream());
            } catch (IOException e) {
                e.printStackTrace();
                bitmap = null;
            }
            if (bitmap == null) {
                return null;
            }
            remoteViews.setImageViewBitmap(identifier2, bitmap);
        }
        int identifier3 = getResources().getIdentifier("title", "id", getPackageName());
        if (inflate.findViewById(identifier3) != null) {
            remoteViews.setTextViewText(identifier3, Html.fromHtml(jSONObject.optString("title", "")));
        }
        int identifier4 = getResources().getIdentifier("message", "id", getPackageName());
        if (inflate.findViewById(identifier4) != null) {
            remoteViews.setTextViewText(identifier4, Html.fromHtml(jSONObject.optString("message", "")));
        }
        int identifier5 = getResources().getIdentifier("time", "id", getPackageName());
        if (inflate.findViewById(identifier5) != null) {
            if (jSONObject.optBoolean("time", true)) {
                remoteViews.setTextViewText(identifier5, new SimpleDateFormat("kk':'mm").format(new Date()));
            } else {
                remoteViews.setViewVisibility(identifier5, 8);
            }
        }
        int identifier6 = getResources().getIdentifier("root", "id", getPackageName());
        if (inflate.findViewById(identifier6) == null || !jSONObject.has("bgcolor")) {
            return remoteViews;
        }
        remoteViews.setInt(identifier6, "setBackgroundColor", Color.parseColor("#" + jSONObject.optString("bgcolor")));
        return remoteViews;
    }

    private int getIdentifier(Context context, String str, String str2) {
        return context.getResources().getIdentifier(str, str2, context.getPackageName());
    }

    private Bitmap getImage(String str) {
        try {
            HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
            httpURLConnection.setDoInput(true);
            httpURLConnection.connect();
            return BitmapFactory.decodeStream(httpURLConnection.getInputStream());
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    private void sendNotification(String str, String str2, String str3, String str4) {
        JSONObject jSONObject;
        RemoteViews remoteViews = null;
        Util.dLog(TAG, "sendNotification: subject = " + str + ", message = " + str2 + ", url = " + str3 + ", option = " + str4);
        Context applicationContext = getApplicationContext();
        String str5 = "";
        int identifier = getResources().getIdentifier("push_icon", "drawable", getPackageName());
        if (str4 != null) {
            try {
                jSONObject = new JSONObject(str4);
            } catch (JSONException e) {
                jSONObject = null;
            }
            if (jSONObject != null) {
                remoteViews = getContentView(applicationContext, jSONObject);
                str5 = jSONObject.optString("tag", "");
                identifier = getIdentifier(applicationContext, "ic_notification" + jSONObject.optInt(SettingsJsonConstants.APP_ICON_KEY, -1), "drawable");
                if (identifier == 0) {
                    identifier = getApplicationContext().getResources().getIdentifier("push_icon", "drawable", getApplicationContext().getPackageName());
                }
            }
        }
        int identifier2 = getApplicationContext().getResources().getIdentifier("notification_title", "string", getApplicationContext().getPackageName());
        Notification.Builder builder = new Notification.Builder(this);
        builder.setAutoCancel(false).setSmallIcon(identifier).setTicker(str).setContentTitle(getString(identifier2)).setContentText(str2).setContentIntent(createPendingIntent(str3, str5));
        if (remoteViews != null) {
            builder.setContent(remoteViews);
        }
        NotificationManager notificationManager = (NotificationManager) getSystemService("notification");
        notificationManager.cancel(str5, getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", getPackageName()));
        notificationManager.notify(str5, getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", getPackageName()), builder.build());
    }

    private void sendNotificationV2(String str, String str2, String str3, String str4) {
        String str5;
        JSONObject jSONObject;
        Bitmap bitmap;
        String str6;
        int i = 0;
        Bitmap bitmap2 = null;
        Util.dLog(TAG, "sendNotificationV2: subject = " + str + ", message = " + str2 + ", url = " + str3 + ", option = " + str4);
        int identifier = getResources().getIdentifier("notification_title", "string", getPackageName());
        Context applicationContext = getApplicationContext();
        String str7 = "";
        int identifier2 = getResources().getIdentifier("push_icon", "drawable", getPackageName());
        String string = getString(identifier);
        if (str4 != null) {
            try {
                jSONObject = new JSONObject(str4);
            } catch (JSONException e) {
                jSONObject = null;
            }
            if (jSONObject != null) {
                i = jSONObject.optInt("layout", 0);
                str7 = jSONObject.optString("tag", "");
                identifier2 = getIdentifier(applicationContext, "ic_notification" + jSONObject.optInt(SettingsJsonConstants.APP_ICON_KEY, -1), "drawable");
                if (identifier2 == 0) {
                    identifier2 = getResources().getIdentifier("push_icon", "drawable", getPackageName());
                    str5 = str7;
                }
            }
            str5 = str7;
        } else {
            str5 = str7;
            jSONObject = null;
        }
        if (i == 1 || i == 2) {
            string = jSONObject.optString("title", "");
            str2 = jSONObject.optString("message", "");
            String optString = jSONObject.optString("img", "");
            bitmap = !optString.equals("") ? getImage(optString) : null;
            if (bitmap == null) {
                bitmap = BitmapFactory.decodeResource(getResources(), getResources().getIdentifier("app_icon", "drawable", getPackageName()));
                str6 = str2;
            }
            str6 = str2;
        } else if (i == 3) {
            string = jSONObject.optString("title", "");
            str2 = jSONObject.optString("message", "");
            bitmap = BitmapFactory.decodeResource(getResources(), getResources().getIdentifier("app_icon", "drawable", getPackageName()));
            String optString2 = jSONObject.optString("img", "");
            if (!optString2.equals("")) {
                bitmap2 = convertBigPicture(getImage(optString2));
                str6 = str2;
            }
            str6 = str2;
        } else if (i == 4 || i == 5) {
            bitmap = BitmapFactory.decodeResource(getResources(), getResources().getIdentifier("app_icon", "drawable", getPackageName()));
            String optString3 = jSONObject.optString("img", "");
            if (!optString3.equals("")) {
                bitmap2 = convertBigPicture(getImage(optString3));
                str6 = str2;
            }
            str6 = str2;
        } else {
            bitmap = BitmapFactory.decodeResource(getResources(), getResources().getIdentifier("app_icon", "drawable", getPackageName()));
            str6 = str2;
        }
        int dimensionPixelSize = getResources().getDimensionPixelSize(17104901);
        int dimensionPixelSize2 = getResources().getDimensionPixelSize(17104902);
        Notification createNotification = createNotification(string, str6, str, identifier2, (bitmap.getWidth() > dimensionPixelSize || bitmap.getHeight() > dimensionPixelSize2) ? Bitmap.createScaledBitmap(bitmap, dimensionPixelSize, dimensionPixelSize2, true) : bitmap, bitmap2, createPendingIntent(str3, str5));
        NotificationManager notificationManager = (NotificationManager) getSystemService("notification");
        notificationManager.cancel(str5, getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", getPackageName()));
        notificationManager.notify(str5, getResources().getIdentifier(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING, "string", getPackageName()), createNotification);
    }

    public void onMessageReceived(RemoteMessage remoteMessage) {
        try {
            String str = (String) remoteMessage.getData().get("title");
            String str2 = (String) remoteMessage.getData().get(LocalNotificationAlarmReceiver.EXTRA_BODY);
            String str3 = (String) remoteMessage.getData().get("url");
            if (VERSION.SDK_INT >= 16) {
                sendNotificationV2(str, str2, str3, null);
            } else {
                sendNotification(str, str2, str3, null);
            }
        } catch (Exception e) {
        }
    }
}
