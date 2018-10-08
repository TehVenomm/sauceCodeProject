package com.google.android.gms.internal;

final class zzcah implements Runnable {
    private /* synthetic */ String zzaoi;
    private /* synthetic */ long zzikq;
    private /* synthetic */ zzcaf zzikr;

    zzcah(zzcaf zzcaf, String str, long j) {
        this.zzikr = zzcaf;
        this.zzaoi = str;
        this.zzikq = j;
    }

    public final void run() {
        this.zzikr.zze(this.zzaoi, this.zzikq);
    }
}
