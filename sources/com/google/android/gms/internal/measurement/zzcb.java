package com.google.android.gms.internal.measurement;

import android.annotation.TargetApi;
import android.content.Context;
import android.os.Build.VERSION;
import android.os.Process;
import android.os.UserManager;
import android.support.annotation.GuardedBy;
import android.support.annotation.RequiresApi;
import android.util.Log;

public class zzcb {
    @GuardedBy("DirectBootUtils.class")
    private static UserManager zzaan;
    private static volatile boolean zzaao = (!zzri());
    @GuardedBy("DirectBootUtils.class")
    private static boolean zzaap = false;

    private zzcb() {
    }

    public static boolean isUserUnlocked(Context context) {
        return !zzri() || zzo(context);
    }

    @TargetApi(24)
    @RequiresApi(24)
    @GuardedBy("DirectBootUtils.class")
    private static boolean zzn(Context context) {
        boolean z;
        int i = 1;
        while (true) {
            if (i > 2) {
                z = false;
                break;
            }
            if (zzaan == null) {
                zzaan = (UserManager) context.getSystemService(UserManager.class);
            }
            UserManager userManager = zzaan;
            if (userManager == null) {
                return true;
            }
            try {
                z = userManager.isUserUnlocked() || !userManager.isUserRunning(Process.myUserHandle());
            } catch (NullPointerException e) {
                Log.w("DirectBootUtils", "Failed to check if user is unlocked.", e);
                zzaan = null;
                i++;
            }
        }
        if (!z) {
            return z;
        }
        zzaan = null;
        return z;
    }

    @TargetApi(24)
    @RequiresApi(24)
    private static boolean zzo(Context context) {
        boolean z = true;
        if (!zzaao) {
            synchronized (zzcb.class) {
                try {
                    if (!zzaao) {
                        z = zzn(context);
                        if (z) {
                            zzaao = z;
                        }
                    }
                } finally {
                    Class<zzcb> cls = zzcb.class;
                }
            }
        }
        return z;
    }

    public static boolean zzri() {
        return VERSION.SDK_INT >= 24;
    }
}
