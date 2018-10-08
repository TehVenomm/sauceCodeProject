package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.Connections.ConnectionResponseCallback;

@Deprecated
final class zzcgx extends zzciz {
    private final zzcj<ConnectionResponseCallback> zzjbl;

    public zzcgx(zzcj<ConnectionResponseCallback> zzcj) {
        this.zzjbl = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzcjv zzcjv) {
        this.zzjbl.zza(new zzcgy(this, zzcjv));
    }
}
