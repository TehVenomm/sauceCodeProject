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
import java.io.Writer;
import java.util.Locale;
import org.apache.commons.lang3.StringUtils;

public class DiagnosticSupport {
    public static final int ANDROID_API_LEVEL;

    static {
        int i;
        try {
            i = VERSION.class.getField("SDK_INT").getInt(null);
        } catch (Exception e) {
            i = Integer.parseInt(VERSION.SDK);
        }
        ANDROID_API_LEVEL = i;
    }

    public static String createDiagnosis(Activity activity, Exception exception) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("Application version: " + getApplicationVersionString(activity) + StringUtils.LF);
        stringBuilder.append("Device locale: " + Locale.getDefault().toString() + "\n\n");
        stringBuilder.append("Android ID: " + getAndroidId(activity, "n/a"));
        stringBuilder.append("PHONE SPECS\n");
        stringBuilder.append("model: " + Build.MODEL + StringUtils.LF);
        stringBuilder.append("brand: " + Build.BRAND + StringUtils.LF);
        stringBuilder.append("product: " + Build.PRODUCT + StringUtils.LF);
        stringBuilder.append("device: " + Build.DEVICE + "\n\n");
        stringBuilder.append("PLATFORM INFO\n");
        stringBuilder.append("Android " + VERSION.RELEASE + " " + Build.ID + " (build " + VERSION.INCREMENTAL + ")\n");
        stringBuilder.append("build tags: " + Build.TAGS + StringUtils.LF);
        stringBuilder.append("build type: " + Build.TYPE + "\n\n");
        stringBuilder.append("SYSTEM SETTINGS\n");
        ContentResolver contentResolver = activity.getContentResolver();
        try {
            stringBuilder.append("network mode: " + (Secure.getInt(contentResolver, "wifi_on") == 0 ? "DATA" : "WIFI") + StringUtils.LF);
            stringBuilder.append("HTTP proxy: " + Secure.getString(contentResolver, "http_proxy") + "\n\n");
        } catch (SettingNotFoundException e) {
            e.printStackTrace();
        }
        stringBuilder.append("STACK TRACE FOLLOWS\n\n");
        Writer stringWriter = new StringWriter();
        exception.printStackTrace(new PrintWriter(stringWriter));
        stringBuilder.append(stringWriter.toString());
        return stringBuilder.toString();
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
