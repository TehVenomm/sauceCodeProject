package com.zopim.android.sdk.util;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Resources.NotFoundException;
import android.util.Log;

public class AppInfo {
    private static final String TAG = AppInfo.class.getSimpleName();

    public static String getApplicationName(Context context) {
        String str = getChatSdkName() + " user";
        try {
            return context.getString(context.getApplicationInfo().labelRes);
        } catch (NotFoundException e) {
            Log.w(TAG, "Can not find application name, will return default");
            return str;
        }
    }

    public static String getApplicationVersionName(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionName;
        } catch (NameNotFoundException e) {
            Log.w(TAG, "Could not find package name " + context.getPackageName());
            e.printStackTrace();
            return "";
        }
    }

    public static String getChatSdkName() {
        return "Mobile Chat Android";
    }

    public static String getChatSdkVersionName() {
        return "1.0.1";
    }
}
