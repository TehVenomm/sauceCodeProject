package com.google.android.gms.common.util;

import android.content.Context;
import android.os.DropBoxManager;
import android.util.Log;
import com.google.android.gms.common.internal.zzbp;

public final class zzf {
    private static final String[] zzfyg = new String[]{"android.", "com.android.", "dalvik.", "java.", "javax."};
    private static DropBoxManager zzfyh = null;
    private static boolean zzfyi = false;
    private static int zzfyj = -1;
    private static int zzfyk = 0;

    public static boolean zza(Context context, Throwable th) {
        return zza(context, th, 0);
    }

    private static boolean zza(Context context, Throwable th, int i) {
        try {
            zzbp.zzu(context);
            zzbp.zzu(th);
        } catch (Throwable e) {
            Log.e("CrashUtils", "Error adding exception to DropBox!", e);
        }
        return false;
    }
}
