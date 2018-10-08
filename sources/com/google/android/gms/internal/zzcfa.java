package com.google.android.gms.internal;

import android.app.job.JobParameters;

final class zzcfa implements Runnable {
    private /* synthetic */ zzcco zziri;
    final /* synthetic */ zzcbo zzirl;
    final /* synthetic */ Integer zziwj;
    final /* synthetic */ JobParameters zziwk;
    final /* synthetic */ zzcez zziwl;

    zzcfa(zzcez zzcez, zzcco zzcco, Integer num, zzcbo zzcbo, JobParameters jobParameters) {
        this.zziwl = zzcez;
        this.zziri = zzcco;
        this.zziwj = num;
        this.zzirl = zzcbo;
        this.zziwk = jobParameters;
    }

    public final void run() {
        this.zziri.zzazj();
        this.zziri.zzi(new zzcfb(this));
        this.zziri.zzazf();
    }
}
