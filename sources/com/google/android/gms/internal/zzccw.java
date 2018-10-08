package com.google.android.gms.internal;

final class zzccw implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcan zziuc;

    zzccw(zzcct zzcct, zzcan zzcan, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziuc = zzcan;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzb(this.zziuc, this.zziua);
    }
}
