package com.google.android.gms.internal;

final class zzcag implements Runnable {
    private /* synthetic */ String zzaoi;
    private /* synthetic */ long zzikq;
    private /* synthetic */ zzcaf zzikr;

    zzcag(zzcaf zzcaf, String str, long j) {
        this.zzikr = zzcaf;
        this.zzaoi = str;
        this.zzikq = j;
    }

    public final void run() {
        this.zzikr.zzd(this.zzaoi, this.zzikq);
    }
}
