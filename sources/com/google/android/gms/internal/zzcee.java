package com.google.android.gms.internal;

final class zzcee implements Runnable {
    private /* synthetic */ zzcec zzivm;
    private /* synthetic */ zzcef zzivn;

    zzcee(zzcec zzcec, zzcef zzcef) {
        this.zzivm = zzcec;
        this.zzivn = zzcef;
    }

    public final void run() {
        this.zzivm.zza(this.zzivn);
        this.zzivm.zziva = null;
        this.zzivm.zzaub().zza(null);
    }
}
