package com.google.android.gms.internal;

final class zzcer implements Runnable {
    private /* synthetic */ zzcfl zziug;
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ boolean zziwa;

    zzcer(zzceg zzceg, boolean z, zzcfl zzcfl) {
        this.zzivw = zzceg;
        this.zziwa = z;
        this.zziug = zzcfl;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Discarding data. Failed to set user attribute");
            return;
        }
        this.zzivw.zza(zzd, this.zziwa ? null : this.zziug);
        this.zzivw.zzwt();
    }
}
