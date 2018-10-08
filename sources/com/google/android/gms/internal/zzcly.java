package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.StatusCallback;
import com.google.android.gms.nearby.messages.internal.zzy;

public final class zzcly extends zzy implements zzclq<StatusCallback> {
    private final zzcj<StatusCallback> zzjgz;

    zzcly(zzcj<StatusCallback> zzcj) {
        this.zzjgz = zzcj;
    }

    public final void onPermissionChanged(boolean z) {
        this.zzjgz.zza(new zzclz(this, z));
    }

    public final zzcj<StatusCallback> zzbbb() {
        return this.zzjgz;
    }
}
