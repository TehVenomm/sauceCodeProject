package com.google.android.gms.internal;

import com.google.android.gms.measurement.AppMeasurement.ConditionalUserProperty;

final class zzcdq implements Runnable {
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ ConditionalUserProperty zziuq;

    zzcdq(zzcdo zzcdo, ConditionalUserProperty conditionalUserProperty) {
        this.zziup = zzcdo;
        this.zziuq = conditionalUserProperty;
    }

    public final void run() {
        this.zziup.zzb(this.zziuq);
    }
}
