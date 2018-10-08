package com.google.android.gms.common.api.internal;

abstract class zzbm {
    private final zzbk zzfnb;

    protected zzbm(zzbk zzbk) {
        this.zzfnb = zzbk;
    }

    protected abstract void zzagy();

    public final void zzc(zzbl zzbl) {
        zzbl.zzfjy.lock();
        try {
            if (zzbl.zzfmx == this.zzfnb) {
                zzagy();
                zzbl.zzfjy.unlock();
            }
        } finally {
            zzbl.zzfjy.unlock();
        }
    }
}
