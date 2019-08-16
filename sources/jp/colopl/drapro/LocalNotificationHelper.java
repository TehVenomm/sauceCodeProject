package p018jp.colopl.drapro;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.support.p000v4.app.NotificationCompat;
import com.google.android.gms.drive.DriveFile;
import com.unity3d.player.UnityPlayer;
import java.util.Arrays;
import java.util.Calendar;
import java.util.LinkedList;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.LocalNotificationHelper */
public class LocalNotificationHelper {
    private static final String REGISTERED_NOTIFICATIONS_KEY = "NOTIFICATIONS";
    private static final String REGISTERED_NOTIFICATIONS_STORE = "jp.colopl.drapro.LocalNotificationHelper.NOTIFICATIONS_STORE_KEY";

    public static void CancelAll() {
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(new Runnable() {
            public void run() {
                try {
                    for (Integer intValue : LocalNotificationHelper.getRegisteredNotificatinos()) {
                        PendingIntent broadcast = PendingIntent.getBroadcast(activity, intValue.intValue(), new Intent(activity, LocalNotificationAlarmReceiver.class), DriveFile.MODE_WRITE_ONLY);
                        if (broadcast != null) {
                            ((AlarmManager) activity.getSystemService(NotificationCompat.CATEGORY_ALARM)).cancel(broadcast);
                        }
                    }
                    LocalNotificationHelper.storeRegisteredNotifications(Arrays.asList(new Integer[0]));
                    Util.dLog("Unity", "SuccessCancelAll");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public static void Register(int i, String str, String str2, int i2) {
        final Activity activity = UnityPlayer.currentActivity;
        Util.dLog("Unity", "Register " + i + " ; " + str + " ; " + str2 + " ; " + i2);
        final int i3 = i;
        final String str3 = str;
        final String str4 = str2;
        final int i4 = i2;
        activity.runOnUiThread(new Runnable() {
            public void run() {
                Intent intent = new Intent("android.media.action.DISPLAY_NOTIFICATION");
                intent.addCategory("android.intent.category.DEFAULT");
                Bundle bundle = new Bundle();
                bundle.putInt("id", i3);
                bundle.putString("title", str3);
                bundle.putString(LocalNotificationAlarmReceiver.EXTRA_BODY, str4);
                intent.putExtras(bundle);
                intent.setClass(activity, LocalNotificationAlarmReceiver.class);
                PendingIntent broadcast = PendingIntent.getBroadcast(activity, i3, intent, 134217728);
                Calendar instance = Calendar.getInstance();
                instance.setTimeInMillis(System.currentTimeMillis());
                instance.add(13, i4);
                ((AlarmManager) activity.getSystemService(NotificationCompat.CATEGORY_ALARM)).set(0, instance.getTimeInMillis(), broadcast);
                Util.dLog("Unity", "SuccessSet");
                try {
                    LocalNotificationHelper.addRegisteredNotification(i3);
                    Util.dLog("Unity", "SuccessRegister");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    /* access modifiers changed from: private */
    public static void addRegisteredNotification(int i) throws JSONException {
        LinkedList linkedList = new LinkedList(Arrays.asList(getRegisteredNotificatinos()));
        if (linkedList.indexOf(Integer.valueOf(i)) < 0) {
            linkedList.add(Integer.valueOf(i));
            storeRegisteredNotifications(linkedList);
        }
    }

    /* access modifiers changed from: private */
    public static Integer[] getRegisteredNotificatinos() throws JSONException {
        JSONArray jSONArray = new JSONArray(UnityPlayer.currentActivity.getSharedPreferences(REGISTERED_NOTIFICATIONS_STORE, 0).getString(REGISTERED_NOTIFICATIONS_KEY, "[]"));
        Integer[] numArr = new Integer[jSONArray.length()];
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            numArr[i] = Integer.valueOf(jSONArray.getInt(i));
        }
        return numArr;
    }

    /* access modifiers changed from: private */
    public static void storeRegisteredNotifications(List<Integer> list) {
        JSONArray jSONArray = new JSONArray(list);
        Editor edit = UnityPlayer.currentActivity.getSharedPreferences(REGISTERED_NOTIFICATIONS_STORE, 0).edit();
        edit.putString(REGISTERED_NOTIFICATIONS_KEY, jSONArray.toString());
        Util.dLog("Unity", "storeRegisteredNotifications " + jSONArray.toString());
        edit.commit();
    }
}
