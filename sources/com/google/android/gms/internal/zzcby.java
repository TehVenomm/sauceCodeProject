package com.google.android.gms.internal;

final class zzcby implements Runnable {
    private /* synthetic */ boolean zziqc;
    private /* synthetic */ zzcbx zziqd;

    zzcby(zzcbx zzcbx, boolean z) {
        this.zziqd = zzcbx;
        this.zziqc = z;
    }

    public final void run() {
        this.zziqd.zzikb.zzbo(this.zziqc);
    }
}
