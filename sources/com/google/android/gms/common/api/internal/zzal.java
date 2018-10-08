package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.data.DataHolder;

public abstract class zzal<L> implements zzcm<L> {
    private final DataHolder zzfkz;

    protected zzal(DataHolder dataHolder) {
        this.zzfkz = dataHolder;
    }

    protected abstract void zza(L l, DataHolder dataHolder);

    public final void zzagw() {
        if (this.zzfkz != null) {
            this.zzfkz.close();
        }
    }

    public final void zzq(L l) {
        zza(l, this.zzfkz);
    }
}
