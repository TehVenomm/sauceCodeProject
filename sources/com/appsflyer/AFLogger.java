package com.appsflyer;

import android.support.annotation.NonNull;
import android.util.Log;
import java.util.Locale;
import java.util.concurrent.TimeUnit;

public class AFLogger {
    /* renamed from: ˎ */
    private static long f122 = System.currentTimeMillis();

    public enum LogLevel {
        NONE(0),
        ERROR(1),
        WARNING(2),
        INFO(3),
        DEBUG(4),
        VERBOSE(5);
        
        /* renamed from: ˏ */
        private int f121;

        private LogLevel(int i) {
            this.f121 = i;
        }

        public final int getLevel() {
            return this.f121;
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
            Log.i(AppsFlyerLib.LOG_TAG, m188(str, false));
        }
        if (z) {
            C0300y.m378().m388("I", m188(str, true));
        }
    }

    public static void resetDeltaTime() {
        f122 = System.currentTimeMillis();
    }

    @NonNull
    /* renamed from: ˋ */
    private static String m188(String str, boolean z) {
        if (z || LogLevel.VERBOSE.getLevel() == AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            return new StringBuilder("(").append(m189(System.currentTimeMillis() - f122)).append(") ").append(str).toString();
        }
        return str;
    }

    /* renamed from: ॱ */
    private static void m191(String str, Throwable th, boolean z) {
        boolean z2;
        if (LogLevel.ERROR.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z2 = true;
        } else {
            z2 = false;
        }
        if (z2 && z) {
            Log.e(AppsFlyerLib.LOG_TAG, m188(str, false), th);
        }
        C0300y.m378().m395(th);
    }

    public static void afRDLog(String str) {
        boolean z;
        if (LogLevel.VERBOSE.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.v(AppsFlyerLib.LOG_TAG, m188(str, false));
        }
        C0300y.m378().m388("V", m188(str, true));
    }

    public static void afInfoLog(String str) {
        afInfoLog(str, true);
    }

    public static void afErrorLog(String str, Throwable th) {
        m191(str, th, false);
    }

    public static void afErrorLog(String str, Throwable th, boolean z) {
        m191(str, th, z);
    }

    /* renamed from: ˎ */
    private static String m189(long j) {
        long toMillis = j - TimeUnit.HOURS.toMillis(TimeUnit.MILLISECONDS.toHours(j));
        toMillis -= TimeUnit.MINUTES.toMillis(TimeUnit.MILLISECONDS.toMinutes(toMillis));
        toMillis = TimeUnit.MILLISECONDS.toMillis(toMillis - TimeUnit.SECONDS.toMillis(TimeUnit.MILLISECONDS.toSeconds(toMillis)));
        return String.format(Locale.getDefault(), "%02d:%02d:%02d:%03d", new Object[]{Long.valueOf(r0), Long.valueOf(r4), Long.valueOf(r6), Long.valueOf(toMillis)});
    }

    /* renamed from: ˎ */
    static void m190(String str) {
        if (!AppsFlyerProperties.getInstance().isLogsDisabledCompletely()) {
            Log.d(AppsFlyerLib.LOG_TAG, m188(str, false));
        }
        C0300y.m378().m388("F", str);
    }

    public static void afDebugLog(String str) {
        boolean z;
        if (LogLevel.DEBUG.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.d(AppsFlyerLib.LOG_TAG, m188(str, false));
        }
        C0300y.m378().m388("D", m188(str, true));
    }

    public static void afWarnLog(String str) {
        boolean z;
        if (LogLevel.WARNING.getLevel() <= AppsFlyerProperties.getInstance().getInt("logLevel", LogLevel.NONE.getLevel())) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            Log.w(AppsFlyerLib.LOG_TAG, m188(str, false));
        }
        C0300y.m378().m388("W", m188(str, true));
    }
}
