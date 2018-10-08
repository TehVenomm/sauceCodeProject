package com.google.android.gms.internal;

final class zzcdh implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcfl zziug;

    zzcdh(zzcct zzcct, zzcfl zzcfl, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziug = zzcfl;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzb(this.zziug, this.zziua);
    }
}
