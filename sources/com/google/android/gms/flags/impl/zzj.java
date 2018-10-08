package com.google.android.gms.flags.impl;

import android.content.Context;
import android.content.SharedPreferences;
import com.google.android.gms.internal.zzbvf;

public final class zzj {
    private static SharedPreferences zzhaz = null;

    public static SharedPreferences zzcz(Context context) throws Exception {
        SharedPreferences sharedPreferences;
        synchronized (SharedPreferences.class) {
            try {
                if (zzhaz == null) {
                    zzhaz = (SharedPreferences) zzbvf.zza(new zzk(context));
                }
                sharedPreferences = zzhaz;
            } catch (Throwable th) {
                Class cls = SharedPreferences.class;
            }
        }
        return sharedPreferences;
    }
}
