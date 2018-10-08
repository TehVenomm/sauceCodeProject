package com.google.android.gms.internal;

import android.os.Build.VERSION;

final class zzcfb implements Runnable {
    private /* synthetic */ zzcfa zziwm;

    zzcfb(zzcfa zzcfa) {
        this.zziwm = zzcfa;
    }

    public final void run() {
        if (this.zziwm.zziwj == null) {
            zzcap.zzawj();
            if (VERSION.SDK_INT >= 24) {
                this.zziwm.zzirl.zzayi().log("AppMeasurementJobService processed last upload request.");
                ((zzcfc) this.zziwm.zziwl.zziwi).zza(this.zziwm.zziwk, false);
            }
        } else if (((zzcfc) this.zziwm.zziwl.zziwi).callServiceStopSelfResult(this.zziwm.zziwj.intValue())) {
            zzcap.zzawj();
            this.zziwm.zzirl.zzayi().zzj("Local AppMeasurementService processed last upload request. StartId", this.zziwm.zziwj);
        }
    }
}
