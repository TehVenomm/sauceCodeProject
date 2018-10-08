package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.Connections.MessageListener;

@Deprecated
final class zzchf extends zzcit {
    private final zzcj<MessageListener> zzjbp;

    zzchf(zzcj<MessageListener> zzcj) {
        this.zzjbp = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzcjz zzcjz) {
        this.zzjbp.zza(new zzchh(this, zzcjz));
    }

    public final void zza(zzckf zzckf) {
        this.zzjbp.zza(new zzchg(this, zzckf));
    }

    public final void zza(zzckh zzckh) {
    }
}
