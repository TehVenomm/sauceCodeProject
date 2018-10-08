package com.google.android.gms.internal;

import android.os.Handler;
import com.google.android.gms.common.internal.zzbp;

abstract class zzcau {
    private static volatile Handler zzdqx;
    private volatile long zzdqy;
    private final zzcco zzikb;
    private boolean zzimx = true;
    private final Runnable zzv = new zzcav(this);

    zzcau(zzcco zzcco) {
        zzbp.zzu(zzcco);
        this.zzikb = zzcco;
    }

    private final Handler getHandler() {
        if (zzdqx != null) {
            return zzdqx;
        }
        Handler handler;
        synchronized (zzcau.class) {
            try {
                if (zzdqx == null) {
                    zzdqx = new Handler(this.zzikb.getContext().getMainLooper());
                }
                handler = zzdqx;
            } catch (Throwable th) {
                Class cls = zzcau.class;
            }
        }
        return handler;
    }

    public final void cancel() {
        this.zzdqy = 0;
        getHandler().removeCallbacks(this.zzv);
    }

    public abstract void run();

    public final boolean zzdp() {
        return this.zzdqy != 0;
    }

    public final void zzs(long j) {
        cancel();
        if (j >= 0) {
            this.zzdqy = this.zzikb.zzvu().currentTimeMillis();
            if (!getHandler().postDelayed(this.zzv, j)) {
                this.zzikb.zzauk().zzayc().zzj("Failed to schedule delayed post. time", Long.valueOf(j));
            }
        }
    }
}
