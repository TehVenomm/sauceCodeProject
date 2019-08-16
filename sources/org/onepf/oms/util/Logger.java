package org.onepf.oms.util;

import android.text.TextUtils;
import android.util.Log;
import org.jetbrains.annotations.NotNull;

public final class Logger {
    public static final String LOG_TAG = "OpenIAB";
    @NotNull
    private static String logTag = LOG_TAG;
    private static boolean loggable;

    private Logger() {
    }

    /* renamed from: d */
    public static void m1025d(String str) {
        if (loggable || Log.isLoggable(logTag, 3)) {
            Log.d(logTag, str);
        }
    }

    /* renamed from: d */
    public static void m1026d(@NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 3)) {
            Log.d(logTag, TextUtils.join("", objArr));
        }
    }

    @Deprecated
    public static void dWithTimeFromUp(String str) {
        m1025d(str);
    }

    @Deprecated
    public static void dWithTimeFromUp(Object... objArr) {
        m1026d(objArr);
    }

    /* renamed from: e */
    public static void m1027e(String str) {
        if (loggable || Log.isLoggable(logTag, 6)) {
            Log.e(logTag, str);
        }
    }

    /* renamed from: e */
    public static void m1028e(String str, Throwable th) {
        if (loggable || Log.isLoggable(logTag, 6)) {
            Log.e(logTag, str, th);
        }
    }

    /* renamed from: e */
    public static void m1029e(Throwable th, @NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 6)) {
            Log.e(logTag, TextUtils.join("", objArr), th);
        }
    }

    /* renamed from: e */
    public static void m1030e(@NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 6)) {
            Log.e(logTag, TextUtils.join("", objArr));
        }
    }

    /* renamed from: i */
    public static void m1031i(String str) {
        if (loggable || Log.isLoggable(logTag, 4)) {
            Log.i(logTag, str);
        }
    }

    /* renamed from: i */
    public static void m1032i(@NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 4)) {
            Log.i(logTag, TextUtils.join("", objArr));
        }
    }

    @Deprecated
    public static void init() {
    }

    public static boolean isLoggable() {
        return loggable;
    }

    public static void setLogTag(String str) {
        if (TextUtils.isEmpty(str)) {
            str = LOG_TAG;
        }
        logTag = str;
    }

    public static void setLoggable(boolean z) {
        loggable = z;
    }

    /* renamed from: v */
    public static void m1033v(@NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 2)) {
            Log.v(logTag, TextUtils.join("", objArr));
        }
    }

    /* renamed from: w */
    public static void m1034w(String str) {
        if (loggable || Log.isLoggable(logTag, 5)) {
            Log.w(logTag, str);
        }
    }

    /* renamed from: w */
    public static void m1035w(String str, Throwable th) {
        if (loggable || Log.isLoggable(logTag, 5)) {
            Log.w(logTag, str, th);
        }
    }

    /* renamed from: w */
    public static void m1036w(@NotNull Object... objArr) {
        if (loggable || Log.isLoggable(logTag, 2)) {
            Log.w(logTag, TextUtils.join("", objArr));
        }
    }
}
