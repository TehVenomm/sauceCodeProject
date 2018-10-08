package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.Connections.ConnectionRequestListener;

@Deprecated
final class zzcgv extends zzciq {
    private final zzcj<ConnectionRequestListener> zzjbg;

    zzcgv(zzcj<ConnectionRequestListener> zzcj) {
        this.zzjbg = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzcjt zzcjt) {
        this.zzjbg.zza(new zzcgw(this, zzcjt));
    }

    public final void zza(zzckl zzckl) {
    }
}
