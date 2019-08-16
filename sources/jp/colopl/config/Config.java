package p018jp.colopl.config;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager.NameNotFoundException;
import android.location.Location;
import android.os.Build.VERSION;
import android.preference.PreferenceManager;
import android.util.Log;
import com.google.android.zippy.SharedPreferencesCompat;
import p017io.fabric.sdk.android.services.common.IdManager;
import p018jp.colopl.libs.ColoplAppInfo;
import p018jp.colopl.util.Crypto;

/* renamed from: jp.colopl.config.Config */
public class Config {
    private static String PREF_KEY_PREF_LOCATON_ACCURACY = "pref_location_acc";
    private static String PREF_KEY_PREF_LOCATON_LATITUDE = "pref_location_lat";
    private static String PREF_KEY_PREF_LOCATON_LONGITUDE = "pref_location_lon";
    private static String PREF_KEY_PREF_LOCATON_PROVIDER = "pref_location_prov";
    private static String PREF_KEY_PREF_LOCATON_TIME = "pref_location_time";
    private static String PREF_REFERRER_AT_INSTALLED = "pref_ref_installed";
    private static final String SETTING_KEY_HAS_SETTINGS = "hasSettings";
    private static final String TAG = "Config";
    public static boolean debuggable = false;
    private static SharedPreferences mDefaultSharedPreferences;
    private Context context;
    private Session session;

    public Config(Context context2) {
        this.context = context2;
        this.session = new Session(context2);
    }

    private static SharedPreferences getDefaultSharedPreferences(Context context2) {
        SharedPreferences sharedPreferences;
        synchronized (Config.class) {
            try {
                if (mDefaultSharedPreferences == null) {
                    mDefaultSharedPreferences = PreferenceManager.getDefaultSharedPreferences(context2);
                }
                sharedPreferences = mDefaultSharedPreferences;
            } finally {
                Class<Config> cls = Config.class;
            }
        }
        return sharedPreferences;
    }

    private float getPreviousLocationAccuracy(SharedPreferences sharedPreferences) {
        return sharedPreferences.getFloat(PREF_KEY_PREF_LOCATON_ACCURACY, -1.0f);
    }

    private double getPreviousLocationLatitude(SharedPreferences sharedPreferences) {
        String string = sharedPreferences.getString(PREF_KEY_PREF_LOCATON_LATITUDE, IdManager.DEFAULT_VERSION_NAME);
        long j = 0;
        try {
            return Double.valueOf(string).doubleValue();
        } catch (NumberFormatException e) {
            Log.e(TAG, e.toString());
            Log.e(TAG, "getPreviousLocationLatitude: latStr = " + string);
            return j;
        }
    }

    private double getPreviousLocationLongitude(SharedPreferences sharedPreferences) {
        String string = sharedPreferences.getString(PREF_KEY_PREF_LOCATON_LONGITUDE, IdManager.DEFAULT_VERSION_NAME);
        long j = 0;
        try {
            return Double.valueOf(string).doubleValue();
        } catch (NumberFormatException e) {
            Log.e(TAG, e.toString());
            Log.e(TAG, "getPreviousLocationLongitude: lonStr = " + string);
            return j;
        }
    }

    private String getPreviousLocationProvider(SharedPreferences sharedPreferences) {
        return sharedPreferences.getString(PREF_KEY_PREF_LOCATON_PROVIDER, "");
    }

    private long getPreviousLocationTime(SharedPreferences sharedPreferences) {
        return sharedPreferences.getLong(PREF_KEY_PREF_LOCATON_TIME, 0);
    }

    public static int getVersionCode(Context context2) {
        boolean z = false;
        try {
            return context2.getPackageManager().getPackageInfo(context2.getPackageName(), 128).versionCode;
        } catch (NameNotFoundException e) {
            return z;
        }
    }

    public static String getVersionName(Context context2) {
        boolean z = false;
        try {
            return context2.getPackageManager().getPackageInfo(context2.getPackageName(), 128).versionName;
        } catch (NameNotFoundException e) {
            return z;
        }
    }

    public String generateToken() {
        try {
            return Crypto.encrypt(String.valueOf(System.currentTimeMillis()));
        } catch (Exception e) {
            return null;
        }
    }

    public String getAndroidTokenCookieName() {
        return getDefaultSharedPreferences(this.context).getString("smartphoneTokenCookieName", null);
    }

    public String getAppTypeCookieName() {
        return getDefaultSharedPreferences(this.context).getString("appTypeCookieName", "installedFrom");
    }

    public String getAppTypeCookieValue() {
        return ColoplAppInfo.isFromAuOneMarket(this.context) ? "auone" : "google";
    }

    public String getAppVersionCookieName() {
        return getDefaultSharedPreferences(this.context).getString("appVersionCookieName", "apv");
    }

    public int getOSVersion() {
        return VERSION.SDK_INT;
    }

    public String getOSVersionCookieName() {
        return getDefaultSharedPreferences(this.context).getString("osVersionCookieName", "osv");
    }

    public Location getPreviousLocation() {
        SharedPreferences defaultSharedPreferences = getDefaultSharedPreferences(this.context);
        double previousLocationLatitude = getPreviousLocationLatitude(defaultSharedPreferences);
        double previousLocationLongitude = getPreviousLocationLongitude(defaultSharedPreferences);
        long previousLocationTime = getPreviousLocationTime(defaultSharedPreferences);
        String previousLocationProvider = getPreviousLocationProvider(defaultSharedPreferences);
        float previousLocationAccuracy = getPreviousLocationAccuracy(defaultSharedPreferences);
        Location location = new Location(previousLocationProvider);
        location.setLatitude(previousLocationLatitude);
        location.setLongitude(previousLocationLongitude);
        location.setTime(previousLocationTime);
        if (previousLocationAccuracy > 0.0f) {
            location.setAccuracy(previousLocationAccuracy);
        }
        return location;
    }

    public String getReferrerAtInstalled() {
        return getDefaultSharedPreferences(this.context).getString(PREF_REFERRER_AT_INSTALLED, "");
    }

    public int getRequiredVersion() {
        return getDefaultSharedPreferences(this.context).getInt("requiredVersion", 0);
    }

    public Session getSession() {
        return this.session;
    }

    public int getVersionCode() {
        return getVersionCode(this.context);
    }

    public String getVersionName() {
        return getVersionName(this.context);
    }

    public boolean hasSettings() {
        return getDefaultSharedPreferences(this.context).getBoolean(SETTING_KEY_HAS_SETTINGS, false);
    }

    public boolean isUpdateRequired() {
        return getRequiredVersion() > getVersionCode();
    }

    public void setPreviousNWLocation(Location location) {
        if (location != null) {
            SharedPreferencesCompat.apply(getDefaultSharedPreferences(this.context).edit().putString(PREF_KEY_PREF_LOCATON_LATITUDE, String.valueOf(location.getLatitude())).putString(PREF_KEY_PREF_LOCATON_LONGITUDE, String.valueOf(location.getLongitude())).putLong(PREF_KEY_PREF_LOCATON_TIME, location.getTime()).putString(PREF_KEY_PREF_LOCATON_PROVIDER, location.getProvider()).putFloat(PREF_KEY_PREF_LOCATON_ACCURACY, location.hasAccuracy() ? location.getAccuracy() : -1.0f));
        }
    }

    public void setReferrerAtInstalled(String str) {
        SharedPreferencesCompat.apply(getDefaultSharedPreferences(this.context).edit().putString(PREF_REFERRER_AT_INSTALLED, str));
    }
}
