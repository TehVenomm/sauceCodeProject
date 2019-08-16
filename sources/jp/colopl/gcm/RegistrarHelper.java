package p018jp.colopl.gcm;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.AsyncTask;
import android.util.Log;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.gcm.GoogleCloudMessaging;
import com.unity3d.player.UnityPlayer;
import java.io.IOException;
import p018jp.colopl.config.Config;

/* renamed from: jp.colopl.gcm.RegistrarHelper */
public class RegistrarHelper {
    public static final String GCM_SENDER_ID = "463095322801";
    private static final int PLAY_SERVICES_RESOLUTION_REQUEST = 9000;
    private static final String PROPERTY_APP_VERSION = "appVersion";
    public static final String PROPERTY_REG_ID = "registration_id";
    public static Activity activity;
    static Context context;
    static GoogleCloudMessaging gcm;

    public static void CreateRegistrationId() {
        context = activity.getApplicationContext();
        if (checkPlayServices()) {
            String registrationId = getRegistrationId(context);
            if (registrationId.equals("")) {
                gcm = GoogleCloudMessaging.getInstance(activity);
                new AsyncTask<Void, Void, String>() {
                    /* access modifiers changed from: protected */
                    public String doInBackground(Void... voidArr) {
                        if (RegistrarHelper.gcm == null) {
                            RegistrarHelper.gcm = GoogleCloudMessaging.getInstance(RegistrarHelper.activity);
                        }
                        try {
                            String register = RegistrarHelper.gcm.register(new String[]{RegistrarHelper.GCM_SENDER_ID});
                            Log.v("RegistrarHelper", "registration_id:" + register);
                            UnityPlayer.UnitySendMessage("NativeReceiver", "setGCMRegistrationId", register);
                            String str = "Device registered, registration ID=" + register;
                            Editor edit = RegistrarHelper.context.getSharedPreferences(RegistrarHelper.context.getPackageName(), 0).edit();
                            edit.putInt(RegistrarHelper.PROPERTY_APP_VERSION, Config.getVersionCode(RegistrarHelper.context));
                            edit.putString(RegistrarHelper.PROPERTY_REG_ID, register);
                            edit.commit();
                            return str;
                        } catch (IOException e) {
                            return "Error :" + e.getMessage();
                        }
                    }
                }.execute(new Void[]{null, null, null});
                return;
            }
            UnityPlayer.UnitySendMessage("NativeReceiver", "GCMRegistered", registrationId);
            return;
        }
        Log.i("", "Google Play Services は無効");
        UnityPlayer.UnitySendMessage("NativeReceiver", "GCMRegistered", "-1");
    }

    private static boolean checkPlayServices() {
        int isGooglePlayServicesAvailable = GooglePlayServicesUtil.isGooglePlayServicesAvailable(activity);
        if (isGooglePlayServicesAvailable == 0) {
            return true;
        }
        if (GooglePlayServicesUtil.isUserRecoverableError(isGooglePlayServicesAvailable)) {
            Dialog errorDialog = GooglePlayServicesUtil.getErrorDialog(isGooglePlayServicesAvailable, activity, 9000);
            if (errorDialog != null) {
                errorDialog.show();
            } else {
                showOkDialogWithText(activity, "Something went wrong. Please make sure that you have the Play Store installed and that you are connected to the internet. Contact developer with details if this persists.");
            }
        } else {
            Log.i("", "Play Service not support");
        }
        return false;
    }

    private static String getRegistrationId(Context context2) {
        SharedPreferences sharedPreferences = context2.getSharedPreferences(context2.getPackageName(), 0);
        String string = sharedPreferences.getString(PROPERTY_REG_ID, "false");
        if (string.equals("")) {
            return "";
        }
        int i = sharedPreferences.getInt(PROPERTY_APP_VERSION, Integer.MIN_VALUE);
        if (i != Config.getVersionCode(context2)) {
            return "";
        }
        Log.v("RegistrarHelper", "registration_version:" + i);
        Log.v("RegistrarHelper", "registration_id:" + string);
        return string;
    }

    public static void init(Activity activity2) {
        activity = activity2;
    }

    public static void showOkDialogWithText(Context context2, String str) {
        Builder builder = new Builder(context2);
        builder.setMessage(str);
        builder.setCancelable(true);
        builder.setPositiveButton("OK", null);
        builder.create().show();
    }
}
