package com.google.android.gms.internal;

final class zzceu implements Runnable {
    private /* synthetic */ zzcbg zziwf;
    private /* synthetic */ zzcet zziwg;

    zzceu(zzcet zzcet, zzcbg zzcbg) {
        this.zziwg = zzcet;
        this.zziwf = zzcbg;
    }

    public final void run() {
        synchronized (this.zziwg) {
            this.zziwg.zziwd = false;
            if (!this.zziwg.zzivw.isConnected()) {
                this.zziwg.zzivw.zzauk().zzayi().log("Connected to service");
                this.zziwg.zzivw.zza(this.zziwf);
            }
        }
    }
}
