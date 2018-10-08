package com.google.android.gms.internal;

abstract class zzcdm extends zzcdl {
    private boolean zzdoj;

    zzcdm(zzcco zzcco) {
        super(zzcco);
        this.zzikb.zzb(this);
    }

    public final void initialize() {
        if (this.zzdoj) {
            throw new IllegalStateException("Can't initialize twice");
        }
        zzuh();
        this.zzikb.zzazi();
        this.zzdoj = true;
    }

    final boolean isInitialized() {
        return this.zzdoj;
    }

    protected abstract void zzuh();

    protected final void zzwh() {
        if (!isInitialized()) {
            throw new IllegalStateException("Not initialized");
        }
    }
}
