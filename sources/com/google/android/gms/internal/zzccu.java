package com.google.android.gms.internal;

final class zzccu implements Runnable {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;

    zzccu(zzcct zzcct, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziua = zzcak;
    }

    public final void run() {
        this.zziub.zzikb.zzazj();
        this.zziub.zzikb.zzd(this.zziua);
    }
}
