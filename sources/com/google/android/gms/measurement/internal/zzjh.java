package com.google.android.gms.measurement.internal;

abstract class zzjh extends zzje {
    private boolean zzdh;

    zzjh(zzjg zzjg) {
        super(zzjg);
        this.zzkz.zzb(this);
    }

    public final void initialize() {
        if (this.zzdh) {
            throw new IllegalStateException("Can't initialize twice");
        }
        zzbk();
        this.zzkz.zzjs();
        this.zzdh = true;
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

    /* access modifiers changed from: protected */
    public abstract boolean zzbk();
}
