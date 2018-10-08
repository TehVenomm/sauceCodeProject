package com.google.android.gms.internal;

import com.google.android.gms.measurement.AppMeasurement.zzb;

final class zzcdk implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ String zziuh;
    private /* synthetic */ String zziui;
    private /* synthetic */ long zziuj;

    zzcdk(zzcct zzcct, String str, String str2, String str3, long j) {
        this.zziub = zzcct;
        this.zziuh = str;
        this.zziab = str2;
        this.zziui = str3;
        this.zziuj = j;
    }

    public final void run() {
        if (this.zziuh == null) {
            this.zziub.zzikb.zzauc().zza(this.zziab, null);
            return;
        }
        zzb zzb = new zzb();
        zzb.zzikg = this.zziui;
        zzb.zzikh = this.zziuh;
        zzb.zziki = this.zziuj;
        this.zziub.zzikb.zzauc().zza(this.zziab, zzb);
    }
}
