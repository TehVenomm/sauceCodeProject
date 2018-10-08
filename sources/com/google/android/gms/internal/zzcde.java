package com.google.android.gms.internal;

final class zzcde implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcbc zziuf;

    zzcde(zzcct zzcct, zzcbc zzcbc, String str) {
        this.zziub = zzcct;
        this.zziuf = zzcbc;
        this.zziab = str;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzb(this.zziuf, this.zziab);
    }
}
