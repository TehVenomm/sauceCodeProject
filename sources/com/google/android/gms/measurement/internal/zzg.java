package com.google.android.gms.measurement.internal;

abstract class zzg extends zzd {
    private boolean zzdh;

    zzg(zzfj zzfj) {
        super(zzfj);
        this.zzj.zzb(this);
    }

    public final void initialize() {
        if (this.zzdh) {
            throw new IllegalStateException("Can't initialize twice");
        } else if (!zzbk()) {
            this.zzj.zzid();
            this.zzdh = true;
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean isInitialized() {
        return this.zzdh;
    }

    /* access modifiers changed from: protected */
    public final void zzbi() {
        if (!isInitialized()) {
            throw new IllegalStateException("Not initialized");
        }
    }

    public final void zzbj() {
        if (this.zzdh) {
            throw new IllegalStateException("Can't initialize twice");
        }
        zzbl();
        this.zzj.zzid();
        this.zzdh = true;
    }

    /* access modifiers changed from: protected */
    public abstract boolean zzbk();

    /* access modifiers changed from: protected */
    public void zzbl() {
    }
}
