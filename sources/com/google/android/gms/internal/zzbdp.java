package com.google.android.gms.internal;

import android.content.Context;

public final class zzbdp {
    private static zzbdp zzfzn = new zzbdp();
    private zzbdo zzfzm = null;

    private final zzbdo zzcr(Context context) {
        zzbdo zzbdo;
        synchronized (this) {
            if (this.zzfzm == null) {
                if (context.getApplicationContext() != null) {
                    context = context.getApplicationContext();
                }
                this.zzfzm = new zzbdo(context);
            }
            zzbdo = this.zzfzm;
        }
        return zzbdo;
    }

    public static zzbdo zzcs(Context context) {
        return zzfzn.zzcr(context);
    }
}
