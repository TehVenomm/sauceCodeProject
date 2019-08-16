package com.github.droidfu.support;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.Context;
import android.os.Build;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.provider.Settings.SettingNotFoundException;
import android.provider.Settings.System;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.Locale;
import org.apache.commons.lang3.StringUtils;

public class DiagnosticSupport {
    public static final int ANDROID_API_LEVEL;

    static {
        int parseInt;
        try {
            parseInt = VERSION.class.getField("SDK_INT").getInt(null);
        } catch (Exception e) {
            parseInt = Integer.parseInt(VERSION.SDK);
        }
        ANDROID_API_LEVEL = parseInt;
    }

    public static String createDiagnosis(Activity activity, Exception exc) {
        StringBuilder sb = new StringBuilder();
        sb.append("Application version: " + getApplicationVersionString(activity) + StringUtils.f1199LF);
        sb.append("Device locale: " + Locale.getDefault().toString() + "\n\n");
        sb.append("Android ID: " + getAndroidId(activity, "n/a"));
        sb.append("PHONE SPECS\n");
        sb.append("model: " + Build.MODEL + StringUtils.f1199LF);
        sb.append("brand: " + Build.BRAND + StringUtils.f1199LF);
        sb.append("product: " + Build.PRODUCT + StringUtils.f1199LF);
        sb.append("device: " + Build.DEVICE + "\n\n");
        sb.append("PLATFORM INFO\n");
        sb.append("Android " + VERSION.RELEASE + " " + Build.ID + " (build " + VERSION.INCREMENTAL + ")\n");
        sb.append("build tags: " + Build.TAGS + StringUtils.f1199LF);
        sb.append("build type: " + Build.TYPE + "\n\n");
        sb.append("SYSTEM SETTINGS\n");
        ContentResolver contentResolver = activity.getContentResolver();
        try {
            sb.append("network mode: " + (Secure.getInt(contentResolver, "wifi_on") == 0 ? "DATA" : "WIFI") + StringUtils.f1199LF);
            sb.append("HTTP proxy: " + Secure.getString(contentResolver, "http_proxy") + "\n\n");
        } catch (SettingNotFoundException e) {
            e.printStackTrace();
        }
        sb.append("STACK TRACE FOLLOWS\n\n");
        StringWriter stringWriter = new StringWriter();
        exc.printStackTrace(new PrintWriter(stringWriter));
        sb.append(stringWriter.toString());
        return sb.toString();
    }

    public static String getAndroidId(Context context) {
        String string = Secure.getString(context.getContentResolver(), "android_id");
        return string == null ? System.getString(context.getContentResolver(), "android_id") : string;
    }

    public static String getAndroidId(Context context, String str) {
        String androidId = getAndroidId(context);
        return androidId == null ? str : androidId;
    }

    public static String getApplicationVersionString(Context context) {
        try {
            return "v" + context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionName;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}
