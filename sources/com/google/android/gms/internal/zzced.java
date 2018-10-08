package com.google.android.gms.internal;

import android.os.Bundle;
import com.google.android.gms.measurement.AppMeasurement.zzb;

final class zzced implements Runnable {
    private /* synthetic */ boolean zzivj;
    private /* synthetic */ zzb zzivk;
    private /* synthetic */ zzcef zzivl;
    private /* synthetic */ zzcec zzivm;

    zzced(zzcec zzcec, boolean z, zzb zzb, zzcef zzcef) {
        this.zzivm = zzcec;
        this.zzivj = z;
        this.zzivk = zzb;
        this.zzivl = zzcef;
    }

    public final void run() {
        if (this.zzivj && this.zzivm.zziva != null) {
            this.zzivm.zza(this.zzivm.zziva);
        }
        Object obj = (this.zzivk != null && this.zzivk.zziki == this.zzivl.zziki && zzcfo.zzau(this.zzivk.zzikh, this.zzivl.zzikh) && zzcfo.zzau(this.zzivk.zzikg, this.zzivl.zzikg)) ? null : 1;
        if (obj != null) {
            Bundle bundle = new Bundle();
            zzcec.zza(this.zzivl, bundle);
            if (this.zzivk != null) {
                if (this.zzivk.zzikg != null) {
                    bundle.putString("_pn", this.zzivk.zzikg);
                }
                bundle.putString("_pc", this.zzivk.zzikh);
                bundle.putLong("_pi", this.zzivk.zziki);
            }
            this.zzivm.zzaty().zzc("auto", "_vs", bundle);
        }
        this.zzivm.zziva = this.zzivl;
        this.zzivm.zzaub().zza(this.zzivl);
    }
}
