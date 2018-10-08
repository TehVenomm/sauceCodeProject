package com.google.android.gms.internal;

final class zzccv implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcan zziuc;

    zzccv(zzcct zzcct, zzcan zzcan, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziuc = zzcan;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzc(this.zziuc, this.zziua);
    }
}
