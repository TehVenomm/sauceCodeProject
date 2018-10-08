package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.Connections.EndpointDiscoveryListener;

final class zzchc extends zzcje {
    private final zzcj<EndpointDiscoveryListener> zzjbg;

    zzchc(zzcj<EndpointDiscoveryListener> zzcj) {
        this.zzjbg = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzckb zzckb) {
        this.zzjbg.zza(new zzchd(this, zzckb));
    }

    public final void zza(zzckd zzckd) {
        this.zzjbg.zza(new zzche(this, zzckd));
    }

    public final void zza(zzckn zzckn) {
    }
}
