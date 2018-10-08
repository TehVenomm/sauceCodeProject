package com.google.android.gms.internal;

final class zzcdg implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcfl zziug;

    zzcdg(zzcct zzcct, zzcfl zzcfl, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziug = zzcfl;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzc(this.zziug, this.zziua);
    }
}
