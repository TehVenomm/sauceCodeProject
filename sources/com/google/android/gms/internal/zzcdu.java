package com.google.android.gms.internal;

final class zzcdu implements Runnable {
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ long zziut;

    zzcdu(zzcdo zzcdo, long j) {
        this.zziup = zzcdo;
        this.zziut = j;
    }

    public final void run() {
        this.zziup.zzaul().zziqt.set(this.zziut);
        this.zziup.zzauk().zzayh().zzj("Minimum session duration set", Long.valueOf(this.zziut));
    }
}
