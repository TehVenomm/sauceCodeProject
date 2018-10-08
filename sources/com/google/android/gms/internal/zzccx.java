package com.google.android.gms.internal;

final class zzccx implements Runnable {
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcan zziuc;

    zzccx(zzcct zzcct, zzcan zzcan) {
        this.zziub = zzcct;
        this.zziuc = zzcan;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zze(this.zziuc);
    }
}
