package com.appsflyer;

import android.support.annotation.NonNull;
import android.util.Log;
import java.util.Locale;
import java.util.concurrent.TimeUnit;

public class AFLogger {

    /* renamed from: ˎ */
    private static long f139 = System.currentTimeMillis();

    public enum LogLevel {
        NONE(0),
        ERROR(1),
        WARNING(2),
        INFO(3),
        DEBUG(4),
        VERBOSE(5);
        

        /* renamed from: ˏ */
        private int f141;

        private LogLevel(int i) {
            this.f141 = i;
        }

        public final int getLevel() {
            return this.f141;
        }
    }

    public static void afInfoLog(String str, boolean z) {
        boolean z2;
        if (LogLevel.INFO.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z2 = true;
        } else {
            z2 = false;
        }
        if (z2) {
            Log.i(AppsFlyerLib.LOG_TAG, m183(str, false));
        }
        if (z) {
            C0469y.m373().mo6642("I", m183(str, true));
        }
    }

    public static void resetDeltaTime() {
        f139 = System.currentTimeMillis();
    }

    @NonNull
    /* renamed from: ˋ */
    private static String m183(String str, boolean z) {
        if (z || LogLevel.VERBOSE.getLevel() == AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            return new StringBuilder("(").append(m184(System.currentTimeMillis() - f139)).append(") ").append(str).toString();
        }
        return str;
    }

    /* renamed from: ॱ */
    private static void m186(String str, Throwable th, boolean z) {
        boolean z2;
        if (LogLevel.ERROR.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z2 = true;
        } else {
            z2 = false;
        }
        if (z2 && z) {
            Log.e(AppsFlyerLib.LOG_TAG, m183(str, false), th);
        }
        C0469y.m373().mo6649(th);
    }

    public static void afRDLog(String str) {
        boolean z;
        if (LogLevel.VERBOSE.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.v(AppsFlyerLib.LOG_TAG, m183(str, false));
        }
        C0469y.m373().mo6642("V", m183(str, true));
    }

    public static void afInfoLog(String str) {
        afInfoLog(str, true);
    }

    public static void afErrorLog(String str, Throwable th) {
        m186(str, th, false);
    }

    public static void afErrorLog(String str, Throwable th, boolean z) {
        m186(str, th, z);
    }

    /* renamed from: ˎ */
    private static String m184(long j) {
        long hours = TimeUnit.MILLISECONDS.toHours(j);
        long millis = j - TimeUnit.HOURS.toMillis(hours);
        long minutes = TimeUnit.MILLISECONDS.toMinutes(millis);
        long millis2 = millis - TimeUnit.MINUTES.toMillis(minutes);
        long seconds = TimeUnit.MILLISECONDS.toSeconds(millis2);
        return String.format(Locale.getDefault(), "%02d:%02d:%02d:%03d", new Object[]{Long.valueOf(hours), Long.valueOf(minutes), Long.valueOf(seconds), Long.valueOf(TimeUnit.MILLISECONDS.toMillis(millis2 - TimeUnit.SECONDS.toMillis(seconds)))});
    }

    /* renamed from: ˎ */
    static void m185(String str) {
        if (!AppsFlyerProperties.getInstance().isLogsDisabledCompletely()) {
            Log.d(AppsFlyerLib.LOG_TAG, m183(str, false));
        }
        C0469y.m373().mo6642("F", str);
    }

    public static void afDebugLog(String str) {
        boolean z;
        if (LogLevel.DEBUG.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.d(AppsFlyerLib.LOG_TAG, m183(str, false));
        }
        C0469y.m373().mo6642("D", m183(str, true));
    }

    public static void afWarnLog(String str) {
        boolean z;
        if (LogLevel.WARNING.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.w(AppsFlyerLib.LOG_TAG, m183(str, false));
        }
        C0469y.m373().mo6642("W", m183(str, true));
    }
}
