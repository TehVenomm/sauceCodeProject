package com.google.android.gms.common.util;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.PowerManager;
import android.os.SystemClock;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

public final class zza {
    private static final IntentFilter filter = new IntentFilter("android.intent.action.BATTERY_CHANGED");
    private static long zzgv;
    private static float zzgw = Float.NaN;

    @TargetApi(20)
    public static int zzg(Context context) {
        int i = 1;
        if (context == null || context.getApplicationContext() == null) {
            return -1;
        }
        Intent registerReceiver = context.getApplicationContext().registerReceiver(null, filter);
        boolean z = ((registerReceiver == null ? 0 : registerReceiver.getIntExtra("plugged", 0)) & 7) != 0;
        PowerManager powerManager = (PowerManager) context.getSystemService("power");
        if (powerManager == null) {
            return -1;
        }
        int i2 = PlatformVersion.isAtLeastKitKatWatch() ? powerManager.isInteractive() : powerManager.isScreenOn() ? 2 : 0;
        if (!z) {
            i = 0;
        }
        return i2 | i;
    }

    public static float zzh(Context context) {
        float f;
        synchronized (zza.class) {
            try {
                if (SystemClock.elapsedRealtime() - zzgv >= 60000 || Float.isNaN(zzgw)) {
                    Intent registerReceiver = context.getApplicationContext().registerReceiver(null, filter);
                    if (registerReceiver != null) {
                        zzgw = ((float) registerReceiver.getIntExtra(Param.LEVEL, -1)) / ((float) registerReceiver.getIntExtra("scale", -1));
                    }
                    zzgv = SystemClock.elapsedRealtime();
                    f = zzgw;
                } else {
                    f = zzgw;
                }
            } finally {
                Class<zza> cls = zza.class;
            }
        }
        return f;
    }
}
