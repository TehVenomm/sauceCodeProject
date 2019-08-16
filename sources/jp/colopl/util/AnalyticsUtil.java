package p018jp.colopl.util;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import com.google.android.apps.analytics.GoogleAnalyticsTracker;
import p018jp.colopl.config.Config;
import p018jp.colopl.drapro.AppConsts;

/* renamed from: jp.colopl.util.AnalyticsUtil */
public class AnalyticsUtil {
    public static int SCOPE_PAGE = 3;
    public static int SCOPE_SESSION = 2;
    public static int SCOPE_USER = 1;
    public static int SLOT_NO1 = 1;
    public static int SLOT_NO2 = 2;
    public static int SLOT_NO3 = 3;
    public static int SLOT_NO4 = 4;
    public static int SLOT_NO5 = 5;
    private static final String TAG = "AnalyticsUtil";
    private String mCode;
    private Context mContext;
    private int mInterval;
    private GoogleAnalyticsTracker mTracker;

    public AnalyticsUtil(Context context) {
        this(context, true);
    }

    public AnalyticsUtil(Context context, boolean z) {
        this.mInterval = 60;
        this.mCode = AppConsts.GATrackingID;
        if (!Config.debuggable) {
            this.mTracker = GoogleAnalyticsTracker.getInstance();
            this.mContext = context;
            if (z) {
                startNewSession(this.mInterval);
            }
        }
    }

    public void dispatch() {
        if (!Config.debuggable) {
            Util.dLog(TAG, "dispatch");
            this.mTracker.dispatch();
        }
    }

    public String getVersionName() {
        if (Config.debuggable) {
            return "";
        }
        String str = "";
        try {
            return this.mContext.getPackageManager().getPackageInfo(this.mContext.getPackageName(), 128).versionName;
        } catch (NameNotFoundException e) {
            return str;
        }
    }

    public void setCustomVar(int i, String str, String str2, int i2) {
        if (!Config.debuggable) {
            Util.dLog(TAG, "setCustomVar:" + i + ":" + str + ":" + str2 + ":" + i2);
            this.mTracker.setCustomVar(i, str, str2, i2);
        }
    }

    public void startNewSession() {
        if (!Config.debuggable) {
            Util.dLog(TAG, "startNewSession:" + this.mCode);
            this.mTracker.startNewSession(this.mCode, this.mContext);
        }
    }

    public void startNewSession(int i) {
        if (!Config.debuggable) {
            Util.dLog(TAG, "startNewSession:" + this.mCode + ":" + i);
            this.mTracker.startNewSession(this.mCode, i, this.mContext);
        }
    }

    public void stopSession() {
        if (!Config.debuggable) {
            Util.dLog(TAG, "stopSession");
            this.mTracker.stopSession();
        }
    }

    public void trackEvent(String str, String str2) {
        if (!Config.debuggable) {
            trackEvent(str, str2, "");
        }
    }

    public void trackEvent(String str, String str2, String str3) {
        if (!Config.debuggable) {
            trackEvent(str, str2, str3, 1);
        }
    }

    public void trackEvent(String str, String str2, String str3, int i) {
        if (!Config.debuggable) {
            Util.dLog(TAG, "trackEvent:" + str + ":" + str2 + ":" + str3 + ":" + i);
            this.mTracker.trackEvent(str, str2, str3, i);
        }
    }

    public void trackPageView(String str) {
        if (!Config.debuggable) {
            Util.dLog(TAG, "trackPageView:" + str);
            this.mTracker.trackPageView(str);
        }
    }
}
