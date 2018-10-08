package jp.colopl.drapro;

import android.content.Context;
import android.preference.PreferenceManager;

public class Config {
    public static final int APP_PURCHASE_TYPE = 0;
    public static final int APP_PURCHASE_TYPE_AMAZON = 1;
    public static final int APP_PURCHASE_TYPE_AU = 2;
    public static final int APP_PURCHASE_TYPE_GOOGLE = 0;
    private static final String LAST_BOOT_TIME_LOCAL = "lastlBootTimeLocal";
    private static final String LAST_BOOT_TIME_SERVER = "lastBootTimeServer";
    private static final String SETTING_KEY_ENABLE_AD_VIEW = "enableAdView";
    private static final String SETTING_KEY_LAST_VERSION_CODE = "lastVersionCode";
    private static final String SETTING_KEY_SCREEN_LOCK_MODE = "screenLockMode";
    private Context context;

    public Config(Context context) {
        this.context = context;
    }

    public boolean getEnableAdView() {
        return PreferenceManager.getDefaultSharedPreferences(this.context).getBoolean(SETTING_KEY_ENABLE_AD_VIEW, true);
    }

    public String getLastBootTime(boolean z) {
        return PreferenceManager.getDefaultSharedPreferences(this.context).getString(z ? LAST_BOOT_TIME_LOCAL : LAST_BOOT_TIME_SERVER, "");
    }

    public int getLastVersionCode() {
        return PreferenceManager.getDefaultSharedPreferences(this.context).getInt(SETTING_KEY_LAST_VERSION_CODE, 0);
    }

    public boolean getScreenLockMode() {
        return PreferenceManager.getDefaultSharedPreferences(this.context).getBoolean(SETTING_KEY_SCREEN_LOCK_MODE, true);
    }

    public boolean getSpeakerPurchased(String str) {
        return PreferenceManager.getDefaultSharedPreferences(this.context).getBoolean(str, false);
    }

    public void setEnableAdView(boolean z) {
        PreferenceManager.getDefaultSharedPreferences(this.context).edit().putBoolean(SETTING_KEY_ENABLE_AD_VIEW, z).commit();
    }

    public void setLastBootTime(boolean z, String str) {
        PreferenceManager.getDefaultSharedPreferences(this.context).edit().putString(z ? LAST_BOOT_TIME_LOCAL : LAST_BOOT_TIME_SERVER, str).commit();
    }

    public void setLastVersionCode(int i) {
        PreferenceManager.getDefaultSharedPreferences(this.context).edit().putInt(SETTING_KEY_LAST_VERSION_CODE, i).commit();
    }

    public void setScreenLockMode(boolean z) {
        PreferenceManager.getDefaultSharedPreferences(this.context).edit().putBoolean(SETTING_KEY_SCREEN_LOCK_MODE, z).commit();
    }

    public void setSpearkePurchased(String str, boolean z) {
        PreferenceManager.getDefaultSharedPreferences(this.context).edit().putBoolean(str, z).commit();
    }
}
