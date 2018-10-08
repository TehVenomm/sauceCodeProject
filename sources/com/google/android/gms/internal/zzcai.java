package com.google.android.gms.internal;

final class zzcai implements Runnable {
    private /* synthetic */ long zzikq;
    private /* synthetic */ zzcaf zzikr;

    zzcai(zzcaf zzcaf, long j) {
        this.zzikr = zzcaf;
        this.zzikq = j;
    }

    public final void run() {
        this.zzikr.zzak(this.zzikq);
    }
}
