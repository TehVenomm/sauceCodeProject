package p018jp.colopl.config;

import android.content.Context;
import android.preference.PreferenceManager;
import android.util.Log;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import com.google.android.zippy.SharedPreferencesCompat;

/* renamed from: jp.colopl.config.Session */
public class Session {
    public static String PREFERENCE_NAME = "jp.colopl.session";
    private Context context;

    Session(Context context2) {
        this.context = context2;
    }

    public String getName() {
        return "dinopt";
    }

    public String getSid() {
        return this.context.getSharedPreferences(PREFERENCE_NAME, 0).getString("sid", null);
    }

    public void setName(String str) {
        SharedPreferencesCompat.apply(PreferenceManager.getDefaultSharedPreferences(this.context).edit().putString("sessionName", str));
    }

    public void setSid(String str) {
        Log.i("SESSION:", str);
        SharedPreferencesCompat.apply(this.context.getSharedPreferences(PREFERENCE_NAME, 0).edit().putString("sid", str));
    }

    public void signOut() {
        CookieSyncManager.createInstance(this.context);
        CookieManager.getInstance().removeAllCookie();
        CookieSyncManager.getInstance().sync();
    }
}
