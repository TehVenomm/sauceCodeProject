package com.zopim.android.sdk.api;

import android.util.Log;
import org.apache.commons.lang3.StringUtils;

public class Logger {
    private static boolean sEnabled = false;

    private Logger() {
    }

    /* renamed from: d */
    public static void m571d(String str, String str2) {
        logInternal(3, str, str2, null);
    }

    /* renamed from: d */
    public static void m572d(String str, String str2, Throwable th) {
        logInternal(3, str, str2, th);
    }

    /* renamed from: e */
    public static void m573e(String str, String str2) {
        logInternal(6, str, str2, null);
    }

    /* renamed from: e */
    public static void m574e(String str, String str2, Throwable th) {
        logInternal(6, str, str2, th);
    }

    /* renamed from: i */
    public static void m575i(String str, String str2) {
        logInternal(4, str, str2, null);
    }

    /* renamed from: i */
    public static void m576i(String str, String str2, Throwable th) {
        logInternal(4, str, str2, th);
    }

    private static void logInternal(int i, String str, String str2, Throwable th) {
        if (sEnabled) {
            String str3 = null;
            if (th != null) {
                str3 = StringUtils.f1199LF + Log.getStackTraceString(th);
            }
            if (str3 != null) {
                str2 = str2 + str3;
            }
            Log.println(i, str, str2);
        }
    }

    static void setEnabled(boolean z) {
        sEnabled = z;
    }

    /* renamed from: v */
    public static void m577v(String str, String str2) {
        logInternal(2, str, str2, null);
    }

    /* renamed from: v */
    public static void m578v(String str, String str2, Throwable th) {
        logInternal(2, str, str2, th);
    }

    /* renamed from: w */
    public static void m579w(String str, String str2) {
        logInternal(5, str, str2, null);
    }

    /* renamed from: w */
    public static void m580w(String str, String str2, Throwable th) {
        logInternal(5, str, str2, th);
    }
}
