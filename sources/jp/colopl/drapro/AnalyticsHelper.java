package p018jp.colopl.drapro;

import android.app.Activity;
import android.content.SharedPreferences;
import android.content.pm.PackageManager.NameNotFoundException;
import android.preference.PreferenceManager;
import com.google.android.apps.analytics.GoogleAnalyticsTracker;
import com.google.android.gms.nearby.messages.Strategy;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.AnalyticsHelper */
public class AnalyticsHelper {
    private static final String SETTING_KEY_INSTALL_DATE = "install";
    private static final String appid = "jp.colopl.drapro";
    private static GoogleAnalyticsTracker tracker = null;
    private static final String uacode = "drapro";
    private static String version;

    public static void SetReferrer(String str) {
        tracker.setReferrer(str);
    }

    public static void dispatch() {
        tracker.dispatch();
    }

    public static void init(Activity activity) {
        tracker = GoogleAnalyticsTracker.getInstance();
        tracker.startNewSession("drapro", Strategy.TTL_SECONDS_DEFAULT, activity);
        version = "";
        try {
            version = activity.getPackageManager().getPackageInfo("jp.colopl.drapro", 128).versionName;
        } catch (NameNotFoundException e) {
        }
        try {
            SharedPreferences defaultSharedPreferences = PreferenceManager.getDefaultSharedPreferences(activity.getApplicationContext());
            String string = defaultSharedPreferences.getString(SETTING_KEY_INSTALL_DATE, "");
            if (string == "") {
                trackPageView("/install");
                dispatch();
                string = new SimpleDateFormat("yyMMdd", Locale.JAPAN).format(new Date());
                defaultSharedPreferences.edit().putString(SETTING_KEY_INSTALL_DATE, string).commit();
            }
            tracker.setCustomVar(1, "installDate", string);
        } catch (Exception e2) {
        }
    }

    public static void stopSession() {
        tracker.stopSession();
    }

    public static void trackPageView(String str) {
        try {
            tracker.trackPageView(str + "/a/" + version);
        } catch (Exception e) {
            Util.dLog("Analytics", e.toString());
        }
    }
}
