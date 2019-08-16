package android.support.p000v4.content;

import android.content.Context;
import android.content.Intent;
import android.content.res.ColorStateList;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Process;
import android.support.annotation.ColorInt;
import android.support.annotation.ColorRes;
import android.support.annotation.DrawableRes;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.util.TypedValue;
import java.io.File;

/* renamed from: android.support.v4.content.ContextCompat */
public class ContextCompat {
    private static final String TAG = "ContextCompat";
    private static final Object sLock = new Object();
    private static TypedValue sTempValue;

    protected ContextCompat() {
    }

    private static File buildPath(File file, String... strArr) {
        int length = strArr.length;
        int i = 0;
        File file2 = file;
        while (i < length) {
            String str = strArr[i];
            File file3 = file2 == null ? new File(str) : str != null ? new File(file2, str) : file2;
            i++;
            file2 = file3;
        }
        return file2;
    }

    public static int checkSelfPermission(@NonNull Context context, @NonNull String str) {
        if (str != null) {
            return context.checkPermission(str, Process.myPid(), Process.myUid());
        }
        throw new IllegalArgumentException("permission is null");
    }

    public static Context createDeviceProtectedStorageContext(Context context) {
        if (VERSION.SDK_INT >= 24) {
            return context.createDeviceProtectedStorageContext();
        }
        return null;
    }

    private static File createFilesDir(File file) {
        synchronized (ContextCompat.class) {
            try {
                if (!file.exists() && !file.mkdirs() && !file.exists()) {
                    Log.w(TAG, "Unable to create files subdir " + file.getPath());
                    file = null;
                }
            } finally {
                Class<ContextCompat> cls = ContextCompat.class;
            }
        }
        return file;
    }

    public static File getCodeCacheDir(Context context) {
        return VERSION.SDK_INT >= 21 ? context.getCodeCacheDir() : createFilesDir(new File(context.getApplicationInfo().dataDir, "code_cache"));
    }

    @ColorInt
    public static final int getColor(Context context, @ColorRes int i) {
        return VERSION.SDK_INT >= 23 ? context.getColor(i) : context.getResources().getColor(i);
    }

    public static final ColorStateList getColorStateList(Context context, @ColorRes int i) {
        return VERSION.SDK_INT >= 23 ? context.getColorStateList(i) : context.getResources().getColorStateList(i);
    }

    public static File getDataDir(Context context) {
        if (VERSION.SDK_INT >= 24) {
            return context.getDataDir();
        }
        String str = context.getApplicationInfo().dataDir;
        if (str != null) {
            return new File(str);
        }
        return null;
    }

    public static final Drawable getDrawable(Context context, @DrawableRes int i) {
        int i2;
        if (VERSION.SDK_INT >= 21) {
            return context.getDrawable(i);
        }
        if (VERSION.SDK_INT >= 16) {
            return context.getResources().getDrawable(i);
        }
        synchronized (sLock) {
            if (sTempValue == null) {
                sTempValue = new TypedValue();
            }
            context.getResources().getValue(i, sTempValue, true);
            i2 = sTempValue.resourceId;
        }
        return context.getResources().getDrawable(i2);
    }

    public static File[] getExternalCacheDirs(Context context) {
        if (VERSION.SDK_INT >= 19) {
            return context.getExternalCacheDirs();
        }
        return new File[]{context.getExternalCacheDir()};
    }

    public static File[] getExternalFilesDirs(Context context, String str) {
        if (VERSION.SDK_INT >= 19) {
            return context.getExternalFilesDirs(str);
        }
        return new File[]{context.getExternalFilesDir(str)};
    }

    public static final File getNoBackupFilesDir(Context context) {
        return VERSION.SDK_INT >= 21 ? context.getNoBackupFilesDir() : createFilesDir(new File(context.getApplicationInfo().dataDir, "no_backup"));
    }

    public static File[] getObbDirs(Context context) {
        if (VERSION.SDK_INT >= 19) {
            return context.getObbDirs();
        }
        return new File[]{context.getObbDir()};
    }

    public static boolean isDeviceProtectedStorage(Context context) {
        if (VERSION.SDK_INT >= 24) {
            return context.isDeviceProtectedStorage();
        }
        return false;
    }

    public static boolean startActivities(Context context, Intent[] intentArr) {
        return startActivities(context, intentArr, null);
    }

    public static boolean startActivities(Context context, Intent[] intentArr, Bundle bundle) {
        if (VERSION.SDK_INT >= 16) {
            context.startActivities(intentArr, bundle);
        } else {
            context.startActivities(intentArr);
        }
        return true;
    }

    public static void startActivity(Context context, Intent intent, @Nullable Bundle bundle) {
        if (VERSION.SDK_INT >= 16) {
            context.startActivity(intent, bundle);
        } else {
            context.startActivity(intent);
        }
    }

    public static void startForegroundService(Context context, Intent intent) {
        if (VERSION.SDK_INT >= 26) {
            context.startForegroundService(intent);
        } else {
            context.startService(intent);
        }
    }
}
