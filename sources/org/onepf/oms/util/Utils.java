package org.onepf.oms.util;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Looper;
import android.text.TextUtils;
import org.jetbrains.annotations.NotNull;

public final class Utils {
    private Utils() {
    }

    public static boolean hasRequestedPermission(@NotNull Context context, @NotNull String str) {
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Permission can't be null or empty.");
        }
        boolean z;
        try {
            PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 4096);
            if (!CollectionUtils.isEmpty(packageInfo.requestedPermissions)) {
                for (Object equals : packageInfo.requestedPermissions) {
                    if (str.equals(equals)) {
                        z = true;
                        break;
                    }
                }
            }
            z = false;
        } catch (Throwable e) {
            Logger.m4029e(e, "Error during checking permission ", str);
            z = false;
        }
        Logger.m4026d("hasRequestedPermission() is ", Boolean.valueOf(z), " for ", str);
        return z;
    }

    public static boolean isPackageInstaller(@NotNull Context context, String str) {
        Logger.m4026d("isPackageInstaller() is ", Boolean.valueOf(TextUtils.equals(context.getPackageManager().getInstallerPackageName(context.getPackageName()), str)), " for ", str);
        return TextUtils.equals(context.getPackageManager().getInstallerPackageName(context.getPackageName()), str);
    }

    public static boolean packageInstalled(@NotNull Context context, @NotNull String str) {
        boolean z;
        try {
            context.getPackageManager().getPackageInfo(str, 1);
            z = true;
        } catch (NameNotFoundException e) {
            z = false;
        }
        Logger.m4026d("packageInstalled() is ", Boolean.valueOf(z), " for ", str);
        return z;
    }

    public static boolean uiThread() {
        return Thread.currentThread() == Looper.getMainLooper().getThread();
    }
}
