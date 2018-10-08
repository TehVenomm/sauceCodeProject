package com.google.android.gms.common.util;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.PowerManager;
import android.os.SystemClock;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

public final class zzj {
    private static IntentFilter zzfyr = new IntentFilter("android.intent.action.BATTERY_CHANGED");
    private static long zzfys;
    private static float zzfyt = Float.NaN;

    @TargetApi(20)
    public static int zzcn(Context context) {
        int i = 1;
        if (context == null || context.getApplicationContext() == null) {
            return -1;
        }
        Intent registerReceiver = context.getApplicationContext().registerReceiver(null, zzfyr);
        int i2 = ((registerReceiver == null ? 0 : registerReceiver.getIntExtra("plugged", 0)) & 7) != 0 ? 1 : 0;
        PowerManager powerManager = (PowerManager) context.getSystemService("power");
        if (powerManager == null) {
            return -1;
        }
        int i3 = zzp.zzali() ? powerManager.isInteractive() : powerManager.isScreenOn() ? 1 : 0;
        if (i2 == 0) {
            i = 0;
        }
        return (i3 << 1) | i;
    }

    public static float zzco(Context context) {
        float f;
        synchronized (zzj.class) {
            try {
                if (SystemClock.elapsedRealtime() - zzfys >= 60000 || Float.isNaN(zzfyt)) {
                    Intent registerReceiver = context.getApplicationContext().registerReceiver(null, zzfyr);
                    if (registerReceiver != null) {
                        zzfyt = ((float) registerReceiver.getIntExtra(Param.LEVEL, -1)) / ((float) registerReceiver.getIntExtra("scale", -1));
                    }
                    zzfys = SystemClock.elapsedRealtime();
                    f = zzfyt;
                } else {
                    f = zzfyt;
                }
            } catch (Throwable th) {
                Class cls = zzj.class;
            }
        }
        return f;
    }
}
