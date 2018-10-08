package com.google.android.gms.internal;

final class zzcbp implements Runnable {
    private /* synthetic */ String zzips;
    private /* synthetic */ zzcbo zzipt;

    zzcbp(zzcbo zzcbo, String str) {
        this.zzipt = zzcbo;
        this.zzips = str;
    }

    public final void run() {
        zzcdm zzaul = this.zzipt.zzikb.zzaul();
        if (zzaul.isInitialized()) {
            zzaul.zziqf.zzf(this.zzips, 1);
        } else {
            this.zzipt.zzk(6, "Persisted config not initialized. Not logging error/warn");
        }
    }
}
