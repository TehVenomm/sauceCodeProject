package com.google.android.gms.internal;

final class zzcdd implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcbc zziuf;

    zzcdd(zzcct zzcct, zzcbc zzcbc, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziuf = zzcbc;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzb(this.zziuf, this.zziua);
    }
}
