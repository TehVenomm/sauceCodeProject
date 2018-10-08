package net.gogame.gowrap.support;

import android.content.Context;
import android.content.SharedPreferences.Editor;

public final class PreferenceUtils {
    private static final String SHARED_PREFERENCES_NAME = "net.gogame.gowrap.preferences";

    private PreferenceUtils() {
    }

    public static String getPreference(Context context, String str) {
        return context.getSharedPreferences(SHARED_PREFERENCES_NAME, 0).getString(str, null);
    }

    public static void setPreference(Context context, String str, String str2) {
        Editor edit = context.getSharedPreferences(SHARED_PREFERENCES_NAME, 0).edit();
        edit.putString(str, str2);
        edit.apply();
    }

    public static void clearPreferences(Context context) {
        context.getSharedPreferences(SHARED_PREFERENCES_NAME, 0).edit().clear().commit();
    }
}
