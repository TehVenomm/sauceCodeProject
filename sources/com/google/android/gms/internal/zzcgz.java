package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.EndpointDiscoveryCallback;

final class zzcgz extends zzcje {
    private final zzcj<EndpointDiscoveryCallback> zzjbg;

    zzcgz(zzcj<EndpointDiscoveryCallback> zzcj) {
        this.zzjbg = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzckb zzckb) {
        this.zzjbg.zza(new zzcha(this, zzckb));
    }

    public final void zza(zzckd zzckd) {
        this.zzjbg.zza(new zzchb(this, zzckd));
    }

    public final void zza(zzckn zzckn) {
    }
}
