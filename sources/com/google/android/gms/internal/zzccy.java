package com.google.android.gms.internal;

final class zzccy implements Runnable {
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcan zziuc;

    zzccy(zzcct zzcct, zzcan zzcan) {
        this.zziub = zzcct;
        this.zziuc = zzcan;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzd(this.zziuc);
    }
}
