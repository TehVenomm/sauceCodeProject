package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.ServiceConnection;

public abstract class zzaf {
    private static final Object zzfun = new Object();
    private static zzaf zzfuo;

    public static zzaf zzcf(Context context) {
        synchronized (zzfun) {
            if (zzfuo == null) {
                zzfuo = new zzah(context.getApplicationContext());
            }
        }
        return zzfuo;
    }

    public final void zza(String str, String str2, int i, ServiceConnection serviceConnection, String str3) {
        zzb(new zzag(str, str2, i), serviceConnection, str3);
    }

    public final boolean zza(ComponentName componentName, ServiceConnection serviceConnection, String str) {
        return zza(new zzag(componentName, 129), serviceConnection, str);
    }

    protected abstract boolean zza(zzag zzag, ServiceConnection serviceConnection, String str);

    public final void zzb(ComponentName componentName, ServiceConnection serviceConnection, String str) {
        zzb(new zzag(componentName, 129), serviceConnection, str);
    }

    protected abstract void zzb(zzag zzag, ServiceConnection serviceConnection, String str);
}
