package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Context;

final class zzcex implements Runnable {
    private /* synthetic */ zzcet zziwg;

    zzcex(zzcet zzcet) {
        this.zziwg = zzcet;
    }

    public final void run() {
        zzceg zzceg = this.zziwg.zzivw;
        Context context = this.zziwg.zzivw.getContext();
        zzcap.zzawj();
        zzceg.onServiceDisconnected(new ComponentName(context, "com.google.android.gms.measurement.AppMeasurementService"));
    }
}
