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
        boolean z;
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Permission can't be null or empty.");
        }
        try {
            PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 4096);
            if (!CollectionUtils.isEmpty((Object[]) packageInfo.requestedPermissions)) {
                String[] strArr = packageInfo.requestedPermissions;
                int length = strArr.length;
                int i = 0;
                while (true) {
                    if (i >= length) {
                        break;
                    } else if (str.equals(strArr[i])) {
                        z = true;
                        break;
                    } else {
                        i++;
                    }
                }
            }
            z = false;
        } catch (NameNotFoundException e) {
            Logger.m1029e((Throwable) e, "Error during checking permission ", str);
            z = false;
        }
        Logger.m1026d("hasRequestedPermission() is ", Boolean.valueOf(z), " for ", str);
        return z;
    }

    public static boolean isPackageInstaller(@NotNull Context context, String str) {
        boolean equals = TextUtils.equals(context.getPackageManager().getInstallerPackageName(context.getPackageName()), str);
        Logger.m1026d("isPackageInstaller() is ", Boolean.valueOf(equals), " for ", str);
        return equals;
    }

    public static boolean packageInstalled(@NotNull Context context, @NotNull String str) {
        boolean z;
        try {
            context.getPackageManager().getPackageInfo(str, 1);
            z = true;
        } catch (NameNotFoundException e) {
            z = false;
        }
        Logger.m1026d("packageInstalled() is ", Boolean.valueOf(z), " for ", str);
        return z;
    }

    public static boolean uiThread() {
        return Thread.currentThread() == Looper.getMainLooper().getThread();
    }
}
