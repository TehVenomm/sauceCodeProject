package com.google.android.gms.common.util;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.internal.zzbdp;

public final class zzc {
    public static int zzaa(Context context, String str) {
        PackageInfo zzab = zzab(context, str);
        if (zzab == null || zzab.applicationInfo == null) {
            return -1;
        }
        Bundle bundle = zzab.applicationInfo.metaData;
        return bundle != null ? bundle.getInt("com.google.android.gms.version", -1) : -1;
    }

    @Nullable
    private static PackageInfo zzab(Context context, String str) {
        try {
            return zzbdp.zzcs(context).getPackageInfo(str, 128);
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    public static boolean zzac(Context context, String str) {
        "com.google.android.gms".equals(str);
        try {
            return (zzbdp.zzcs(context).getApplicationInfo(str, 0).flags & 2097152) != 0;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
