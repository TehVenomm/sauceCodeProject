package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.measurement.AppMeasurement.zzb;

final class zzcek implements Runnable {
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ zzb zzivy;

    zzcek(zzceg zzceg, zzb zzb) {
        this.zzivw = zzceg;
        this.zzivy = zzb;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Failed to send current screen to service");
            return;
        }
        try {
            if (this.zzivy == null) {
                zzd.zza(0, null, null, this.zzivw.getContext().getPackageName());
            } else {
                zzd.zza(this.zzivy.zziki, this.zzivy.zzikg, this.zzivy.zzikh, this.zzivw.getContext().getPackageName());
            }
            this.zzivw.zzwt();
        } catch (RemoteException e) {
            this.zzivw.zzauk().zzayc().zzj("Failed to send current screen to the service", e);
        }
    }
}
