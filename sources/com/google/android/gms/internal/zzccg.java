package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import com.google.android.gms.measurement.AppMeasurement;

final class zzccg implements Runnable {
    private /* synthetic */ Context zzaok;
    private /* synthetic */ zzcco zziri;
    private /* synthetic */ long zzirj;
    private /* synthetic */ Bundle zzirk;
    private /* synthetic */ zzcbo zzirl;

    zzccg(zzccf zzccf, zzcco zzcco, long j, Bundle bundle, Context context, zzcbo zzcbo) {
        this.zziri = zzcco;
        this.zzirj = j;
        this.zzirk = bundle;
        this.zzaok = context;
        this.zzirl = zzcbo;
    }

    public final void run() {
        zzcfn zzaj = this.zziri.zzaue().zzaj(this.zziri.zzatz().getAppId(), "_fot");
        long longValue = (zzaj == null || !(zzaj.mValue instanceof Long)) ? 0 : ((Long) zzaj.mValue).longValue();
        long j = this.zzirj;
        longValue = (longValue <= 0 || (j < longValue && j > 0)) ? j : longValue - 1;
        if (longValue > 0) {
            this.zzirk.putLong("click_timestamp", longValue);
        }
        AppMeasurement.getInstance(this.zzaok).logEventInternal("auto", "_cmp", this.zzirk);
        this.zzirl.zzayi().log("Install campaign recorded");
    }
}
