package com.google.android.gms.internal;

final class zzcdv implements Runnable {
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ long zziut;

    zzcdv(zzcdo zzcdo, long j) {
        this.zziup = zzcdo;
        this.zziut = j;
    }

    public final void run() {
        this.zziup.zzaul().zziqu.set(this.zziut);
        this.zziup.zzauk().zzayh().zzj("Session timeout duration set", Long.valueOf(this.zziut));
    }
}
